using OpenTK;

namespace Aiv.Fast2D.Component {
    public class SetScale : Action {

        private StateMachineVariable<Transform> owner;
        private StateMachineVariable<Vector2> scale;


        public SetScale (StateMachineVariable<Transform> owner, StateMachineVariable<Vector2> scale) {
            this.owner = owner;
            this.scale = scale;
        }
        public override void OnEnter() {
            owner.GetValue().Scale = scale;
        }

    }
}
