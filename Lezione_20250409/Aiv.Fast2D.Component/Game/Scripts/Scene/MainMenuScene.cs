using OpenTK;

namespace Aiv.Fast2D.Component {
    public class MainMenuScene : Scene {

        public MainMenuScene() : base("Game/Assets/") {

        }

        protected override void LoadAssets() {
            AddTexture("background", "aivBG.png");

            FontMgr.AddFont("stdFont", "Game/Assets/textSheet.png", 15, 32, 20, 20);
        }

        public override void InitializeScene() {
            base.InitializeScene();
            GameObject background = GameObject.CreateGameObject("Background", Vector2.Zero);
            background.AddComponent(new MainMenuLogic(background, typeof(PlayScene), null, typeof(LeaderboardScene), "Confirm", "MoveUp", "MoveDown",4));
            background.AddComponent(new SpriteRenderer(background, "background", 
                Vector2.Zero, DrawLayer.Background));
            InitializeSelectionTextbox();
        }

        private void InitializeSelectionTextbox() {
            GameObject[] textBox = new GameObject[4];
            for (int i = 0; i < textBox.Length; i++) {
                textBox[i] = GameObject.CreateGameObject("MenuText" + (i+1),
                    new Vector2(Game.Win.Width * 0.5f - 125, Game.Win.Height * 0.5f + 100 + (i * 30)));
                textBox[i].AddComponent<TextBox>(FontMgr.GetFont("stdFont"), 15, Vector2.One);
            }
        }


    }
}
