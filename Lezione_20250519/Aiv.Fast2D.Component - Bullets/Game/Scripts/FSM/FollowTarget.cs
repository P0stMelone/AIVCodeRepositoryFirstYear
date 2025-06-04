namespace Aiv.Fast2D.Component {
    public class FollowTarget : Action {

        private StateMachineVariable<Rigidbody> owner;
        private StateMachineVariable<Transform> target;
        private StateMachineVariable<float> speed;
        private StateMachineVariable<float> offset;
        private bool everyFrame;

        private FollowTarget (StateMachineVariable<Rigidbody> owner, StateMachineVariable<Transform> target,
            StateMachineVariable<float> speed, StateMachineVariable<float> offset, bool everyFrame) {
            this.owner = owner;
            this.target = target;
            this.speed = speed;
            this.offset = offset;
            this.everyFrame = everyFrame;
        }

        public override void OnEnter() {
            InternalFollowTarget();
        }

        public override void OnUpdate() {
            if (!everyFrame) return;
            InternalFollowTarget();
        }

        private void InternalFollowTarget () {
            float xOwner = owner.GetValue().transform.Position.X;
            float xTarget = target.GetValue().transform.Position.X;
            if (xOwner > xTarget + offset) {
                owner.GetValue().Velocity = new OpenTK.Vector2(-speed, owner.GetValue().Velocity.Y);
            } else if (xOwner < xTarget - offset) {
                owner.GetValue().Velocity = new OpenTK.Vector2(speed, owner.GetValue().Velocity.Y);
            }
        }


        public static FollowTarget Factory(Rigidbody owner, Transform target, float speed, float offset, bool everyFrame,
            string ownerVariableName = "", string targetVariableName = "", string speedVariableName = "",
            string offsetVariableName = "", FSMComponent fsm = null) {

            var ownerVar = string.IsNullOrEmpty(ownerVariableName) ?
                new StateMachineVariable<Rigidbody>(owner) :
                new StateMachineVariable<Rigidbody>(fsm, ownerVariableName);
            var targetVar = string.IsNullOrEmpty(targetVariableName) ?
                new StateMachineVariable<Transform>(target) :
                new StateMachineVariable<Transform>(fsm, targetVariableName);
            var speedVar = string.IsNullOrEmpty(speedVariableName) ?
                new StateMachineVariable<float>(speed) :
                new StateMachineVariable<float>(fsm, speedVariableName);
            var offsetVar = string.IsNullOrEmpty(offsetVariableName) ?
                new StateMachineVariable<float>(offset) :
                new StateMachineVariable<float>(fsm, offsetVariableName);

            return new FollowTarget(ownerVar, targetVar, speedVar, offsetVar, everyFrame);
        }
    }
}
