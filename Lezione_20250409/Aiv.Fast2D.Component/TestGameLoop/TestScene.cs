using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class TestScene : Scene {

        public TestScene(string assetPath) : base (assetPath) {

        }

        public override void InitializeScene() {
            base.InitializeScene();
            GameObject GO3 = GameObject.CreateGameObject("GO3", Vector2.Zero);
            GO3.AddComponent<TestController>();
            GameObject GO1 = GameObject.CreateGameObject("GO1", Vector2.Zero);
            GO1.AddComponent<TestComponent>();
            GameObject GO2 = GameObject.CreateGameObject("GO2", Vector2.Zero, false);
            GO2.AddComponent<TestComponent>();
            GameObject textExample = GameObject.CreateGameObject("TextExample", Vector2.One * 200);
            textExample.AddComponent(new TextBox(textExample, FontMgr.GetFont("stdFont"), 50, Vector2.One));
            textExample.GetComponent<TextBox>().SetText("Ciaone questa una"+ Environment.NewLine + "prova");
        }

        protected override void LoadAssets() {
            base.LoadAssets();
            FontMgr.AddFont("stdFont", "Game/Assets/textSheet.png", 15, 32, 20, 20);
        }

    }
}
