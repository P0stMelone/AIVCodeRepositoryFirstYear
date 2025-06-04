namespace Aiv.Fast2D.Component {
    public class FollowTarget : Action {

        private StateMachineVariable<Rigidbody> owner;
        private StateMachineVariable<Transform> target;
        private StateMachineVariable<float> speed;
        private StateMachineVariable<float> offset;
        private bool everyFrame;

        public FollowTarget (StateMachineVariable<Rigidbody> owner, StateMachineVariable<Transform> target,
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

    }
}
