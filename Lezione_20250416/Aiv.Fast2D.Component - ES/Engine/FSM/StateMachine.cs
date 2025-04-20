namespace Aiv.Fast2D.Component {
    public class StateMachine : State {

        private State[] states;
        private State firstState;

        private State activeState;


        public void Init(State[] states, State firstState) {
            this.states = states;
            this.firstState = firstState;
            this.transitions = new Transition[0];
        }

        public override void SetOwner(StateMachine stateMachine) {
            base.SetOwner(stateMachine);
            foreach(State state in states) {
                state.SetOwner(this);
            }
        }

        public override void OnEnter () {
            SwapState(firstState);
            foreach(Transition t in transitions) {
                t.OnEnter();
            }
        }

        public override void OnExit() {
            if (activeState == null) return;
            activeState.OnExit();
            activeState = null;
        }

        public override void OnUpdate() {
            if (activeState == null) return;
            activeState.OnUpdate();
        }

        public override void OnLateUpdate() {
            if (activeState == null) return;
            activeState.OnLateUpdate();
        }

        public override void OnCollide(GameObject other) {
            if (activeState == null) return;
            activeState.OnCollide(other);
        }


        public void SwapState (State newState) {
            if (activeState != null) {
                activeState.OnExit();
            }
            activeState = newState;
            activeState.OnEnter();
        }

        public override void CheckTranstition() {
            if (activeState != null) {
                activeState.CheckTranstition();
            }
            foreach(Transition transition in transitions) {
                if (transition.Validate()) {
                    owner.SwapState(transition.ToState);
                    return;
                }
            }
        }

    }
}
