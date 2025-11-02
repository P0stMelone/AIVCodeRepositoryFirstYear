using OpenTK;

namespace Aiv.Fast2D.Component {
    public class Bullet : UserComponent {

        private GameObject shotBy;
        private float speed;
        private float damage;

        private Rigidbody rb;

        public Bullet (GameObject owner, float speed, float damage) : base (owner) {
            this.speed = speed;
            this.damage = damage;
        }

        public override void Start() {
            gameObject.IsActive = false;
            rb = GetComponent<Rigidbody>();
        }

        public void ShootMe (Vector2 startPosition, Vector2 direction) {
            transform.Position = startPosition;
            direction.Normalize();
            transform.Forward = direction;
            rb.Velocity = direction * speed;
            gameObject.IsActive = true;
        }

        public override void LateUpdate() {
            if (transform.Position.X < 0 || transform.Position.X > Game.Win.OrthoWidth) {
                DespawnMe();
                return;
            }
            if (transform.Position.Y < 0 || transform.Position.Y > Game.Win.OrthoHeight) {
                DespawnMe();
                return;
            }
        }

        public override void OnCollide(Collision collisionInfo) {
            if (collisionInfo.)
        }

        public void DespawnMe () {
            gameObject.IsActive = false;
        }

    }
}
