using OpenTK;

namespace Aiv.Fast2D.Component {

    public static class FSMTemplate {

        public const string patrolTargetName = "PatrolTarget";
        public const string targetName = "Target";
        public const float enemyMaxDistance = 7f;
        public const float enemyViewAngle = 120f;

        public static State CreateRandomizePositionState (FSMComponent fsm) {
            Vector2 xBound = new Vector2(1, Game.Win.OrthoWidth - 1);
            Vector2 yBound = new Vector2(1, Game.Win.OrthoHeight - 1);
            RandomizeTransformPosition action = RandomizeTransformPosition.Factory
                (null, xBound, yBound, patrolTargetName, fsm);
            State randomizePositionState = new State();
            randomizePositionState.Init(new Action[] { action });
            return randomizePositionState;
        }

        public static State CreateFollowTargetState (Rigidbody rb, string targetName, FSMComponent fsm, float speed,
            bool everyFrame) {
            FollowTarget action = FollowTarget.Factory(rb, null, speed, 0, everyFrame, targetVariableName: targetName, fsm: fsm);
            State followTargetState = new State();
            followTargetState.Init(new Action[] { action });
            return followTargetState;
        }

        public static StateMachine CreateAttackStateMachine (ShootModule sm, FSMComponent fsm) {
            State shootState = ShootState(sm);
            State waitingState = WaitToShootState(sm.gameObject, fsm);
            Transition s2w = S2W(shootState, waitingState);
            Transition w2s = W2S(waitingState, shootState, 2);
            shootState.Init(new Transition[] { s2w });
            waitingState.Init(new Transition[] { w2s });
            StateMachine subStateMachineAttack = new StateMachine();
            subStateMachineAttack.Init(new State[] { shootState, waitingState }, shootState);
            subStateMachineAttack.Init(new Transition[0]);
            return subStateMachineAttack;
        }




        public static Transition FromRP2FT (State rp, State ft) {
            Transition transition = new Transition();
            transition.SetUpMe(rp, ft, new Condition[0]);
            return transition;
        }

        public static Transition FromFT2RP (Transform from, State ft, State rp, FSMComponent fsm) {
            Condition checkDistance = CheckDistance.Factory(from, null, 0.25f, ComparerType.LessOrEqual, string.Empty,
                toVar: patrolTargetName, fsm: fsm);
            Transition transition = new Transition();
            transition.SetUpMe(ft, rp, new Condition[] { checkDistance });
            return transition;
        }

        public static Transition FromPatrol2FT (Transform from, State patrol, State ft, FSMComponent fsm) {
            Condition checkDistance = CheckDistance.Factory(from, null, enemyMaxDistance, ComparerType.LessOrEqual, string.Empty,
                toVar: targetName, fsm: fsm);
            Condition inLineOfSight = InLineOfSight.Factory(from, null, 60, true, targetName: targetName, fsm: fsm);
            Transition patrol2FT = new Transition();
            patrol2FT.SetUpMe(patrol, ft, new Condition[] { checkDistance, inLineOfSight });
            return patrol2FT;
        }

        public static Transition[] FTStateTransitions (Transform from, State ft, State patrol, FSMComponent fsm, State at) {
            Condition checkDistance = CheckDistance.Factory(from, null, enemyMaxDistance + 2, ComparerType.Greater, string.Empty,
                toVar: targetName, fsm: fsm);
            Transition cdT = new Transition();
            cdT.SetUpMe(ft, patrol, new Condition[] { checkDistance });
            Condition inLineOfSight = InLineOfSight.Factory(from, null, enemyViewAngle, false, targetName: targetName, fsm: fsm);
            Transition lsitT = new Transition();
            lsitT.SetUpMe(ft, patrol, new Condition[] { inLineOfSight });

            checkDistance = CheckDistance.Factory(from, null, 4f, ComparerType.LessOrEqual, string.Empty,
                toVar: targetName, fsm: fsm);
            Transition ft2At = new Transition();
            ft2At.SetUpMe(ft, at, new Condition[] { checkDistance });
            return new Transition[] { cdT, lsitT, ft2At };
        }

        public static Transition[] AttackState2FT (Transform from, State attack, State ft, FSMComponent fsm) {
            Condition checkDistance = CheckDistance.Factory(from, null, 5f, ComparerType.GreaterOrEqual, toVar: targetName, fsm: fsm);
            Transition a2ft = new Transition();
            a2ft.SetUpMe(attack, ft, new Condition[] { checkDistance });
            return new Transition[] { a2ft };
        }

        public static StateMachine PatrolSM (GameObject owner) {
            FSMComponent fsm = owner.GetComponent<FSMComponent>();
            Rigidbody rb = owner.GetComponent<Rigidbody>();
            State rp = CreateRandomizePositionState(fsm);
            State ft = CreateFollowTargetState(rb, patrolTargetName, fsm, 2, false);
            Transition rp2ft = FromRP2FT(rp, ft);
            Transition ft2rp = FromFT2RP(owner.transform, ft, rp, fsm);
            rp.Init(new Transition[] { rp2ft });
            ft.Init(new Transition[] { ft2rp });
            StateMachine patrolSM = new StateMachine();
            patrolSM.Init(new State[] { rp, ft }, rp);
            return patrolSM;
        }

        private static State ShootState (ShootModule sm) {
            State shootState = new State();
            Action shootAction = ShootAction.Factory(sm);
            Action setVelocityAction = SetVelocity.Factory(sm.GetComponent<Rigidbody>(), Vector2.Zero, false);
            shootState.Init(new Action[] { shootAction, setVelocityAction } );
            return shootState;
        }

        private static State WaitToShootState (GameObject enemy, FSMComponent fsm) {
            State waitingToShoot = new State();
            Action rotateToPlayer = RotateToTarget.Factory(enemy.transform, null, string.Empty, targetName, fsm, true);
            waitingToShoot.Init(new Action[] { rotateToPlayer });
            return waitingToShoot;
        }

        private static Transition S2W (State fromState, State toState) {
            Transition S2W = new Transition();
            S2W.SetUpMe(fromState, toState, new Condition[0]);
            return S2W;
        }

        private static Transition W2S (State fromState, State toState, float waitingTime) {
            Transition W2S = new Transition();
            Condition waitingTimeCondition = ExitTime.Factory(waitingTime, false);
            W2S.SetUpMe(fromState, toState, new Condition[] { waitingTimeCondition });
            return W2S;
        }

    }
}
