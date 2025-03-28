using OpenTK;

namespace Aiv.Fast2D.Component {
    public class TestInputScene : Scene {

        public TestInputScene () : base (string.Empty) {

        }

        public override void InitializeScene() {
            base.InitializeScene();
            GameObject go = GameObject.CreateGameObject("TestInput", Vector2.Zero);
            go.AddComponent(new TestInputSystemComponent(go));

        }
    }
}
