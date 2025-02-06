using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    public abstract class PlayerBullet : Bullet {

        public PlayerBullet(string texturePath, int damage, float speed, BulletType bulletType) :
            base(texturePath, damage, speed, bulletType) {
            rigidbody.Type = RigidbodyType.PlayerBullet;
            rigidbody.AddCollisionType(RigidbodyType.Enemy);
            rigidbody.AddCollisionType(RigidbodyType.EnemyBullet);
        }

        protected override bool InsideBorder() {
            return Position.X <= Game.Win.Width;
        }

        public override void OnCollide(GameObject other) {
            if (other is Enemy) {
                Enemy e = (Enemy)other;
                e.TakeDamge(damage);
            } else {
                ((EnemyBullet)other).DespawnMe();
            }
            DespawnMe();
        }
    }
}
