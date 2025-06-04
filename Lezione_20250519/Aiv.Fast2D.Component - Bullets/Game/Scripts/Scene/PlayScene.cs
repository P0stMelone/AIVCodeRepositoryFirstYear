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
            //CreateWhiteEnemy();
            //CreateRedEnemy();
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
            player.AddComponent(new PlayerController(player, "Horizontal", "Jump", "Shoot", 7, -7, 1));
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

        private void CreateEnemyMgr () {
            GameObject enemyMgr = GameObject.CreateGameObject("EnemyMgr", Vector2.Zero);
            enemyMgr.AddComponent(new EnemyMgr(enemyMgr, 20));
        }

    }
}
