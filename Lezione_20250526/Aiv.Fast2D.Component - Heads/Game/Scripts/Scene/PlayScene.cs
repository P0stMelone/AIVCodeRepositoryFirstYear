using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayScene : Scene {
        public PlayScene() : base("Game/Assets/") {
        }

        protected override void LoadAssets() {
            AddTexture("Background", "hex_grid_green.png");
            AddTexture("P1", "player_1.png");
        }

        public override void InitializeScene() {
            base.InitializeScene();
            CreateBackground();
            CreateP1();
        }

        private void CreateBackground () {
            GameObject go = GameObject.CreateGameObject("Background", Vector2.Zero);
            go.AddComponent(new SpriteRenderer(go, "Background", Vector2.Zero, DrawLayer.Background));
        }

        private void CreateP1 () {
            GameObject player = GameObject.CreateGameObject("Player_1", new Vector2(4, 4));
            player.AddComponent(new SpriteRenderer(player, "P1", Vector2.One * 0.5f, DrawLayer.Playground));
            player.AddComponent<Rigidbody>().Friction = 15;
            player.AddComponent(ColliderFactory.CreateCircleFor(player));
            player.AddComponent(new PlayerController(player, "P1_Horizontal", "P1_Vertical", 5));
            player.AddComponent(new FaceVelocity(player, 10));
        }

    }
}
