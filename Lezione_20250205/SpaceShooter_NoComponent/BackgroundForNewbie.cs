using OpenTK;
using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    class BackgroundForNewbie {


        private Sprite head;
        private Sprite tail;
        private Texture texture;

        private float speed = 100;

        public BackgroundForNewbie(string texturePath) {
            texture = new Texture(texturePath);
            texture.SetRepeatX(true);
            head = new Sprite(Game.Win.Width, Game.Win.Height);
            tail = new Sprite(Game.Win.Width, Game.Win.Height);
        }

        public void Draw() {
            head.position += -Vector2.UnitX * speed * Game.Win.DeltaTime;
            if (head.position.X < -head.Width) {
                head.position = Vector2.Zero;
            }
            tail.position = head.position + Vector2.UnitX * head.Width;
            head.DrawTexture(texture);
            tail.DrawTexture(texture);
        }

    }
}
