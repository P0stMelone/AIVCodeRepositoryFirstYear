using OpenTK;
using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    public class TextObject {

        protected TextChar[] sprites; //sono i componenti che mi disegnano tutti i caratteri
        protected string text;
        protected bool isActive;
        protected Font font;
        protected int hSpace;

        public Vector2 Position;

        public string Text {
            get { return text; }
            set { SetText(value); }
        }

        public bool IsActive {
            get { return isActive; }
            set {
                isActive = value;
                UpdateCharStatus(value);
            }
        }

        public TextObject (Vector2 spritePos, int characterNumber, string textString = "", 
            Font font = null, int horizontalSpace = 0) {
            if (font == null) {
                font = FontMgr.DefaultFont;
            }
            this.font = font;
            hSpace = horizontalSpace;
            Position = spritePos;
            sprites = new TextChar[characterNumber];
            InitializeTextChar();
            SetText(textString);
        }

        private void InitializeTextChar () {
            int charX = (int)Position.X;
            int charY = (int)Position.Y;
            for (int i = 0; i < sprites.Length; i++) {
                sprites[i] = new TextChar(new Vector2(charX, charY),'z', font);
                charX += (int)(sprites[i].Width) + hSpace;
                sprites[i].IsActive = false;
            }
        }

        private void SetText (string newText) {
            if (text == newText) return;
            text = newText;
            int numChars = text.Length;
            int i = 0;
            for (; i < text.Length && i < sprites.Length; i++) {
                char c = text[i];
                sprites[i].Character = c;
                sprites[i].IsActive = true;
            }
            for(; i < sprites.Length; i++) {
                sprites[i].IsActive = false;
            }
        }

        protected virtual void UpdateCharStatus (bool activeStatus) {
            for (int i = 0; i < sprites.Length; i++) {
                sprites[i].IsActive = activeStatus;
            }
        }

    }
}
