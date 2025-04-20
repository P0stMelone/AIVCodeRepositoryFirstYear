using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayerController : UserComponent {

        private float speed;

        private Rigidbody rb;

        public PlayerController(GameObject owner, float speed) : base (owner) {
            this.speed = speed;
        }

        public override void Awake() {
            rb = GetComponent<Rigidbody>();
        }

        public override void Update() {
            Vector2 inputDirection = Vector2.UnitX * Input.GetAxis("Horizontal") + Vector2.UnitY * Input.GetAxis("Vertical");
            if (inputDirection.LengthSquared != 0) {
                inputDirection.Normalize();
            }
            rb.Velocity = inputDirection * speed;
        }

    }
}
