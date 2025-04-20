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
            AddTexture("asteroid", "Asteroid.png");
            AddTexture("enemy_2", "ship_1.png");
            FontMgr.AddFont("stdFont", "Game/Assets/textSheet.png", 15, 32, 20, 20);
        }

        public override void InitializeScene() {
            base.InitializeScene();
            CreateBackgroundGO();
            CreatePlayer();
            PlayerInputs.GamePlayInputMapping();
            CreateBulletPool();
            CreateEnemyMgr();
            CreatePowerUpsMgr();
            CreateAsteroidMgr();
            CreateUI();
        }


        private void CreateBackgroundGO () {
            GameObject bg = GameObject.CreateGameObject("Background", Vector2.Zero);
            SpriteRenderer sr = new SpriteRenderer(bg, "background", Vector2.Zero, DrawLayer.Background);
            GameObject bg2 = GameObject.CreateGameObject("BackgroundTail", Vector2.Zero);
            bg2.AddComponent<SpriteRenderer>("background", Vector2.Zero, DrawLayer.Background);
            bg.AddComponent(sr);
            bg.AddComponent<BackgroundComponent>();
        }

        private void CreatePlayer() {
            GameObject[] player = new GameObject[GameData.PlayerCount];
            Vector2[] position;
            if (GameData.PlayerCount > 1) {
                position = new Vector2[2];
                position[0] = new Vector2(Game.Win.Width * 0.25f, Game.Win.Height * 0.33f);
                position[1] = new Vector2(Game.Win.Width * 0.25f, Game.Win.Height * 0.66f);
            }
            else {
                position = new Vector2[1];
                position[0] = new Vector2(Game.Win.Width * 0.25f, Game.Win.Height * 0.5f);
            }
            for (int i = 0; i < GameData.PlayerCount; i++) {
                player[i] = GameObject.CreateGameObject("Player" + (i + 1), position[i]);
                player[i].AddComponent<SpriteRenderer>("player", new Vector2(0.5f, 0.5f), DrawLayer.Playground);
                player[i].AddComponent<PlayerController>(500, "Horizontal" + (i + 1), "Vertical" + (i + 1),
                    0.5f, "Shoot" + (i + 1), 100, i);
                player[i].AddComponent<KeepInBorder>();
                player[i].AddComponent<Rigidbody>();
                player[i].AddComponent<ShootModule>();
                player[i].Layer = (uint)Layer.Player;
                player[i].AddComponent(ColliderFactory.CreateBoxFor(player[i]));
            }
            if(GameData.PlayerCount > 1) {
                    player[0].GetComponent<SpriteRenderer>().SetAdditiveColor(new Vector4(255, 0, 0, 0));
                    player[1].GetComponent<SpriteRenderer>().SetAdditiveColor(new Vector4(0, 0, 255, 0));
            }
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

        private void CreateAsteroidMgr() {
            GameObject asteroidMgr = GameObject.CreateGameObject("AsteroidMgr", Vector2.Zero);
            asteroidMgr.AddComponent<AsteroidPool>(10, 2, 7);
        }

        private void CreateUI () {
            GameObject UILogic = GameObject.CreateGameObject("UILogic", Vector2.Zero);
            UILogic.AddComponent(new UILogic(UILogic));

            GameObject[] healthBG = new GameObject[GameData.PlayerCount];
            Vector2[] healthBGPos = { new Vector2(30, 30), new Vector2(30, Game.Win.Height - 30) };
            GameObject[] healthBar = new GameObject[GameData.PlayerCount];
            Vector2[] healthBarPos = { new Vector2(34, 34), new Vector2(34, Game.Win.Height - 26) };
            GameObject[] scoreText = new GameObject[GameData.PlayerCount];
            Vector2[] scoreTextPos = { new Vector2(30, 60), new Vector2(34, Game.Win.Height - 60) };

            for (int i = 0; i < GameData.PlayerCount; i++) {
                healthBG[i] = GameObject.CreateGameObject("HealthBG"+(i+1), healthBGPos[i]);
                healthBG[i].AddComponent<SpriteRenderer>("healthBG", Vector2.Zero, DrawLayer.GUI);
                healthBar[i] = GameObject.CreateGameObject("HealthBar" + (i + 1), healthBarPos[i]);
                healthBar[i].AddComponent<SpriteRenderer>("healthBar", Vector2.Zero, DrawLayer.GUI);
                scoreText[i] = GameObject.CreateGameObject("ScoreText" + (i+1), scoreTextPos[i]);
                scoreText[i].AddComponent<TextBox>(FontMgr.GetFont("stdFont"), 6, Vector2.One);
            }
        }

    }
}
