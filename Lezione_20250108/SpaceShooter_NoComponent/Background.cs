using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    public class Background {


        private Sprite sprite;
        private Texture texture;

        public Background(string texturePath) {
            texture = new Texture(texturePath);
            sprite = new Sprite(Game.Win.Width, Game.Win.Height);
        }

        public void Draw () {
            sprite.DrawTexture(texture);
        }


    }
}
