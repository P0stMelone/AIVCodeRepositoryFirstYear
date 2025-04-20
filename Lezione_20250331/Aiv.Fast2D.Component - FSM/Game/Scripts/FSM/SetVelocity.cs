using OpenTK;

namespace Aiv.Fast2D.Component {
    public class SetVelocity : Action {

        private Rigidbody rb;
        private Vector2 velocityToSet;
        private bool everyFrame;

        public SetVelocity (Rigidbody rb, Vector2 velocityToSet, bool everyFrame) {
            this.rb = rb;
            this.velocityToSet = velocityToSet;
            this.everyFrame = everyFrame;
        }

        public override void OnEnter() {
            InternalSetVelocity();
        }

        public override void OnUpdate() {
            if (!everyFrame) return;
            InternalSetVelocity();
        }

        private void InternalSetVelocity () {
            rb.Velocity = velocityToSet;
        }

    }
}
