using OpenTK;

namespace Aiv.Fast2D.Component {
    public class ShootAtTarget : Action {

        private StateMachineVariable<ShootModule> sm;
        private StateMachineVariable<Transform> target;
        private Vector2 startPositionOffset;
        private Vector2 targetOffset;

        public ShootAtTarget (StateMachineVariable<ShootModule> sm, StateMachineVariable<Transform> target,
            Vector2 startPositionOffset, Vector2 targetOffset) {
            this.sm = sm;
            this.target = target;
            this.startPositionOffset = startPositionOffset;
            this.targetOffset = targetOffset;
        }

        public override void OnEnter() {
            Vector2 startPosition = sm.GetValue().transform.Position + startPositionOffset;
            Vector2 targetPosition = target.GetValue().transform.Position + targetOffset;
            Vector2 direction = targetPosition - startPosition;
            sm.GetValue().Shoot(startPosition, direction);
        }

    }
}
