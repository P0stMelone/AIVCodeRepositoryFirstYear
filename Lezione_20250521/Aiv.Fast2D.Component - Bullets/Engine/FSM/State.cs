namespace Aiv.Fast2D.Component {
    public class State : ExecutableNode {

        protected StateMachine owner;
        protected Transition[] transitions;

        private Action[] actions;

        public void Init (Action[] actions) {
            this.actions = actions;
        }

        public void Init (Transition[] transitions) {
            this.transitions = transitions;
        }

        public virtual void SetOwner (StateMachine stateMachine) {
            this.owner = stateMachine;
        }

        public override void OnEnter () {
            foreach(Action action in actions) {
                action.OnEnter();
            }
            foreach(Transition transition in transitions) {
                transition.OnEnter();
            }
        }

        public override void OnExit () {
            foreach(Action action in actions) {
                action.OnExit();
            }
            foreach (Transition transition in transitions) {
                transition.OnExit();
            }
        }

        public virtual void OnUpdate () {
            foreach(Action action in actions) {
                action.OnUpdate();
            }
        }

        public virtual void OnLateUpdate() {
            foreach (Action action in actions) {
                action.OnLateUpdate();
            }
        }

        public virtual void OnCollide (Collision collisionInfo) {
            foreach(Action action in actions) {
                action.OnCollide(collisionInfo);
            }
        }

        public virtual void CheckTranstition() {
            foreach(Transition transtion in transitions) {
                if (!transtion.Validate()) continue;
                owner.SwapState(transtion.ToState);
                return;
            }
        }
    }
}
