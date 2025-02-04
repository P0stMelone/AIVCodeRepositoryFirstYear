using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    public abstract class PlayerBullet : Bullet {

        public PlayerBullet(string texturePath, int damage, float speed, BulletType bulletType) :
            base(texturePath, damage, speed, bulletType) {

        }

        protected override bool InsideBorder() {
            return Position.X <= Game.Win.Width;
        }
    }
}
