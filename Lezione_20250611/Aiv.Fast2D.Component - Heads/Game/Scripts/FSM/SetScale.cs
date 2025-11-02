using OpenTK;

namespace Aiv.Fast2D.Component {
    public class SetScale : Action {

        private StateMachineVariable<Transform> owner;
        private StateMachineVariable<Vector2> scale;


        private SetScale (StateMachineVariable<Transform> owner, StateMachineVariable<Vector2> scale) {
            this.owner = owner;
            this.scale = scale;
        }
        public override void OnEnter() {
            owner.GetValue().Scale = scale;
        }

        public static SetScale Factory(Transform owner, Vector2 scale,
            string ownerVariableName = "", string scaleVariableName = "", FSMComponent fsm = null) {

            var ownerVar = string.IsNullOrEmpty(ownerVariableName) ?
                new StateMachineVariable<Transform>(owner) :
                new StateMachineVariable<Transform>(fsm, ownerVariableName);
            var scaleVar = string.IsNullOrEmpty(scaleVariableName) ?
                new StateMachineVariable<Vector2>(scale) :
                new StateMachineVariable<Vector2>(fsm, scaleVariableName);

            return new SetScale(ownerVar, scaleVar);
        }

    }
}
