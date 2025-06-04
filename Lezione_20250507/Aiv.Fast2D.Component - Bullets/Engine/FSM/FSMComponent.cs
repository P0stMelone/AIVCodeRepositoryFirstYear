using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public class FSMComponent : UserComponent {


        private Dictionary<string, object> variables;
        private StateMachine stateMachine;


        public void SetVariable (string name, object value) {
            variables[name] = value;
        }

        public T GetVariable<T> (string name)  {
            return (T)variables[name];
        }

        public FSMComponent(GameObject owner) : base(owner) {
            variables = new Dictionary<string, object>();
        }

        public void Init (StateMachine stateMachine) {
            this.stateMachine = stateMachine;
        }

        public override void Start() {
            stateMachine.SetOwner(null);
            stateMachine.OnEnter();
        }

        public override void Update() {
            stateMachine.OnUpdate();
        }

        public override void LateUpdate() {
            stateMachine.OnLateUpdate();
            stateMachine.CheckTranstition();
        }

        public override void OnCollide(Collision collisionInfo) {
            stateMachine.OnCollide(collisionInfo);
        }

    }
   
}
