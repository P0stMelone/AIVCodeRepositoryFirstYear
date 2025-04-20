

using OpenTK;

namespace Aiv.Fast2D.Component {
    internal class NewHighScoreScene : Scene {
        public NewHighScoreScene() : base("Game/Assets/") {
        }

        protected override void LoadAssets() {
            AddTexture("background", "LeaderboardBackground.png");
            FontMgr.AddFont("stdFont", "Game/Assets/textSheet.png", 15, 32, 20, 20);
        }

        public override void InitializeScene() {
            base.InitializeScene();

            GameObject background = GameObject.CreateGameObject("Background", Vector2.Zero);
            background.AddComponent<SpriteRenderer>("background", Vector2.Zero, DrawLayer.Background);

            GameObject leaderboard = GameObject.CreateGameObject("Leaderboard", Vector2.Zero);
            leaderboard.AddComponent<NewHighScoreLogic>();

            GameObject nameText = GameObject.CreateGameObject("TextName", new Vector2(Game.Win.Width * 0.5f - 40, 250));
            nameText.AddComponent<TextBox>(FontMgr.GetFont("stdFont"), 3, Vector2.One*2);
            nameText.AddComponent<TextBoxInput>();

            GameObject scoreText = GameObject.CreateGameObject("NewHighScoreText", new Vector2(Game.Win.Width * 0.5f -120, 350));
            scoreText.AddComponent<TextBox>(FontMgr.GetFont("stdFont"), 6, Vector2.One*2);
            if (GameData.IsMultiplayer) {
                MultiplayerTag();
            }
        }

        public void MultiplayerTag() {
            GameObject tagText = GameObject.CreateGameObject("TagText", new Vector2(Game.Win.Width * 0.5f - 250, 250));
            tagText.AddComponent<TextBox>(FontMgr.GetFont("stdFont"), 3, Vector2.One * 2);
        }
    }
}
