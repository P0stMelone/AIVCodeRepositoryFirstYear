using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayScene : Scene{

        public PlayScene () : base ("Game/Assets/") {

        }

        protected override void LoadAssets() {
            AddTexture("player", "player_ship.png");
            AddTexture("background", "spaceBg.png");
            AddTexture("blueLaser", "Bluelaser.png");
            AddTexture("greenGlobe", "greenGlobe.png");
            AddTexture("fireGlobe", "fireGlobe.png");
            AddTexture("enemy_0", "enemy_ship.png");
            AddTexture("enemy_1", "redEnemy_ship.png");
            AddTexture("energyPowerUp", "powerUp_battery.png");
            AddTexture("triplePowerUp", "powerUp_triple.png");
            AddTexture("healthBG", "loadingBar_frame.png");
            AddTexture("healthBar", "loadingBar_bar.png");
            FontMgr.AddFont("stdFont", "Game/Assets/textSheet.png", 15, 32, 20, 20);
        }

        public override void InitializeScene() {
            base.InitializeScene();
            CreateBackgroundGO();
            CreatePlayerGO();
            CreateBulletPool();
            CreateEnemyMgr();
            CreatePowerUpsMgr();
            CreateUI();
        }

        private void CreateBackgroundGO () {
            GameObject bg = GameObject.CreateGameObject("Background", Vector2.Zero);
            SpriteRenderer sr = new SpriteRenderer(bg, "background", Vector2.Zero, DrawLayer.Background);
            bg.AddComponent(sr);
        }

        private void CreatePlayerGO () {
            GameObject player = GameObject.CreateGameObject("Player", new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f));
            SpriteRenderer sr = new SpriteRenderer(player, "player", new Vector2(0.5f, 0.5f), DrawLayer.Playground);
            player.AddComponent(sr);
            PlayerController pc = new PlayerController(player, 500, KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D,
                0.5f, KeyCode.Space, 100);
            player.AddComponent(pc);
            KeepInBorder kb = new KeepInBorder(player);
            player.AddComponent(kb);
            player.AddComponent(typeof(Rigidbody));
            player.AddComponent(typeof(ShootModule));
            player.Layer = (uint)Layer.Player;
            player.AddComponent(ColliderFactory.CreateBoxFor(player));
        }

        private void CreateBulletPool () {
            GameObject go = GameObject.CreateGameObject("BulletPool", Vector2.Zero);
            go.AddComponent(new BulletPool(go, 30));

        }

        private void CreateEnemyMgr () {
            GameObject enemyMgr = GameObject.CreateGameObject("EnemyMgr", Vector2.Zero);
            enemyMgr.AddComponent(new EnemyMgr(enemyMgr, 20, 3, 8));
        }

        private void CreatePowerUpsMgr () {
            GameObject pum = GameObject.CreateGameObject("PowerUpsMgr", Vector2.Zero);
            pum.AddComponent(new PowerUpMgr(pum, 4, 10));
        }

        private void CreateUI () {
            GameObject UILogic = GameObject.CreateGameObject("UILogic", Vector2.Zero);
            UILogic.AddComponent(new UILogic(UILogic));

            GameObject healthBG = GameObject.CreateGameObject("HealthBG", new Vector2(30, 30));
            healthBG.AddComponent(new SpriteRenderer(healthBG, "healthBG", Vector2.Zero, DrawLayer.GUI));
            GameObject healthBar = GameObject.CreateGameObject("HealthBar", new Vector2(34, 34));
            healthBar.AddComponent(new SpriteRenderer(healthBar, "healthBar", Vector2.Zero, DrawLayer.GUI));

            GameObject scoreText = GameObject.CreateGameObject("ScoreText", new Vector2(30, 60));
            scoreText.AddComponent(new TextBox(scoreText, FontMgr.GetFont("stdFont"), 6, Vector2.One));
        }

    }
}
