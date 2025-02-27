using OpenTK;

namespace SpaceShooter_NoComponent {
    public class PlayScene : Scene {

        private Player player;
        private Background background;
        private EnemyMgr enemyMgr;
        private PowerUpMgr powerUpMgr;

        public EnemyMgr EnemyMgr {
            get { return enemyMgr; }
        }
        public PowerUpMgr PowerUpMgr {
            get { return powerUpMgr; }
        }

        protected override void LoadAssets() {
            GfxMgr.AddTexture("player", "Assets/player_ship.png");
            GfxMgr.AddTexture("enemy_0", "Assets/enemy_ship.png");
            GfxMgr.AddTexture("enemy_1", "Assets/redEnemy_ship.png");
            GfxMgr.AddTexture("fireglobe", "Assets/fireGlobe.png");
            GfxMgr.AddTexture("greenglobe", "Assets/greenGlobe.png");
            GfxMgr.AddTexture("bluelaser", "Assets/blueLaser.png");
            GfxMgr.AddTexture("healthBar", "Assets/loadingBar_bar.png");
            GfxMgr.AddTexture("healthBackground", "Assets/loadingBar_frame.png");
            GfxMgr.AddTexture("energyPowerUp", "Assets/powerUp_battery.png");
            GfxMgr.AddTexture("triplePowerUp", "Assets/powerUp_triple.png");
        }

        public override void Start() {
            base.Start();
            background = new Background("Assets/spaceBg.png");
            player = new Player(new Vector2(Game.Win.Width / 2, Game.Win.Height / 2), "player", 10);
            BulletMgr.Init();
            enemyMgr = new EnemyMgr();
            powerUpMgr = new PowerUpMgr();
        }

        public override void GameLoop() {
            base.GameLoop();
            if (!player.IsActive) {
                IsPlaying = false;
                nextScene = new GameOverScene();
            }
        }

    }
}
