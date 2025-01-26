using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    public abstract class PlayerBullet : Bullet {

        public PlayerBullet(Texture texture, int damage, float speed, BulletType bulletType) :
            base(texture, damage, speed, bulletType) {

        }

        protected override bool InsideBorder() {
            return Position.X <= Game.Win.Width;
        }
    }
}
