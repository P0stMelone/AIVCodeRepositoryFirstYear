using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayScene : Scene {
        public PlayScene() : base("Game/Assets/") {
        }

        protected override void LoadAssets() {
            AddTexture("Background", "hex_grid_green.png");
            AddTexture("P1", "player_1.png");
            AddTexture("P2", "player_2.png");
            AddTexture("E1", "enemy_0.png");
            AddTexture("Fireball", "fireball.png");
        }

        public override void InitializeScene() {
            base.InitializeScene();
            CreateBackground();
            CreateP1();
            //CreateP2();
            CreateE1();
            CreateBulletMgr();
        }

        private void CreateBackground () {
            GameObject go = GameObject.CreateGameObject("Background", Vector2.Zero);
            go.AddComponent(new SpriteRenderer(go, "Background", Vector2.Zero, DrawLayer.Background));
        }

        private void CreateP1 () {
            GameObject player = GameObject.CreateGameObject("Player_1", new Vector2(4, 4));
            player.Tag = "Player";
            player.Layer = (uint)Layer.Player;
            player.AddComponent(new SpriteRenderer(player, "P1", Vector2.One * 0.5f, DrawLayer.Playground));
            player.AddComponent<Rigidbody>().Friction = 15;
            player.AddComponent(ColliderFactory.CreateCircleFor(player));
            player.AddComponent(new PlayerController(player, "P1_Horizontal", "P1_Vertical", 6, "P1_Shoot", 100));
            player.AddComponent(new FaceVelocity(player, 10));
            player.AddComponent<ShootModule>();
        }

        private void CreateP2 () {
            GameObject player = GameObject.CreateGameObject("Player_2", new Vector2(4, 4));
            player.Tag = "Player";
            player.Layer = (uint)Layer.Player;
            player.AddComponent(new SpriteRenderer(player, "P2", Vector2.One * 0.5f, DrawLayer.Playground));
            player.AddComponent<Rigidbody>().Friction = 15;
            player.AddComponent(ColliderFactory.CreateCircleFor(player));
            player.AddComponent(new PlayerController(player, "P2_Horizontal", "P2_Vertical", 6, "P2_Shoot", 100));
            player.AddComponent(new FaceVelocity(player, 10));
            player.AddComponent<ShootModule>();
        }

        private void CreateE1 () {
            GameObject p1 = GameObject.Find("Player_1");
            GameObject patrolTarget_E1 = GameObject.CreateGameObject("PT_E1", Vector2.Zero);
            GameObject enemy = GameObject.CreateGameObject("E1", new Vector2(Game.Win.OrthoWidth / 2, Game.Win.OrthoHeight / 2));
            enemy.Layer = (uint)Layer.Enemy;
            enemy.AddComponent(new SpriteRenderer(enemy, "E1", Vector2.One * 0.5f, DrawLayer.Playground));
            enemy.AddComponent<Rigidbody>();
            enemy.AddComponent(new FaceVelocity(enemy, 20));
            enemy.AddComponent<ShootModule>();
            enemy.AddComponent<Enemy>(100);
            enemy.AddComponent(ColliderFactory.CreateCircleFor(enemy));
            FSMComponent fsm = enemy.AddComponent<FSMComponent>();
            StateMachine sm = new StateMachine();
            StateMachine patrol = FSMTemplate.PatrolSM(enemy);
            StateMachine attackSM = FSMTemplate.CreateAttackStateMachine(enemy.GetComponent<ShootModule>(), fsm);

            State ft = FSMTemplate.CreateFollowTargetState(enemy.GetComponent<Rigidbody>(), FSMTemplate.targetName,
                fsm, 3, true);
            Transition patro2FT = FSMTemplate.FromPatrol2FT(enemy.transform, patrol, ft, fsm);
            patrol.Init(new Transition[] { patro2FT });
            ft.Init(FSMTemplate.FTStateTransitions(enemy.transform, ft, patrol, fsm, attackSM));
            attackSM.Init(FSMTemplate.AttackState2FT(enemy.transform, attackSM, ft, fsm));

            sm.Init(new State[] { patrol, ft, attackSM }, patrol);
            fsm.SetVariable(FSMTemplate.patrolTargetName, patrolTarget_E1.transform);
            fsm.SetVariable(FSMTemplate.targetName, p1.transform);
            fsm.Init(sm);
        }

        private void CreateBulletMgr() {
            GameObject go = GameObject.CreateGameObject("BulletMgr", Vector2.Zero);
            go.AddComponent(new BulletMgr(go, 20));
        }

    }
}
