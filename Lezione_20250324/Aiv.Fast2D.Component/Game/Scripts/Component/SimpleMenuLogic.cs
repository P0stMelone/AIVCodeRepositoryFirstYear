using System;

namespace Aiv.Fast2D.Component {
    public class SimpleMenuLogic : UserComponent {

        private Type confirmScene;
        private Type cancelScene;
        private string confirmButton;
        private string cancelButton;

        public SimpleMenuLogic(GameObject owner, Type confirmScene, Type cancelScene, 
            string confirmButton, string cancelButton) : base (owner) {
            this.confirmScene = confirmScene;
            this.cancelScene = cancelScene;
            this.confirmButton = confirmButton;
            this.cancelButton = cancelButton;
        }

        public override void Update () {
            if (Input.GetInputActionButtonDown(confirmButton)) {
                if (confirmScene == null) {
                    Game.TriggerChangeScene(null);
                    return;
                }
                Game.TriggerChangeScene(Activator.CreateInstance(confirmScene) as Scene);
            } else if (Input.GetInputActionButtonDown(cancelButton)) {
                if (cancelScene == null) {
                    Game.TriggerChangeScene(null);
                    return;
                }
                Game.TriggerChangeScene(Activator.CreateInstance(cancelScene) as Scene);
            }
        }

    }
}
