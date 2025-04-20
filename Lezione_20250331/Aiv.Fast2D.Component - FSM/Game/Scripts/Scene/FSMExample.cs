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
            State moveState = CreateMoveState(enemy.GetComponent<Rigidbody>(), Vector2.UnitX * 20);
            State jumpState = CreateJumpState(enemy.GetComponent<Rigidbody>(), -200);
            moveState.Init(new Transition[] { TransitionFromMoveToJump(moveState, jumpState, 3) });
            jumpState.Init(new Transition[] { TransitionFromJumpToMove(moveState,
                jumpState, enemy.GetComponent<FakeGround>()) });
            StateMachine fsm = enemy.AddComponent<StateMachine>();
            fsm.Init(new State[] { moveState, jumpState }, moveState);
        }

        private State CreateMoveState(Rigidbody rb, Vector2 velocity) {
            State moveState = new State();
            Action setVelocity = new SetVelocity(rb, velocity, false);
            moveState.Init(new Action[] { setVelocity });
            return moveState;
        }

        private State CreateJumpState(Rigidbody rb, float jumpForce) {
            State jumpState = new State();
            Action setVelocity = new SetVelocity(rb, Vector2.Zero, false);
            Action jumpForceAction = new JumpAction(rb, jumpForce);
            jumpState.Init(new Action[] { setVelocity, jumpForceAction });
            return jumpState;
        }

        private Transition TransitionFromMoveToJump(State moveState, State jumpState, float waitingTime) {
            Transition transition = new Transition();
            Condition waitForTime = new ExitTime(waitingTime);
            transition.SetUpMe(moveState, jumpState, new Condition[] { waitForTime });
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
