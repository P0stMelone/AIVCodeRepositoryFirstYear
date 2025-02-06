using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    public abstract class EnemyBullet : Bullet {

        public EnemyBullet(string texturePath, int damage, float speed, BulletType bulletType) :
            base(texturePath, damage, speed, bulletType) {
            rigidbody.Type = RigidbodyType.EnemyBullet;
            rigidbody.AddCollisionType(RigidbodyType.Player);
            rigidbody.Collider = ColliderFactory.CreateCircleFor(this);
        }

        protected override bool InsideBorder() {
            return Position.X >= -Width;
        }

        public override void OnCollide(GameObject other) {
            ((Player)other).TakeDamge(damage);
            DespawnMe();
        }
    }
}
