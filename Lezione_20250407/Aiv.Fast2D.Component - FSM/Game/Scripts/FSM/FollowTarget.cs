using OpenTK;

namespace Aiv.Fast2D.Component {
    public class FollowTarget : Action {

        private Rigidbody rb;
        private StateMachineVariable<Vector2> targetPosition;
        private StateMachineVariable<float> speed;
        private bool everyFrame;

        public FollowTarget(Rigidbody rb, StateMachineVariable<Vector2> targetPosition, 
            StateMachineVariable<float> speed, bool everyFrame) {
            this.rb = rb;
            this.targetPosition = targetPosition;
            this.speed = speed;
            this.everyFrame = everyFrame;
        }

        public override void OnEnter () {
            InternalSetVelocity();
        }

        public override void OnUpdate() {
            if (!everyFrame) return;
            InternalSetVelocity();
        }


        private void InternalSetVelocity () {
            Vector2 direction = (targetPosition.GetValue() - rb.transform.Position).Normalized();
            rb.Velocity = direction * speed.GetValue();
        }


    }
}
