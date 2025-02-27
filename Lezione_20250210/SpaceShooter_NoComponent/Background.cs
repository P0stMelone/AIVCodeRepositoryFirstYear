using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    public class Background : IDrawable {

        public bool IsActive {
            get { return true; }
        }

        private Sprite sprite;
        private Texture texture;

        private float speed = 100;
        private float currentXOffset;

        public Background(string texturePath) {
            texture = new Texture(texturePath);
            texture.SetRepeatX(true);
            sprite = new Sprite(Game.Win.Width, Game.Win.Height);
            currentXOffset = 0;
            DrawMgr.AddItem(this);
        }

        public void Draw () {
            currentXOffset += speed * Game.Win.DeltaTime;
            currentXOffset = currentXOffset % texture.Width;
            sprite.DrawTexture(texture, (int)currentXOffset, 0);
        }


    }
}
