using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayScene : Scene {
        public PlayScene() : base("Game/Assets/") {
        }

        protected override void LoadAssets() {
            AddTexture("Player", "player.png");
            AddTexture("Link", "Link_Sheet.png");
            AddTexture("Bg_0", "bg_0.png");
            AddTexture("Bg_1", "bg_1.png");
            AddTexture("Bg_2", "bg_2.png");
            AddTexture("Bg_3", "bg_3.png");
            AddTexture("Sky", "sky.png");
            AddTexture("TileGrass", "earthGrass.png");
            AddTexture("TileStone", "stone.png");
            AddTexture("GreenGlobe", "greenGlobe.png");
        }

        public override void InitializeScene() {
            base.InitializeScene();
            CreatePlayer();
            //CreateLink();
            CreateBackground();
            CreateTile();
            CreateBulletMgr();
            CreateWhiteEnemy();
        }

        private void CreatePlayer () {
            GameObject player = GameObject.CreateGameObject("Player", new Vector2(Game.Win.OrthoWidth * 0.5f, 
                Game.Win.OrthoHeight * 0.5f));
            player.AddComponent(new SpriteRenderer(player, "Player", new Vector2 (0.5f, 1), DrawLayer.Playground,
                58,58, Vector2.Zero));
            //player.transform.Scale = Vector2.One * 2f;
            player.Layer = (uint)Layer.Player;
            player.AddCollisionLayer((uint)Layer.Tile);
            Sheet playerSheet = new Sheet(GfxMgr.GetTexture("Player"), 2, 1);
            SheetClip idle = new SheetClip(playerSheet, "Idle", new int[] { 0 }, true, 1);
            SheetClip shooting = new SheetClip(playerSheet, "Shooting", new int[] { 1 }, false, 5, "Idle");
            player.AddComponent(new SheetAnimator(player, player.GetComponent<SpriteRenderer>()));
            SheetAnimator playerAnimator = player.GetComponent<SheetAnimator>();
            playerAnimator.AddClip(idle);
            playerAnimator.AddClip(shooting);
            player.AddComponent<Rigidbody>().IsGravityAffected = true;
            player.AddComponent(new PlayerController(player, "Horizontal", "Jump", "Shoot", 5, -7, 1));
            player.AddComponent(new CollisionDetector(player, new string[] { "Tile" }));
            player.AddComponent(ColliderFactory.CreateBoxFor(player));
            player.AddComponent(new ShootModule(player, BulletType.GreenGlobe));
            CameraMgr.Init(player.transform.Position, new Vector2(Game.Win.OrthoWidth * 0.5f, Game.Win.OrthoHeight * 0.5f));
            CameraMgr.target = player.transform;
            CameraMgr.SetCameraLimits(Game.Win.OrthoWidth * 0.5f, Game.Win.OrthoWidth * 1.5f, Game.Win.OrthoHeight * 0.5f,
                Game.Win.OrthoHeight * 0.5f);
        }
        
        private void CreateLink () {
            GameObject link = GameObject.CreateGameObject("Link", new Vector2(Game.Win.OrthoWidth * 0.5f,
                Game.Win.OrthoHeight * 0.5f));
            link.AddComponent(new SpriteRenderer(link, "Link", Vector2.Zero, DrawLayer.Playground, 64, 64, Vector2.Zero));
            Sheet linkSheet = new Sheet(GfxMgr.GetTexture("Link"), 8, 8);
            SheetClip run = new SheetClip(linkSheet, "Run", new int[] { 0, 1, 2, 3, 4, 5, 6 }, true, 10);
            SheetClip die = new SheetClip(linkSheet, "Die", new int[] { 18, 18, 19, 20, 21, 22, 23 }, false, 10);
            SheetClip attack = new SheetClip(linkSheet, "Attack", new int[] { 38, 39, 40 }, false, 10, "Run");
            SheetAnimator animator = new SheetAnimator(link, link.GetComponent<SpriteRenderer>());
            link.AddComponent(animator);
            animator.AddClip(run);
            animator.AddClip(die);
            animator.AddClip(attack);
            link.AddComponent<LinkController>();
        }

        private void CreateBackground () {
            CameraMgr.AddCameras("GUI", new Camera());
            CameraMgr.AddCameras("Sky", speed: 0.02f);
            CameraMgr.AddCameras("Bg_0", speed: 0.15f);
            CameraMgr.AddCameras("Bg_1", speed: 0.2f);
            CameraMgr.AddCameras("Bg_2", speed: 0.9f);
            GameObject sky = GameObject.CreateGameObject("Sky", Vector2.Zero);
            sky.AddComponent(new SpriteRenderer(sky, "Sky", Vector2.Zero, DrawLayer.Background));
            sky.GetComponent<SpriteRenderer>().Camera = CameraMgr.GetCamera("Sky");
            GameObject clonedSky = GameObject.Clone(sky);
            clonedSky.transform.Position += Vector2.UnitX * Game.PixelsToUnit(GfxMgr.GetTexture("Sky").Width);
            GameObject temp;
            for (int i = 0; i < 4; i++) {
                temp = GameObject.CreateGameObject("Bg_" + i, Vector2.Zero);
                temp.AddComponent(new SpriteRenderer(temp, "Bg_" + i, Vector2.Zero, DrawLayer.Background));
                temp.GetComponent<SpriteRenderer>().Camera = CameraMgr.GetCamera("Bg_" + i);
                if (i >= 2) {
                    temp.transform.Position += Vector2.UnitY * Game.PixelsToUnit(80);
                }
                temp = GameObject.Clone(temp);
                temp.transform.Position += Vector2.UnitX * Game.PixelsToUnit(GfxMgr.GetTexture("Bg_" + i).Width);
            }
        }

        private void CreateTile () {
            GameObject tile;
            Texture texture = GfxMgr.GetTexture("TileStone");
            for (int i = 0; i < 4; i++) {
                tile = GameObject.CreateGameObject("Rock_Tile_" + i, new Vector2(10 + i * Game.PixelsToUnit(texture.Width), 8));
                tile.Tag = "Tile";
                tile.Layer = (uint)Layer.Tile;
                tile.AddComponent(new SpriteRenderer(tile, "TileStone", Vector2.Zero, DrawLayer.Middleground));
                tile.AddComponent(ColliderFactory.CreateBoxFor(tile));
                tile.AddComponent<Rigidbody>();
            }
            texture = GfxMgr.GetTexture("TileGrass");
            for (int i = 0; i < 4; i++) {
                tile = GameObject.CreateGameObject("Rock_Tile_" + i, new Vector2(16 + i * Game.PixelsToUnit(texture.Width), 6));
                tile.Tag = "Tile";
                tile.Layer = (uint)Layer.Tile;
                tile.AddComponent(new SpriteRenderer(tile, "TileGrass", Vector2.Zero, DrawLayer.Middleground));
                tile.AddComponent(ColliderFactory.CreateBoxFor(tile));
                tile.AddComponent<Rigidbody>();
            }
            for (int i = 0; i < 50; i++) {
                tile = GameObject.CreateGameObject("Rock_Tile_" + i, new Vector2(0 + i * Game.PixelsToUnit(texture.Width), 
                    Game.Win.OrthoHeight));
                tile.Tag = "Tile";
                tile.Layer = (uint)Layer.Tile;
                tile.AddComponent(new SpriteRenderer(tile, "TileGrass", Vector2.Zero, DrawLayer.Middleground));
                tile.AddComponent(ColliderFactory.CreateBoxFor(tile));
                tile.AddComponent<Rigidbody>();
            }
        }

        private void CreateBulletMgr () {
            GameObject bulletMgr = GameObject.CreateGameObject("BulletMgr", Vector2.Zero);
            bulletMgr.AddComponent<BulletMgr>(20).Init();
        }


        private void CreateWhiteEnemy () {
            GameObject player = GameObject.Find("Player");
            GameObject enemy = GameObject.CreateGameObject("WhiteEnemy_Test", 
                new Vector2(Game.Win.OrthoWidth * 0.75f, Game.Win.OrthoHeight * 0f));
            enemy.Tag = "Enemy";
            enemy.Layer = (uint)Layer.Enemy;
            enemy.AddCollisionLayer((uint)Layer.Tile);
            enemy.AddCollisionLayer((uint)Layer.Enemy);
            enemy.AddComponent(new SpriteRenderer(enemy, "Player", new Vector2(0.5f, 1f), DrawLayer.Playground,
                58, 58, Vector2.Zero));
            enemy.AddComponent(ColliderFactory.CreateBoxFor(enemy));
            enemy.AddComponent<Rigidbody>().IsGravityAffected = true;
            enemy.AddComponent(new CollisionDetector(enemy, new string[] { "Tile", "Enemy" }));
            FSMComponent fsmComponent = enemy.AddComponent<FSMComponent>();
            State idleState = IdleState(enemy);
            State followTarget = FollowTarget(enemy, player, 3);
            State crushOnPlayer = CrushOnPlayer(enemy, player, 5);
            Transition i2ft = IdleToFollowTarget(idleState, followTarget, enemy,player, -0.25f);
            Transition ft2i = FT2I(followTarget, idleState, enemy, player, 0.25f);
            Transition ft2cp = FT2CP(followTarget, crushOnPlayer, enemy, player, 5);
            idleState.Init(new Transition[] { i2ft });
            followTarget.Init(new Transition[] { ft2i });
            crushOnPlayer.Init(new Transition[] { ft2cp });
            StateMachine sm = new StateMachine();
            sm.Init(new State[] { idleState, followTarget, crushOnPlayer }, idleState);
            sm.Init(new Transition[0]);
            fsmComponent.Init(sm);
        }

        private State IdleState (GameObject owner) {
            State idle = new State();
            Action setVelocity = new SetVelocity(
                new StateMachineVariable<Rigidbody>(owner.GetComponent<Rigidbody>()), 
                new StateMachineVariable<Vector2>(Vector2.Zero), 
                false);
            idle.Init(new Action[] { setVelocity });
            return idle;
        }

        private State FollowTarget (GameObject owner, GameObject target, float speed) {
            State followTarget = new State();
            Action followTargetAction = new FollowTarget(
                new StateMachineVariable<Rigidbody>(owner.GetComponent<Rigidbody>()),
                new StateMachineVariable<Transform>(target.transform),
                new StateMachineVariable<float>(speed),
                new StateMachineVariable<float>(0.5f),
                true);
            followTarget.Init(new Action[] { followTargetAction });
            return followTarget;
        }

        private State CrushOnPlayer (GameObject owner, GameObject target, float speed) {
            State crushOnPlayer = new State();
            Action followTargetAction = new FollowTarget(
                new StateMachineVariable<Rigidbody>(owner.GetComponent<Rigidbody>()),
                new StateMachineVariable<Transform>(target.transform),
                new StateMachineVariable<float>(speed),
                new StateMachineVariable<float>(0.5f),
                true);
            Action setSpriteColor = new SetSpriteColor(
                new StateMachineVariable<SpriteRenderer>(owner.GetComponent<SpriteRenderer>()), 
                new StateMachineVariable<Vector4>(new Vector4(1, 0, 0, 1)));
            Action setScale = new SetScale(new StateMachineVariable<Transform>(owner.transform),
                new StateMachineVariable<Vector2>(new Vector2(1, 1.5f)));
            crushOnPlayer.Init(new Action[] { followTargetAction, setSpriteColor, setScale });
            return crushOnPlayer;
        }

        private Transition IdleToFollowTarget (State idle, State followTarget, GameObject owner, GameObject target, float yOffset) {
            Transition idleToFT = new Transition();
            Condition hg = new CompareYPosition(
                    new StateMachineVariable<Transform>(owner.transform),
                    new StateMachineVariable<Transform>(target.transform),
                    ComparerType.LessOrEqual,
                    new StateMachineVariable<float>(yOffset));
            idleToFT.SetUpMe(idle, followTarget, new Condition[] { hg });
            return idleToFT;
        }

        private Transition FT2I(State followTarget, State idle, GameObject owner, GameObject target, float yOffset) {
            Transition ft2i = new Transition();
            Condition hg = new CompareYPosition(
                    new StateMachineVariable<Transform>(target.transform),
                    new StateMachineVariable<Transform>(owner.transform),
                    ComparerType.LessOrEqual,
                    new StateMachineVariable<float>(yOffset));
            ft2i.SetUpMe(followTarget, idle, new Condition[] { hg });
            return ft2i;
        }

        private Transition FT2CP (State followTarget, State crushOnPlayer, GameObject owner, GameObject target, float distance) {
            Transition ft2cp = new Transition();
            Condition cd = new CheckDistance(
                    new StateMachineVariable<Transform>(owner.transform),
                    new StateMachineVariable<Transform>(target.transform),
                    new StateMachineVariable<float>(distance),
                    ComparerType.LessOrEqual);
            ft2cp.SetUpMe(followTarget, crushOnPlayer, new Condition[] { cd });
            return ft2cp;
        }

    }
}
