using OpenTK;

namespace Aiv.Fast2D.Component {
    public class RotateToTarget : Action {

        public StateMachineVariable<Transform> objectToRotate;
        public StateMachineVariable<Transform> target;
        public bool everyFrame;

        private RotateToTarget (StateMachineVariable<Transform> objectToRotate, 
            StateMachineVariable<Transform> target, bool everyFrame) {
            this.objectToRotate = objectToRotate;
            this.target = target;
            this.everyFrame = everyFrame;
        }

        public override void OnEnter() {
            InternalRotate();
        }

        public override void OnUpdate() {
            if (!everyFrame) return;
            InternalRotate();
        }


        private void InternalRotate () {
            Vector2 direction = target.GetValue().Position - objectToRotate.GetValue().Position;
            direction.Normalize();
            objectToRotate.GetValue().Forward = direction;
        }


        public static RotateToTarget Factory (Transform objectToRotate, Transform target, 
            string objectToRotateName, string targetName, FSMComponent fsm, bool everyFrame) {
            return new RotateToTarget(
                StateMachineVariable<Transform>.StateMachineVariableFactory(objectToRotate, objectToRotateName, fsm),
                StateMachineVariable<Transform>.StateMachineVariableFactory(target, targetName, fsm),
                everyFrame);
        }
    }
}
