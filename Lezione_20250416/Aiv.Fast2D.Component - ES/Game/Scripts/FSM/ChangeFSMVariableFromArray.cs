using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component {
    public class ChangeFSMVariableFromArray<T> : Action {

        private FSMComponent owner;
        private T[] possibleValues;
        private StateMachineVariable<int> currentIndex;

        private string variableName;

        public ChangeFSMVariableFromArray(FSMComponent owner, T[] possibleValues, string variableName, StateMachineVariable<int> index) {
            this.owner = owner;
            this.possibleValues = possibleValues;
            this.currentIndex = index;
            this.variableName = variableName;
        }

        public override void OnEnter() {
            currentIndex.SetValue(currentIndex.GetValue() + 1);
            currentIndex.SetValue(currentIndex.GetValue() % possibleValues.Length);
            owner.SetVariable(variableName, possibleValues[currentIndex.GetValue()]);
        }



    }
}
