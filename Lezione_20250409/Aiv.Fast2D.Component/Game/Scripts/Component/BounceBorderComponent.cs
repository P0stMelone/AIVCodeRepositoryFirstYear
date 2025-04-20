using OpenTK;

namespace Aiv.Fast2D.Component {
    public class BounceBorderComponent : UserComponent {

        private bool bounceOnTop;
        private bool bounceOnBottom;
        private bool bounceOnLeft;
        private bool bounceOnRight;

        private SpriteRenderer sr;
        private Rigidbody rb;

        public BounceBorderComponent (GameObject owner, bool bounceOnTop, bool bounceOnBottom, bool bounceOnLeft, bool bounceOnRight) : base (owner) {
            this.bounceOnTop = bounceOnTop;
            this.bounceOnBottom = bounceOnBottom;
            this.bounceOnLeft = bounceOnLeft;
            this.bounceOnRight = bounceOnRight;
        }

        public override void Awake() {
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody>();
        }

        public override void LateUpdate() {
            BounceOnBorder();
        }

        private void BounceOnBorder() {
            if (sr.TopLeft.Y <= 0 && bounceOnTop) {
                InvertYVelocity();
            }
            if (sr.BottomRight.Y >= Game.Win.Height && bounceOnBottom) {
                InvertYVelocity();
            }
            if(sr.TopLeft.X <= 0 && bounceOnLeft) {
                InvertXVelocity();
            }
            if(sr.BottomRight.X >= Game.Win.Width && bounceOnRight) {
                InvertXVelocity();
            }
        }

        private void InvertYVelocity () {
            rb.Velocity = new Vector2(rb.Velocity.X, -rb.Velocity.Y);
        }

        private void InvertXVelocity () {
            rb.Velocity = new Vector2(-rb.Velocity.X, rb.Velocity.Y);
        }
    }
}
