using OpenTK;

namespace Aiv.Fast2D.Component {
    public class SetTargetFromTransform : Action {

        private StateMachineVariable<Vector2> targetToSet;
        private Transform targetTransform;
        private bool everyFrame;


        public SetTargetFromTransform(StateMachineVariable<Vector2> targetToSet, Transform targetTransform, bool everyFrame) {
            this.targetToSet = targetToSet;
            this.targetTransform = targetTransform;
            this.everyFrame = everyFrame;
        }

        public override void OnEnter() {
            InternalSet();
        }

        public override void OnUpdate() {
            if (!everyFrame) return;
            InternalSet();
        }


        private void InternalSet () {
            targetToSet.SetValue(targetTransform.Position);
        }

    }
}
