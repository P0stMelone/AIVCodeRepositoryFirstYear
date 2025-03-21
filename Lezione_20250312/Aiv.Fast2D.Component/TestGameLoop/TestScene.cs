using OpenTK;

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
        }

        protected override void LoadAssets() {
            base.LoadAssets();
        }

    }
}
