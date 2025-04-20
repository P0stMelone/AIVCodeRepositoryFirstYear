using OpenTK;

namespace Aiv.Fast2D.Component {
    public class AsteroidComponent : UserComponent {

        private Rigidbody rb;
        private SpriteRenderer sr;

        private int impactDamage;

        public float Width {
            get { return sr.Width; }
        }
        public float Height {
            get { return sr.Height; }
        }

        public AsteroidComponent(GameObject gameObject, int damage) : base(gameObject) {
            impactDamage = damage;
        }

        public override void Start() {
            rb = GetComponent<Rigidbody>();
            sr = GetComponent<SpriteRenderer>();
            gameObject.IsActive = false;
        }

        public void SpawnAsteroid(Vector2 startPosition, Vector2 velocity) {
            transform.Position = startPosition;
            rb.Velocity = velocity;
            gameObject.IsActive = true;
        }

        public override void Update() {
            if (sr.BottomRight.X < 0) DestroyMe();
        }

        private void DestroyMe() {
            gameObject.IsActive = false;
        }


        public override void OnCollide(GameObject other) {
            if (other.Layer == (uint)Layer.Player) {
                (other.GetComponent(typeof(PlayerController)) as PlayerController).TakeDamage(impactDamage);
                DestroyMe();
            }
        }

    }
}
