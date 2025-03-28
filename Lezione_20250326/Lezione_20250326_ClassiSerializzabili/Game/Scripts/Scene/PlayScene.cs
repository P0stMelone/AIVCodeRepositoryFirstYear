using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayScene : Scene{

        public PlayScene() : base("Game/Assets/") {

        }

        public override void InitializeScene() {
            base.InitializeScene();
            GameObject gameLogic = GameObject.CreateGameObject("GameLogic", Vector2.Zero);
            gameLogic.AddComponent<GameLogic>();
            GameObject ui = GameObject.CreateGameObject("ShootNumberText", Vector2.UnitY *50 + Vector2.UnitX * 300);
            ui.AddComponent(new TextBox(ui, FontMgr.GetFont("default"), 10, Vector2.One));
            ui = GameObject.CreateGameObject("TimePlayedText", Vector2.UnitY * 100 + Vector2.UnitX * 300);
            ui.AddComponent(new TextBox(ui, FontMgr.GetFont("default"), 10, Vector2.One));
        }

        protected override void LoadAssets() {
            base.LoadAssets();
            FontMgr.AddFont("default", "Game/Assets/textSheet.png", 15, 32, 20, 20);
        }
    }
}
