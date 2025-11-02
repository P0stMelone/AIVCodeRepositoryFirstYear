namespace Aiv.Fast2D.Component {
    public class StateMachineVariable<T> {

        private FSMComponent fsmComponent;
        private T value;
        private string variableName;

        public StateMachineVariable (T value) {
            this.value = value;
        }

        public StateMachineVariable (FSMComponent fsmComponent, string variableName) {
            this.fsmComponent = fsmComponent;
            this.variableName = variableName;
        }

        public T GetValue () {
            if (!string.IsNullOrEmpty(variableName)) {
                return fsmComponent.GetVariable<T>(variableName);
            }
            return value;
        }

        public void SetValue (T value) {
            if (!string.IsNullOrEmpty(variableName)) {
                fsmComponent.SetVariable(variableName, value);
            }
            this.value = value;
        }

        public static implicit operator T (StateMachineVariable<T> variable) => variable.value;


        public static StateMachineVariable<T> StateMachineVariableFactory (T value, string valueName = "", FSMComponent fsm = null) {
            if (string.IsNullOrEmpty(valueName)) {
                return new StateMachineVariable<T>(value);
            } else {
                return new StateMachineVariable<T>(fsm, valueName);
            }
        }
    }
}
