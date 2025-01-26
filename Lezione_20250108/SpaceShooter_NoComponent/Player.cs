using Aiv.Fast2D;
using OpenTK;

namespace SpaceShooter_NoComponent {
    public class Player {

        private Sprite sprite;
        private Texture texture;

        private float speed = 250;
        private Vector2 velocity;

        public Vector2 Position {
            get { return sprite.position; }
            set { sprite.position = value; }
        }
        public int Width {
            get { return (int)sprite.Width; }
        }
        public int Height {
            get { return (int)sprite.Height; }
        }

        public Player (Vector2 position,string texturePath) {
            texture = new Texture(texturePath);
            sprite = new Sprite(texture.Width, texture.Height);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            Position = position;
        }

        public void Update () {
            Move();
            KeepInBorder();
        }

        private void Move () {
            Vector2 inputDirection = Vector2.Zero;
            if (Game.Win.GetKey(KeyCode.W)) {
                inputDirection.Y = -1;
            }
            if (Game.Win.GetKey(KeyCode.S)) {
                inputDirection.Y = 1;
            }
            if (Game.Win.GetKey(KeyCode.A)) {
                inputDirection.X = -1;
            }
            if (Game.Win.GetKey(KeyCode.D)) {
                inputDirection.X = 1;
            }
            if (inputDirection == Vector2.Zero) {
                velocity = Vector2.Zero;
            } else {
                velocity = inputDirection.Normalized() * speed;
            }
            Position += velocity * Game.Win.DeltaTime;

        }

        private void KeepInBorder () {
            if (Position.X + Width / 2 > Game.Win.Width) {
                Position = new Vector2(Game.Win.Width - Width / 2, Position.Y);
            } else if (Position.X - Width / 2 < 0) {
                Position = new Vector2(Width / 2, Position.Y);
            }
            if (Position.Y + Height / 2 > Game.Win.Height) {
                Position = new Vector2(Position.X, Game.Win.Height - Height / 2);
            } else if (Position.Y - Height / 2 < 0) {
                Position = new Vector2(Position.X, Height / 2);
            }
        }

        public void Draw () {
            sprite.DrawTexture(texture);
        }

    }
}
