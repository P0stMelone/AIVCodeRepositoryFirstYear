using OpenTK;

namespace Aiv.Fast2D.Component {
    class FSMPatrolExample : Scene {

        public FSMPatrolExample() : base("Game/Assets/") {

        }

        protected override void LoadAssets() {
            AddTexture("Enemy", "powerUp_battery.png");
            AddTexture("Player", "player_ship.png");
        }


        public override void InitializeScene() {
            base.InitializeScene();
            GameObject enemy = CreateEnemy();
            CreatePlayer();
            CreateFSM(enemy);
        }


        private GameObject CreateEnemy() {
            GameObject enemy = GameObject.CreateGameObject("Enemy", new OpenTK.Vector2(Game.Win.Width / 2, Game.Win.Height / 2));
            Rigidbody rb = enemy.AddComponent<Rigidbody>();
            rb.IsGravityAffected = false;
            enemy.AddComponent(new SpriteRenderer(enemy, "Enemy", OpenTK.Vector2.One * 0.5f, DrawLayer.Playground));
            return enemy;
        }

        private void CreatePlayer () {
            GameObject player = GameObject.CreateGameObject("Player", new Vector2(0, Game.Win.Height - 50));
            player.AddComponent<Rigidbody>();
            player.AddComponent(new PlayerController(player, 500));
            player.AddComponent(new SpriteRenderer(player, "Player", Vector2.Zero, DrawLayer.Playground));
        }

        private void CreateFSM (GameObject enemy) {
            Vector2[] checkpoints = new Vector2[] {
                new Vector2 (50,50),
                new Vector2 (Game.Win.Width - 50, 50),
                new Vector2 (Game.Win.Width - 50, Game.Win.Height -50),
                new Vector2 (50, Game.Win.Height -50)
            };
            FSMComponent fsmComponent = new FSMComponent(enemy);
            enemy.AddComponent(fsmComponent);
            fsmComponent.SetVariable("Target", Vector2.Zero);
            fsmComponent.SetVariable("LastPatrolTarget", checkpoints[0]);
            fsmComponent.SetVariable("Player", FindGameObject("Player").transform);

            StateMachine patrolSM = CreatePatrolSubFSM(fsmComponent, enemy, checkpoints);
            State chaseState = CreateFollowPlayerState(fsmComponent, enemy.GetComponent<Rigidbody>(), 250, "Player", "Target", true);
            State attackState = CreateAttackState(enemy);

            patrolSM.Init(new Transition[] { CheckDistanceTransition(patrolSM, chaseState, 
                fsmComponent, enemy.transform, "Player", Comparer.LessEqual, 200) });
            chaseState.Init(new Transition[] {
                CheckDistanceTransition(chaseState, patrolSM, fsmComponent, enemy.transform, "Player", Comparer.Greater, 300),
                CheckDistanceTransition(chaseState, attackState, fsmComponent, enemy.transform, "Player", Comparer.LessEqual, 100)
            });
            attackState.Init(new Transition[] {
                CheckDistanceTransition(attackState, chaseState, fsmComponent, enemy.transform, "Player", Comparer.Greater, 150),
            });
            StateMachine stateMachine = new StateMachine();
            stateMachine.Init(new State[] { patrolSM, chaseState, attackState }, patrolSM);
            fsmComponent.Init(stateMachine);
        }

        private StateMachine CreatePatrolSubFSM (FSMComponent fsmComponent, GameObject enemy, Vector2[] checkpoints) {
            State setFirstTarget = SetTargetState(fsmComponent, "Target", checkpoints);
            State followTarget = CreateFollowTargetState(fsmComponent, enemy.GetComponent<Rigidbody>(), 100, "Target", false);
            State waitingState = CreateWaitingState(fsmComponent, enemy.GetComponent<Rigidbody>(), "Target", checkpoints);
            Transition fromFirstTargetToFollowTarget = new Transition();
            fromFirstTargetToFollowTarget.SetUpMe(setFirstTarget, followTarget, new Condition[0]);
            Transition fromFollowToWaiting = CheckDistanceTransitionVector2(followTarget, waitingState, fsmComponent, 
                enemy.transform, "Target");
            Transition fromWaitingToFollow = OnlyExitTimeTransition(waitingState, followTarget, 1);
            setFirstTarget.Init(new Transition[] { fromFirstTargetToFollowTarget });
            followTarget.Init(new Transition[] { fromFollowToWaiting });
            waitingState.Init(new Transition[] { fromWaitingToFollow });
            StateMachine patrolSM = new StateMachine();
            patrolSM.Init(new State[] {setFirstTarget, followTarget, waitingState }, setFirstTarget);
            return patrolSM;
        }

