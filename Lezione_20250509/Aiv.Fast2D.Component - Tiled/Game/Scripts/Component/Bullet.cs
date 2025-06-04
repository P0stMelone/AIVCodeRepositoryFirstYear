using OpenTK;

namespace Aiv.Fast2D.Component {

    public enum BulletType { GreenGlobe, RedGlobe, Last}

    public class Bullet : UserComponent {

        protected BulletType bulletType;
        protected int damage;
        protected GameObject shotBy;
        protected float speed;

        protected Rigidbody rb;
        protected SpriteRenderer sr;

        public Bullet(GameObject owner, BulletType bulletType, int damage, float speed) : base (owner) {
            this.bulletType = bulletType;
            this.damage = damage;
            this.speed = speed;
        }

        public override void Awake() {
            rb = GetComponent<Rigidbody>();
            sr = GetComponent<SpriteRenderer>();
            gameObject.IsActive = false;
        }

        public override void LateUpdate() {
            if (CameraMgr.InsideCameraLimits(transform.Position)) return;
            DestroyBullet();
        }

        public void Shoot (Vector2 startPosition, GameObject shotBy) {
            transform.Position = startPosition;
            this.shotBy = shotBy;
            gameObject.IsActive = true;
        }

        public void Shoot (Vector2 startPosition, Vector2 direction, GameObject shotBy) {
            rb.Velocity = direction.Normalized() * speed;
            Shoot(startPosition, shotBy);
        }

        public void DestroyBullet () {
            gameObject.IsActive = false;
        }

    }
}
