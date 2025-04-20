using OpenTK;

namespace Aiv.Fast2D.Component {
    public class EventManagerTestScene : Scene {


        public EventManagerTestScene() : base(string.Empty) {

        }


        protected override void LoadAssets() {
            AudioMgr.AddClip("PlayerDeath", "Game/Audio/cannonShoot.wav");
        }

        public override void InitializeScene() {
            base.InitializeScene();
            GameObject player = GameObject.CreateGameObject("Player", new Vector2(100, 100));
            player.AddComponent<Player>();
            GameObject testListenr = GameObject.CreateGameObject("TestListener", Vector2.Zero);
            testListenr.AddComponent<TestListener>();
            testListenr.AddComponent<AudioSource>();
            GameObject enemy = GameObject.CreateGameObject("Giangiovanni Giancarlo", Vector2.Zero);
            enemy.AddComponent<Enemy>();
        }
    }
}
