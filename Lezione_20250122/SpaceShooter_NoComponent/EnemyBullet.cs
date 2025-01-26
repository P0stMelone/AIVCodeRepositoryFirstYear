using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    public abstract class EnemyBullet : Bullet {

        public EnemyBullet(Texture texture, int damage, float speed, BulletType bulletType) :
            base(texture, damage, speed, bulletType) {

        }

        protected override bool InsideBorder() {
            return Position.X >= -Width;
        }
    }
}
