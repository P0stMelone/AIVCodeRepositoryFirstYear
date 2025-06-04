using OpenTK;

namespace Aiv.Fast2D.Component {
    public static class FSMTemplates {

        public static State CreateIdleState(GameObject owner) {
            var setVelocity = SetVelocity.Factory(owner.GetComponent<Rigidbody>(), Vector2.Zero, false);
            var setSpriteColor = SetSpriteColor.Factory(owner.GetComponent<SpriteRenderer>(), new Vector4(1, 1, 1, 1));
            var setScale = SetScale.Factory(owner.transform, new Vector2(1, 1f));
            var setColliderOffset = SetColliderOffsetFromSR.Factory(owner.GetComponent<Collider>(), owner.GetComponent<SpriteRenderer>());

            State idle = new State();
            idle.Init(new Action[] { setVelocity, setSpriteColor, setScale, setColliderOffset });
            return idle;
        }

        public static State CreateFollowTargetState(GameObject owner, GameObject target, float speed, Vector4 color) {
            var followTargetAction = FollowTarget.Factory(
                owner.GetComponent<Rigidbody>(),
                target.transform,
                speed,
                0.5f,
                true
            );
            var setSpriteColor = SetSpriteColor.Factory(owner.GetComponent<SpriteRenderer>(), color);
            var setScale = SetScale.Factory(owner.transform, new Vector2(1, 1f));
            var setColliderOffset = SetColliderOffsetFromSR.Factory(owner.GetComponent<Collider>(), owner.GetComponent<SpriteRenderer>());

            State followTarget = new State();
            followTarget.Init(new Action[] { followTargetAction, setSpriteColor, setScale, setColliderOffset });
            return followTarget;
        }

        public static State CreateCrushOnPlayerState(GameObject owner, GameObject target, float speed) {
            var followTargetAction = FollowTarget.Factory(
                owner.GetComponent<Rigidbody>(),
                target.transform,
                speed,
                0.5f,
                true
            );
            var setSpriteColor = SetSpriteColor.Factory(owner.GetComponent<SpriteRenderer>(), new Vector4(1, 0, 0, 1));
            var setScale = SetScale.Factory(owner.transform, new Vector2(1, 1.5f));
            var setColliderOffset = SetColliderOffsetFromSR.Factory(owner.GetComponent<Collider>(), owner.GetComponent<SpriteRenderer>());

            State crushOnPlayer = new State();
            crushOnPlayer.Init(new Action[] { followTargetAction, setSpriteColor, setScale, setColliderOffset });
            return crushOnPlayer;
        }

        public static Transition CreateTransitionCompareY(State from, State to, GameObject a, GameObject b, 
            float yOffset, ComparerType comparer) {
            var cond = CompareYPosition.Factory(
                a.transform,
                b.transform,
                yOffset,
                comparer
            );
            Transition t = new Transition();
            t.SetUpMe(from, to, new Condition[] { cond });
            return t;
        }

        public static Transition CreateTransitionDistance(State from, State to, GameObject a, GameObject b, 
            float distance, ComparerType comparer) {
            var cond = CheckDistance.Factory(
                a.transform,
                b.transform,
                distance,
                comparer
            );
            Transition t = new Transition();
            t.SetUpMe(from, to, new Condition[] { cond });
            return t;
        }


        public static StateMachine CreateWhiteEnemyStateMachine(GameObject enemy, GameObject player) {
            var idle = CreateIdleState(enemy);
            var follow = CreateFollowTargetState(enemy, player, 3, new Vector4(1f,1,1,1));
            var crush = CreateCrushOnPlayerState(enemy, player, 5);

            var i2f = CreateTransitionCompareY(idle, follow, enemy, player, -0.25f, ComparerType.LessOrEqual);
            var f2i = CreateTransitionCompareY(follow, idle, player, enemy, 0.25f, ComparerType.LessOrEqual);
            var f2c = CreateTransitionDistance(follow, crush, enemy, player, 5, ComparerType.LessOrEqual);
            var c2i = CreateTransitionCompareY(crush, idle, player, enemy, 0.25f, ComparerType.LessOrEqual);
            var c2f = CreateTransitionDistance(crush, follow, enemy, player, 6, ComparerType.GreaterOrEqual);

            idle.Init(new Transition[] { i2f });
            follow.Init(new Transition[] { f2i, f2c });
            crush.Init(new Transition[] { c2i, c2f });

            var sm = new StateMachine();
            sm.Init(new State[] { idle, follow, crush }, idle);
            sm.Init(new Transition[0]);
            return sm;
        }

        public static StateMachine CreateShootSubState(GameObject owner, GameObject target, float reloadTime) {
            var wait = new State();
            var shoot = new State();

            var waitAction = SetVelocity.Factory(owner.GetComponent<Rigidbody>(), Vector2.Zero, false);

            float halfHeightOwner = owner.GetComponent<SpriteRenderer>().Width * 0.5f * owner.transform.Scale.Y;
            float halfHeightTarget = target.GetComponent<SpriteRenderer>().Width * 0.5f * target.transform.Scale.Y;

            var shootAction = ShootAtTarget.Factory(
                owner.GetComponent<ShootModule>(),
                target.transform,
                new Vector2(0,-halfHeightOwner),
                new Vector2(0, -halfHeightTarget)
            );

            wait.Init(new Action[] { waitAction });
            shoot.Init(new Action[] { shootAction });

            var tWaitToShoot = new Transition();
            var timerCond = ExitTime.Factory(reloadTime, false);
            tWaitToShoot.SetUpMe(wait, shoot, new Condition[] { timerCond });

            var tShootToWait = new Transition();
            var doneCond = ExitTime.Factory(0.1f, false);
            tShootToWait.SetUpMe(shoot, wait, new Condition[] { doneCond });

            wait.Init(new Transition[] { tWaitToShoot });
            shoot.Init(new Transition[] { tShootToWait });

            var shootSM = new StateMachine();
            shootSM.Init(new State[] { wait, shoot }, wait);
            shootSM.Init(new Transition[0]);
            return shootSM;
        }

        public static StateMachine CreateRedEnemyStateMachine(GameObject enemy, GameObject player, float followSpeed, float stopDistance, 
            float reloadTime, float escapeDistance) {
            var follow = CreateFollowTargetState(enemy, player, followSpeed, new Vector4(1,0,0,1f));
            var attackSubSM = CreateShootSubState(enemy, player, reloadTime);

            var f2a = CreateTransitionDistance(follow, attackSubSM, enemy, player, stopDistance, ComparerType.LessOrEqual);
            var a2f = CreateTransitionDistance(attackSubSM, follow, enemy, player, escapeDistance, ComparerType.Greater);

            follow.Init(new Transition[] { f2a });
            attackSubSM.Init(new Transition[] { a2f });

            var sm = new StateMachine();
            sm.Init(new State[] { follow, attackSubSM }, follow);
            sm.Init(new Transition[0]);
            return sm;
        }
    }

}