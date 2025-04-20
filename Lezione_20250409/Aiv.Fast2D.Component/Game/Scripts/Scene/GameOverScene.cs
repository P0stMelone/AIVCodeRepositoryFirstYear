using OpenTK;

namespace Aiv.Fast2D.Component {
    class GameOverScene : Scene {

        public GameOverScene() : base("Game/Assets/") {
        }

        protected override void LoadAssets() {
            AddTexture("background", "gameOverBg.png");
            FontMgr.AddFont("stdFont", "Game/Assets/textSheet.png", 15, 32, 20, 20);

        }

        public override void InitializeScene() {
            base.InitializeScene();
            GameObject background = GameObject.CreateGameObject("Background", Vector2.Zero);
            background.AddComponent(new GameOverLogic(background, typeof(PlayScene), typeof(MainMenuScene), typeof(LeaderboardScene), "Confirm",
                "MoveUp", "MoveDown",3));
            background.AddComponent(new SpriteRenderer(background, "background",
                Vector2.Zero, DrawLayer.Background));
            InitializeSelectionTextbox();
            InitializeScoreText();
        }

        private void InitializeScoreText() {
            Vector2[] textboxPos = {
                new Vector2(Game.Win.Width* 0.5f - 150, Game.Win.Height * 0.5f + 100),
                new Vector2(Game.Win.Width* 0.33f - 250, Game.Win.Height * 0.5f + 100),
                new Vector2(Game.Win.Width * 0.66f - 100, Game.Win.Height * 0.5f + 100)
            };
            GameObject[] textBox = new GameObject[GameData.PlayerCount];
            if (!GameData.IsMultiplayer) {
                textBox[0] = GameObject.CreateGameObject("ScoreText1", textboxPos[0]);
                textBox[0].AddComponent<TextBox>(FontMgr.GetFont("stdFont"), 15, Vector2.One * 2);
                return;
            }
            for (int i = 0; i < GameData.PlayerCount; i++) {
                textBox[i] = GameObject.CreateGameObject("ScoreText" + (i + 1), textboxPos[i + 1]);
                textBox[i].AddComponent<TextBox>(FontMgr.GetFont("stdFont"), 15, Vector2.One * 2);
            }
        }

        private void InitializeSelectionTextbox() {
            GameObject[] textBox = new GameObject[3];
            for (int i = 0; i < textBox.Length; i++) {
                textBox[i] = GameObject.CreateGameObject("MenuText" + (i + 1),
                    new Vector2(Game.Win.Width * 0.5f - 125, Game.Win.Height * 0.5f + 200 + (i * 30)));
                textBox[i].AddComponent<TextBox>(FontMgr.GetFont("stdFont"), 15, Vector2.One);
            }
        }

    }
}