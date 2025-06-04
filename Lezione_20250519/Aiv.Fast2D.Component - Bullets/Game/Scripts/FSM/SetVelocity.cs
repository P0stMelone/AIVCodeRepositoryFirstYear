using OpenTK;

namespace Aiv.Fast2D.Component {
    public class SetVelocity : Action {

        private StateMachineVariable<Rigidbody> rb;
        private StateMachineVariable<Vector2> velocity;
        private bool everyFrame;

        private SetVelocity(StateMachineVariable<Rigidbody> rb, StateMachineVariable<Vector2> velocity, bool everyFrame) {
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

        public static SetVelocity Factory(Rigidbody rb, Vector2 velocity, bool everyFrame,
            string rbVariableName = "", string velocityVariableName = "", FSMComponent fsm = null) {

            var rbVar = string.IsNullOrEmpty(rbVariableName) ?
                new StateMachineVariable<Rigidbody>(rb) :
                new StateMachineVariable<Rigidbody>(fsm, rbVariableName);
            var velVar = string.IsNullOrEmpty(velocityVariableName) ?
                new StateMachineVariable<Vector2>(velocity) :
                new StateMachineVariable<Vector2>(fsm, velocityVariableName);

            return new SetVelocity(rbVar, velVar, everyFrame);
        }

    }
}