        private State CreateFollowTargetState (FSMComponent owner, Rigidbody rb, float speed, string targetVariableName, bool everyFrame) {
            State followTargetState = new State();
            Action followTargetAction = new FollowTarget(rb, new StateMachineVariable<Vector2>(owner, targetVariableName), 
                new StateMachineVariable<float>(speed), everyFrame);
            followTargetState.Init(new Action[] { followTargetAction });
            return followTargetState;
        }

        private State CreateFollowPlayerState (FSMComponent owner, Rigidbody rb, float speed, 
            string playerVariableName, string targetVariableName, bool everyFrame) {
            State followPlayerState = new State();
            Action setLastTargetAsTarget = new SetVariableAction<Vector2>(owner, "LastPatrolTarget", 
                new StateMachineVariable<Vector2>(owner, "Target"));
            Action setPlayerPositionToTarget = new SetTargetFromTransform(new StateMachineVariable<Vector2>(owner, targetVariableName),
                owner.GetVariable<Transform>(playerVariableName).transform, everyFrame);
            Action followTargetAction = new FollowTarget(rb, new StateMachineVariable<Vector2>(owner, targetVariableName),
                new StateMachineVariable<float>(speed), everyFrame);
            followPlayerState.Init(new Action[] { setLastTargetAsTarget, setPlayerPositionToTarget, followTargetAction });
            return followPlayerState;
        }

        private State CreateWaitingState (FSMComponent owner, Rigidbody rb, string targetVariableName, Vector2[] checkpoints) {
            State waitingState = new State();
            Action stopAction = new SetVelocity(rb, new StateMachineVariable<Vector2>(Vector2.Zero), false);

            Action changeTargetAction = new ChangeFSMVariableFromArray<Vector2>(owner, checkpoints, targetVariableName,
                new StateMachineVariable<int>(0));
            waitingState.Init(new Action[] { stopAction, changeTargetAction });
            return waitingState;
        }

        private State SetTargetState (FSMComponent owner, string targetVariableName, Vector2[] checkpoints) {
            State setTargetState = new State();
            Action restoreLastPatrolTarget = new SetVariableAction<Vector2>(owner, "Target", 
                new StateMachineVariable<Vector2>(owner, "LastPatrolTarget")); 
            setTargetState.Init(new Action[] { restoreLastPatrolTarget });
            return setTargetState;
        }

        private Transition OnlyExitTimeTransition(State fromState, State toState, float waitingTime) {
            Transition transition = new Transition();
            Condition waitForTime = new ExitTime(new StateMachineVariable<float>(waitingTime));
            transition.SetUpMe(fromState, toState, new Condition[] { waitForTime });
            return transition;
        }

        private Transition CheckDistanceTransitionVector2 (State fromState, State toState, FSMComponent owner, 
            Transform from, string toVariableName) {
            Transition transition = new Transition();
            Condition checkDistanceVector2 = new CheckDistanceVector2(from, new StateMachineVariable<Vector2>(owner, toVariableName),
                Comparer.LessEqual, new StateMachineVariable<float>(10));
            transition.SetUpMe(fromState, toState, new Condition[] { checkDistanceVector2 });
            return transition;
        }

        private Transition CheckDistanceTransition(State fromState, State toState, FSMComponent owner, 
            Transform from, string toVariableName, Comparer comparer, float distanceValue) {
            Transition transition = new Transition();
            Condition checkDistanceVector2 = new CheckDistance(from, owner.GetVariable<Transform>(toVariableName), comparer,
                new StateMachineVariable<float>(distanceValue));
            transition.SetUpMe(fromState, toState, new Condition[] { checkDistanceVector2 });
            return transition;
        }


        private State CreateAttackState (GameObject enemy) {
            State attackState = new State();
            Action stopAction = new SetVelocity(enemy.GetComponent<Rigidbody>(), new StateMachineVariable<Vector2>(Vector2.Zero), false);
            Action shootAction = new ShootAction(new StateMachineVariable<float>(2));
            attackState.Init(new Action[] { stopAction, shootAction});
            return attackState;
        }
    }
}
