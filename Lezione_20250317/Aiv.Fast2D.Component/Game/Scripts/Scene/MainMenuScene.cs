using OpenTK;

namespace Aiv.Fast2D.Component {
    public class MainMenuScene : Scene {

        public MainMenuScene() : base("Game/Assets/") {

        }

        protected override void LoadAssets() {
            AddTexture("background", "aivBG.png");
        }

        public override void InitializeScene() {
            base.InitializeScene();
            GameObject background = GameObject.CreateGameObject("Background", Vector2.Zero);
            background.AddComponent<MainMenuLogic>();
            background.AddComponent(new SpriteRenderer(background, "background", 
                Vector2.Zero, DrawLayer.Background));
        }

    }
}
