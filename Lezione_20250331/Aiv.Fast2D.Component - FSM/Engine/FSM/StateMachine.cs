namespace Aiv.Fast2D.Component {
    public class StateMachine : UserComponent {

        private State[] states;
        private State activeState;

        public StateMachine(GameObject owner): base (owner) {

        }


        public void Init(State[] states, State firstState) {
            this.states = states;
            foreach(State state in this.states) {
                state.Init(this);
            }
            SwapState(firstState);
        }

        public override void Update() {
            if (activeState == null) return;
            activeState.OnUpdate();
        }

        public override void LateUpdate() {
            if (activeState == null) return;
            activeState.OnLateUpdate();
            activeState.CheckTranstition();
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

    }
}
