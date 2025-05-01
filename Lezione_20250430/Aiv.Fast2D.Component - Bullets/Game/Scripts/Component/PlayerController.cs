using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayerController : UserComponent {

        private string axisName, jumpButtonName, shootButtonName;
        private float speed, jumpVelocity;
        private int maxConsecutiveJump;

        private Rigidbody rigibody;

        private int jumpCount;

        private bool isGrounded;
        public bool IsGrounded {
            get { return isGrounded; }
            set {
                isGrounded = value;
            }
        }

        public PlayerController (GameObject owner, string axisName, string jumpButtonName, string shootButtonName,
                float speed, float jumpVelocity, int maxConsecutiveJump) : base (owner) {
            this.axisName = axisName;
            this.jumpButtonName = jumpButtonName;
            this.shootButtonName = shootButtonName;
            this.speed = speed;
            this.jumpVelocity = jumpVelocity;
            this.maxConsecutiveJump = maxConsecutiveJump;
            jumpCount = 0;
        }

        public override void Awake() {
            rigibody = GetComponent<Rigidbody>();
        }

        public override void Update() {
            HandleMovement();
            HandleJump();
        }

        public override void LateUpdate() {
            if (transform.Position.Y <= Game.Win.OrthoHeight) return;
            TouchedGround(Game.Win.OrthoHeight);
        }

        private void HandleMovement () {
            float axis = Input.GetAxis(axisName);
            rigibody.Velocity = new Vector2(axis * speed, rigibody.Velocity.Y);
        }

        private void HandleJump () {
            if (jumpCount >= maxConsecutiveJump) return;
            if (!Input.GetInputActionButtonDown(jumpButtonName)) return;
            IsGrounded = false;
            rigibody.Velocity = new Vector2(rigibody.Velocity.X, jumpVelocity);
            rigibody.IsGravityAffected = true;
            jumpCount++;
        }

        public void TouchedGround (float groundYPosition) {
            transform.Position = new Vector2(transform.Position.X, groundYPosition);
            rigibody.Velocity = new Vector2(rigibody.Velocity.X, 0);
            rigibody.IsGravityAffected = false;
            IsGrounded = true;
            jumpCount = 0;
        }


    }
}
