
using OpenTK;

public enum BulletType {
    BlueLaser,
    FireGlobe,
    IceGlobe,
    GreenGlobe,
    Last
}


namespace Aiv.Fast2D.Component {
    public abstract class Bullet : UserComponent {

        protected BulletType bulletType;
        protected int damage;
        protected GameObject shotBy;
        protected float speed;

        protected Rigidbody rb;
        protected SpriteRenderer sr;

        public Bullet (GameObject owner, BulletType bulletType, int damage, float speed) : base (owner) {
            this.bulletType = bulletType;
            this.damage = damage;
            this.speed = speed;
        }

        public override void Start() {
            rb = GetComponent(typeof(Rigidbody)) as Rigidbody;
            sr = GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            gameObject.IsActive = false;
        }

        public virtual void Shoot (Vector2 startPosition, GameObject shotBy) {
            gameObject.IsActive = true;
            transform.Position = startPosition;
            this.shotBy = shotBy;
        }

        public virtual void Shoot (Vector2 startPosition, Vector2 direction, GameObject shotBy) {
            rb.Velocity = direction.Normalized() * speed;
            Shoot(startPosition, shotBy);
        }

        public virtual void DestroyMe () {
            gameObject.IsActive = false;
        }
    }
}
