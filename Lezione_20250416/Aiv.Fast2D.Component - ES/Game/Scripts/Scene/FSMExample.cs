using OpenTK;

namespace Aiv.Fast2D.Component {
    public class FSMExample : Scene {

        public FSMExample() : base("Game/Assets/") {

        }

        protected override void LoadAssets() {
            AddTexture("Enemy", "powerUp_battery.png");
        }


        public override void InitializeScene() {
            base.InitializeScene();
            GameObject enemy = CreateEnemy();
            AddFSM(enemy);
        }


        private GameObject CreateEnemy () {
            GameObject enemy = GameObject.CreateGameObject("Enemy", new OpenTK.Vector2(50, Game.Win.Height - 50));
            Rigidbody rb = enemy.AddComponent<Rigidbody>();
            rb.IsGravityAffected = true;
            enemy.AddComponent(new SpriteRenderer(enemy, "Enemy", OpenTK.Vector2.One * 0.5f, DrawLayer.Playground));
            enemy.AddComponent(new FakeGround(enemy, Game.Win.Height - 50));
            return enemy;
        }

        private void AddFSM(GameObject enemy) {
            FSMComponent fsmComponent = new FSMComponent(enemy);
            enemy.AddComponent(fsmComponent);
            fsmComponent.SetVariable("MoveVelocity", Vector2.UnitX * 20);
            State moveState = CreateMoveState(enemy.GetComponent<Rigidbody>(), "MoveVelocity", fsmComponent);
            State jumpState = CreateJumpState(enemy.GetComponent<Rigidbody>(), -200, fsmComponent);
            State logState = CreateLogState(enemy.GetComponent<Rigidbody>(), "Loggo qualcosa", false);
            StateMachine moveStateComplex = new StateMachine();
            moveStateComplex.Init(new State[] { moveState, logState }, moveState);
            moveStateComplex.Init(new Transition[] { OnlyExitTimeTransition(moveStateComplex, jumpState, 3) });
            jumpState.Init(new Transition[] { TransitionFromJumpToMove(moveStateComplex,
                jumpState, enemy.GetComponent<FakeGround>()) });
            moveState.Init(new Transition[] { OnlyExitTimeTransition(moveState, logState, 1) });
            logState.Init(new Transition[] { OnlyExitTimeTransition(logState, moveState, 0.5f) });
            StateMachine stateMachine = new StateMachine();
            stateMachine.Init(new State[] { moveStateComplex, jumpState }, moveStateComplex);
            fsmComponent.Init(stateMachine);
        }

        private State CreateMoveState(Rigidbody rb, string velocityVariable, FSMComponent owner) {
            State moveState = new State();
            Action setVelocity = new SetVelocity(rb, new StateMachineVariable<Vector2> (owner, velocityVariable), false);
            moveState.Init(new Action[] { setVelocity });
            return moveState;
        }

        private State CreateJumpState(Rigidbody rb, float jumpForce, FSMComponent owner) {
            State jumpState = new State();
            Action setVelocity = new SetVelocity(rb, new StateMachineVariable<Vector2>(Vector2.Zero), false);
            Action jumpForceAction = new JumpAction(rb, new StateMachineVariable<float>(jumpForce));
            Action setVariable = new SetVariableAction<Vector2>(owner, "MoveVelocity", 
                new StateMachineVariable<Vector2>(Vector2.UnitX * 60));
            jumpState.Init(new Action[] { setVelocity, jumpForceAction, setVariable });
            return jumpState;
        }

        private State CreateLogState (Rigidbody rb, string textToLog, bool everyFrame) {
            State logState = new State();
            Action logAction = new LogAction(textToLog, everyFrame);
            Action zeroVelocity = new SetVelocity(rb, new StateMachineVariable<Vector2>(Vector2.Zero), false);
            logState.Init(new Action[] { logAction });
            return logState;
        }

        private Transition OnlyExitTimeTransition (State fromState, State toState, float waitingTime) {
            Transition transition = new Transition();
            Condition waitForTime = new ExitTime(new StateMachineVariable<float>(waitingTime));
            transition.SetUpMe(fromState, toState, new Condition[] { waitForTime });
            return transition;
        }

        private Transition TransitionFromJumpToMove(State moveState, State jumpState, FakeGround enemyFakeGround) {
            Transition transition = new Transition();
            Condition isGroundCondition = new CheckIsGround(enemyFakeGround);
            transition.SetUpMe(jumpState, moveState, new Condition[] { isGroundCondition });
            return transition;
        }
    }
}
