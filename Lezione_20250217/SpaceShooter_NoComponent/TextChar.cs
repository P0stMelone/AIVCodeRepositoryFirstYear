using OpenTK;
using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    public class TextChar : GameObject {

        protected char character;
        protected Vector2 textureOffset;
        protected Font font;

        protected bool isActive;

        public override bool IsActive {
            get { return isActive; }
            set { isActive = value; }
        }

        public char Character {
            get { return character; }
            set {
                if (character == value) return;
                character = value;
                ComputeOffset();
            }
        }


        public TextChar(Vector2 spritePosition, char character, Font font):
            base(font.TextureName, font.CharacterWidth, font.CharacterHeight) {
            Position = spritePosition;
            this.font = font;
            Character = character;
            sprite.pivot = Vector2.Zero;
        }

        private void ComputeOffset () {
            textureOffset = font.GetOffset(character);
        }

        public override void Draw() {
            sprite.DrawTexture(texture, (int)textureOffset.X, (int)textureOffset.Y,
                font.CharacterWidth, font.CharacterHeight);
        }

    }
}
