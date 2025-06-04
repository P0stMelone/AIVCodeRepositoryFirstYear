using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component {
    public class SetVariableAction<T> : Action {

        private FSMComponent fsmComponent;
        private string name;
        private StateMachineVariable<T> value;

        public SetVariableAction(FSMComponent fsmComponent, string name, StateMachineVariable<T> value) {
            this.fsmComponent = fsmComponent;
            this.name = name;
            this.value = value;
        }

        public override void OnEnter() {
            fsmComponent.SetVariable(name, value.GetValue());
        }

    }
}
