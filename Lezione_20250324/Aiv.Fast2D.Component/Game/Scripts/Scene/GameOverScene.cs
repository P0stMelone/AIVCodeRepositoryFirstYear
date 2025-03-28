using OpenTK;

namespace Aiv.Fast2D.Component {
    class GameOverScene : Scene {

        public GameOverScene() : base("Game/Assets/") {

        }

        protected override void LoadAssets() {
            AddTexture("background", "gameOverBg.png");
        }

        public override void InitializeScene() {
            base.InitializeScene();
            GameObject background = GameObject.CreateGameObject("Background", Vector2.Zero);
            background.AddComponent(new SimpleMenuLogic(background, typeof(PlayScene), typeof(MainMenuScene),
                "Confirm", "Cancel"));
            background.AddComponent(new SpriteRenderer(background, "background",
                Vector2.Zero, DrawLayer.Background));
        }

    }
}