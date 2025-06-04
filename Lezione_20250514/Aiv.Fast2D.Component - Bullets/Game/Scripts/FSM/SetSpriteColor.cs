using OpenTK;

namespace Aiv.Fast2D.Component {
    public class SetSpriteColor : Action {

        private StateMachineVariable<SpriteRenderer> sr;
        private StateMachineVariable<Vector4> color;


        public SetSpriteColor (StateMachineVariable<SpriteRenderer> sr, StateMachineVariable<Vector4> color) {
            this.sr = sr;
            this.color = color;
        }

        public override void OnEnter() {
            sr.GetValue().Sprite.SetMultiplyTint(color);
        }

    }
}
