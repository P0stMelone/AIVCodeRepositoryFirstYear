
namespace Aiv.Fast2D.Component {
    public class SetColliderOffsetFromSR : Action {

        private StateMachineVariable<Collider> collider;
        private StateMachineVariable<SpriteRenderer> sr;

        public SetColliderOffsetFromSR (StateMachineVariable<Collider> collider, StateMachineVariable<SpriteRenderer> sr) {
            this.collider = collider;
            this.sr = sr;
        }

        public override void OnEnter() {
            collider.GetValue().Offset = sr.GetValue().CenterOffset;
        }

    }
}
