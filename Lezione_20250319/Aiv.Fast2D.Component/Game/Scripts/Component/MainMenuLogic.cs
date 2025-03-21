namespace Aiv.Fast2D.Component {
    public class MainMenuLogic : UserComponent {

        public MainMenuLogic (GameObject owner) : base (owner) {

        }

        public override void Update() {
            if (Game.Win.GetKey(KeyCode.Return)) {
                Game.TriggerChangeScene(new PlayScene());
            } else if (Game.Win.GetKey(KeyCode.Esc)) {
                Game.TriggerChangeScene(null);
            }
        }

    }
}
