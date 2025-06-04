using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayerController : UserComponent {

        private string horizontalAxis;
        private string verticalAxis;
        private float speed;

        private Rigidbody rb;

        public PlayerController (GameObject owner, string horizontalAxis, 
            string verticalAxis, float speed) : base (owner) {
            this.horizontalAxis = horizontalAxis;
            this.verticalAxis = verticalAxis;
            this.speed = speed;
        }

        public override void Awake() {
            rb = GetComponent<Rigidbody>();
        }

        public override void Update() {
            Vector2 input = new Vector2(Input.GetAxis(horizontalAxis), 
                Input.GetAxis(verticalAxis));
            if (input != Vector2.Zero) {
                input.Normalize();
                rb.Velocity = input * speed;
            }
        }

    }
}
