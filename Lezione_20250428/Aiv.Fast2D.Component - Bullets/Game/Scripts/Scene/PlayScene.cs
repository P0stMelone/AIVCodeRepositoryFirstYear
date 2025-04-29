using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayScene : Scene {
        public PlayScene() : base("Game/Assets/") {
        }

        protected override void LoadAssets() {
            AddTexture("Player", "player.png");
            AddTexture("Link", "Link_Sheet.png");
        }

        public override void InitializeScene() {
            base.InitializeScene();
            //CreatePlayer();
            CreateLink();
        }

        private void CreatePlayer () {
            GameObject player = GameObject.CreateGameObject("Player", new Vector2(Game.Win.OrthoWidth * 0.5f, 
                Game.Win.OrthoHeight * 0.5f));
            player.AddComponent(new SpriteRenderer(player, "Player", Vector2.One * 0.5f, DrawLayer.Playground));
        }
        
        private void CreateLink () {
            GameObject link = GameObject.CreateGameObject("Link", new Vector2(Game.Win.OrthoWidth * 0.5f,
                Game.Win.OrthoHeight * 0.5f));
            link.AddComponent(new SpriteRenderer(link, "Link", Vector2.Zero, DrawLayer.Playground, 64, 64, Vector2.Zero));
            Sheet linkSheet = new Sheet(GfxMgr.GetTexture("Link"), 8, 8);
            SheetClip run = new SheetClip(linkSheet, "Run", new int[] { 0, 1, 2, 3, 4, 5, 6 }, true, 10);
            SheetClip die = new SheetClip(linkSheet, "Die", new int[] { 18, 18, 19, 20, 21, 22, 23 }, false, 10);
            SheetClip attack = new SheetClip(linkSheet, "Attack", new int[] { 38, 39, 40 }, false, 10, "Run");
            SheetAnimator animator = new SheetAnimator(link, link.GetComponent<SpriteRenderer>());
            link.AddComponent(animator);
            animator.AddClip(run);
            animator.AddClip(die);
            animator.AddClip(attack);
            link.AddComponent<LinkController>();
        }
    }
}
