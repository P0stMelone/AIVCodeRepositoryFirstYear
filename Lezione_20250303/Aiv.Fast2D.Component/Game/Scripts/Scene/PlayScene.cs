using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayScene : Scene{

        public PlayScene () : base ("Game/Assets/") {

        }

        protected override void LoadAssets() {
            AddTexture("player", "player_ship.png");
            AddTexture("background", "spaceBg.png");
            AddTexture("blueLaser", "Bluelaser.png");
            AddTexture("greenGlobe", "greenGlobe.png");
            AddTexture("fireGlobe", "fireGlobe.png");
        }

        public override void InitializeScene() {
            base.InitializeScene();
            CreateBackgroundGO();
            CreatePlayerGO();
            CreateBulletPool();
            isInitialized = true;
        }

        private void CreateBackgroundGO () {
            GameObject bg = GameObject.CreateGameObject("Background", Vector2.Zero);
            SpriteRenderer sr = new SpriteRenderer(bg, "background", Vector2.Zero, DrawLayer.Background);
            bg.AddComponent(sr);
        }

        private void CreatePlayerGO () {
            GameObject player = GameObject.CreateGameObject("Player", new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f));
            SpriteRenderer sr = new SpriteRenderer(player, "player", new Vector2(0.5f, 0.5f), DrawLayer.Playground);
            player.AddComponent(sr);
            PlayerController pc = new PlayerController(player, 500, KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
            player.AddComponent(pc);
            KeepInBorder kb = new KeepInBorder(player);
            player.AddComponent(kb);
            player.AddComponent(typeof(Rigidbody));
            player.Layer = (uint)Layer.Player;
        }

        private void CreateBulletPool () {
            GameObject go = GameObject.CreateGameObject("BulletPool", Vector2.Zero);
            go.AddComponent(new BulletPool(go, 30));

        }

    }
}
