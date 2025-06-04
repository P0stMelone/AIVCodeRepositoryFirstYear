namespace Aiv.Fast2D.Component {
    public class Transition {

        private State fromState;
        public State FromState {
            get { return fromState; }
        }
        private State toState;
        public State ToState {
            get { return toState; }
        }

        private Condition[] conditions;


        public void SetUpMe (State fromState, State toState, Condition[] conditions) {
            this.fromState = fromState;
            this.toState = toState;
            this.conditions = conditions;
        }

        public void OnEnter () {
            foreach(Condition cond in conditions) {
                cond.OnEnter();
            }
        }

        public void OnExit () {
            foreach(Condition cond in conditions) {
                cond.OnExit();
            }
        }

        public bool Validate () {
            foreach(Condition cond in conditions) {
                if (!cond.Validate()) return false;
            }
            return true;
        }

    }
}
