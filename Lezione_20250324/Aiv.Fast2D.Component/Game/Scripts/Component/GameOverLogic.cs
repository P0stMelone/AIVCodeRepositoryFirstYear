namespace Aiv.Fast2D.Component {
    class GameOverLogic : UserComponent {

        public GameOverLogic(GameObject owner) : base(owner) {

        }

        public override void Update() {
            if (Game.Win.GetKey(KeyCode.Y)) {
                Game.TriggerChangeScene(new PlayScene());
            } else if (Game.Win.GetKey(KeyCode.N)) {
                Game.TriggerChangeScene(new MainMenuScene());
            }
        }

    }
}
