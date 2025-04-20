
using OpenTK;

namespace Aiv.Fast2D.Component {
    internal class LeaderboardScene : Scene {
        public LeaderboardScene() : base("Game/Assets/") {

        }

        protected override void LoadAssets() {
            AddTexture("background", "LeaderboardBackground.png");
            FontMgr.AddFont("stdFont", "Game/Assets/textSheet.png", 15, 32, 20, 20);
        }

        public override void InitializeScene() {
            base.InitializeScene();
            GameObject background = GameObject.CreateGameObject("Background", Vector2.Zero);
            background.AddComponent<SpriteRenderer>("background", Vector2.Zero, DrawLayer.Background);
            background.AddComponent<LeaderboardLogic>();

            InitializeScoreTextBox();

            GameObject pageName = GameObject.CreateGameObject("PageName", new Vector2(Game.Win.Width*0.5f -200, 75));
            pageName.AddComponent<TextBox>(FontMgr.GetFont("stdFont"), 10, Vector2.One * 2);
        }

        public void InitializeScoreTextBox() {
            GameObject[] nameBox = new GameObject[GameData.leaderboardData.Scores.Length];
            GameObject[] scoreBox = new GameObject[GameData.leaderboardData.Scores.Length];
            for (int i = 0; i < GameData.leaderboardData.Scores.Length; i++) {
                nameBox[i] = GameObject.CreateGameObject("LNameBox"+(i + 1), new Vector2(Game.Win.Width * 0.33f * 2, 100 + 50 * (i + 1)));
                nameBox[i].AddComponent<TextBox>(FontMgr.GetFont("stdFont"), 6, Vector2.One);
                scoreBox[i] = GameObject.CreateGameObject("LScoreBox" + (i + 1), new Vector2(Game.Win.Width * 0.33f, 100 + 50 * (i + 1)));
                scoreBox[i].AddComponent<TextBox>(FontMgr.GetFont("stdFont"), 3, Vector2.One);
            }
        }


    }
}
