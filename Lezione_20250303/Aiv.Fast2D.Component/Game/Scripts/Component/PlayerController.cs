using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayerController : UserComponent {

        private KeyCode upKey, downKey, leftKey, rightKey;
        private float speed;

        private Rigidbody rb;

        public PlayerController(GameObject gameObject, float speed, KeyCode upKey, KeyCode downKey, KeyCode leftKey, KeyCode rightKey) : base(gameObject) {
            this.speed = speed;
            this.upKey = upKey;
            this.downKey = downKey;
            this.leftKey = leftKey;
            this.rightKey = rightKey;
        }

        public override void Start() {
            rb = GetComponent(typeof(Rigidbody)) as Rigidbody;
        }


        public override void Update () {
            Vector2 input = Vector2.Zero;
            if (Game.Win.GetKey(upKey)) {
                input.Y -= 1;
            } else if (Game.Win.GetKey(downKey)) {
                input.Y += 1;
            }
            if (Game.Win.GetKey(leftKey)) {
                input.X -= 1;
            } else if (Game.Win.GetKey(rightKey)) {
                input.X += 1;
            }
            if (input != Vector2.Zero) input.Normalize();
            rb.Velocity = input * speed;
        }

    }
}
