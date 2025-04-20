using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class TextBox : Component, IDrawable {

        public DrawLayer Layer { get { return DrawLayer.GUI; } }

        private Font font;
        private Sprite sprite;
        private Vector2[] availableCharacters_offset;
        private Vector2[] availableCharacters_positions;

        public int MaxCharacters {
            get { return availableCharacters_offset.Length; }
        }

        private string currentText;

        public TextBox (GameObject owner, Font font, int maxCharacters, Vector2 fontScale) : base (owner) {
            currentText = string.Empty;
            this.font = font;
            availableCharacters_offset = new Vector2[maxCharacters];
            availableCharacters_positions = new Vector2[maxCharacters];

            sprite = new Sprite(font.CharacterWidth * fontScale.X, font.CharacterHeight * fontScale.Y);
            DrawMgr.AddItem(this);

        }

        public void SetText (string text) {
            if (currentText == text) return;
            currentText = text;
            int maxIndex = GetMax();
            int xIndex = 0;
            float xPos = transform.Position.X;
            float yPos = transform.Position.Y;

            for (int i = 0; i < maxIndex; i++) {
                if (currentText[i].Equals('\n')) {
                    yPos += sprite.Height;
                    xIndex = 0;
                    continue;
                }
                availableCharacters_positions[i].X = xPos + xIndex * sprite.Width;
                availableCharacters_positions[i].Y = yPos;
                availableCharacters_offset[i] = font.GetOffset(currentText[i]);
                xIndex++;
            }
        }

        public void Draw() {
            int maxIndex = GetMax();
            for (int i = 0; i < maxIndex; i++) {
                sprite.position = availableCharacters_positions[i];
                sprite.DrawTexture(font.Texture, (int)availableCharacters_offset[i].X,
                    (int)availableCharacters_offset[i].Y, font.CharacterWidth, font.CharacterHeight);
            }
        }

        private int GetMax () {
            return currentText.Length < availableCharacters_positions.Length ? currentText.Length :
                availableCharacters_positions.Length;
        }

        public void SetScale(Vector2 fontScale) {
            sprite = new Sprite(font.CharacterWidth * fontScale.X, font.CharacterHeight * fontScale.Y);
        }

    }
}
