using OpenTK;

namespace Aiv.Fast2D.Component {
    public class SetSpriteColor : Action {

        private StateMachineVariable<SpriteRenderer> sr;
        private StateMachineVariable<Vector4> color;


        private SetSpriteColor (StateMachineVariable<SpriteRenderer> sr, StateMachineVariable<Vector4> color) {
            this.sr = sr;
            this.color = color;
        }

        public override void OnEnter() {
            sr.GetValue().Sprite.SetMultiplyTint(color);
        }

        public static SetSpriteColor Factory (SpriteRenderer sr, Vector4 color, 
            string srVariableName = "", string colorVariableName = "", FSMComponent fsm = null) {
            StateMachineVariable<SpriteRenderer> srVariable = string.IsNullOrEmpty(srVariableName) ?
                new StateMachineVariable<SpriteRenderer>(sr) :
                new StateMachineVariable<SpriteRenderer>(fsm, srVariableName);
            StateMachineVariable<Vector4> colorVariable = string.IsNullOrEmpty(colorVariableName) ?
                new StateMachineVariable<Vector4>(color) :
                new StateMachineVariable<Vector4>(fsm, colorVariableName);
            return new SetSpriteColor(srVariable, colorVariable);
        }
        
    }
}
