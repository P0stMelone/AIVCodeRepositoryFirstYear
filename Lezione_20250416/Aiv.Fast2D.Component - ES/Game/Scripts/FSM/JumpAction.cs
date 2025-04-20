namespace Aiv.Fast2D.Component {
    public class JumpAction : Action {

        private Rigidbody rb;
        private StateMachineVariable<float> jumpForce;

        private bool gravityAffectedBefore;

        public JumpAction(Rigidbody rb, StateMachineVariable<float> jumpForce) {
            this.rb = rb;
            this.jumpForce = jumpForce;
        }

        public override void OnEnter() {
            gravityAffectedBefore = rb.IsGravityAffected;
            rb.IsGravityAffected = true;
            rb.Velocity = new OpenTK.Vector2(rb.Velocity.X, jumpForce.GetValue());
        }

        public override void OnExit() {
            rb.IsGravityAffected = gravityAffectedBefore;
        }

    }
}
