
namespace Aiv.Fast2D.Component {
    public class SetColliderOffsetFromSR : Action {

        private StateMachineVariable<Collider> collider;
        private StateMachineVariable<SpriteRenderer> sr;

        private SetColliderOffsetFromSR (StateMachineVariable<Collider> collider, StateMachineVariable<SpriteRenderer> sr) {
            this.collider = collider;
            this.sr = sr;
        }

        public override void OnEnter() {
            collider.GetValue().Offset = sr.GetValue().CenterOffset;
        }

        public static SetColliderOffsetFromSR Factory(Collider collider, SpriteRenderer sr,
                string colliderVariableName = "", string srVariableName = "", FSMComponent fsm = null) {

            var colliderVar = string.IsNullOrEmpty(colliderVariableName) ?
                new StateMachineVariable<Collider>(collider) :
                new StateMachineVariable<Collider>(fsm, colliderVariableName);
            var srVar = string.IsNullOrEmpty(srVariableName) ?
                new StateMachineVariable<SpriteRenderer>(sr) :
                new StateMachineVariable<SpriteRenderer>(fsm, srVariableName);

            return new SetColliderOffsetFromSR(colliderVar, srVar);
        }

    }
}
