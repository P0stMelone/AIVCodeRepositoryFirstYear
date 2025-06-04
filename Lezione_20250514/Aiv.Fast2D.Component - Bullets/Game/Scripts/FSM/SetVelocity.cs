using OpenTK;

namespace Aiv.Fast2D.Component {
    public class SetVelocity : Action {

        private StateMachineVariable<Rigidbody> rb;
        private StateMachineVariable<Vector2> velocity;
        private bool everyFrame;

        public SetVelocity(StateMachineVariable<Rigidbody> rb, StateMachineVariable<Vector2> velocity, bool everyFrame) {
            this.rb = rb;
            this.velocity = velocity;
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
            rb.GetValue().Velocity = velocity;
        }

    }
}
