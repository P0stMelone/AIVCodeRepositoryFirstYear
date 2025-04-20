namespace Aiv.Fast2D.Component {
    public class State {

        private StateMachine stateMachine;

        private Action[] actions;
        private Transition[] transitions;

        public void Init (Action[] actions) {
            this.actions = actions;
        }

        public void Init (Transition[] transitions) {
            this.transitions = transitions;
        }

        public void Init (StateMachine stateMachine) {
            this.stateMachine = stateMachine;
        }

        public void OnEnter () {
            foreach(Action action in actions) {
                action.OnEnter();
            }
            foreach(Transition transition in transitions) {
                transition.OnEnter();
            }
        }

        public void OnExit () {
            foreach(Action action in actions) {
                action.OnExit();
            }
            foreach (Transition transition in transitions) {
                transition.OnExit();
            }
        }

        public void OnUpdate () {
            foreach(Action action in actions) {
                action.OnUpdate();
            }
        }

        public void OnLateUpdate() {
            foreach (Action action in actions) {
                action.OnLateUpdate();
            }
        }

        public void OnCollide (GameObject other) {
            foreach(Action action in actions) {
                action.OnCollide(other);
            }
        }

        public void CheckTranstition() {
            foreach(Transition transtion in transitions) {
                if (!transtion.Validate()) continue;
                stateMachine.SwapState(transtion.ToState);
                return;
            }
        }
    }
}
