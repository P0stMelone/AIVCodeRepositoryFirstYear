using System;

namespace Aiv.Fast2D.Component {
    public class MainMenuLogic : SimpleMenuLogic {

        string[] mainMenuSelections = { "1 GIOCATORE", "2 GIOCATORI", "CLASSIFICA", "ESCI" };

        public MainMenuLogic(GameObject owner, Type confirmScene, Type menuScene, Type leaderboardScene, string confirmButton, string upButton, string downButton, int numberOfSelection) 
            : base(owner, confirmScene, menuScene, leaderboardScene, confirmButton, upButton, downButton, numberOfSelection) {
            textbox = new TextBox[mainMenuSelections.Length];
        }

        public override void Awake() {
            for (int i = 0; i < textbox.Length; i++) {
                textbox[i] = GameObject.Find("MenuText" + (i + 1)).GetComponent<TextBox>();
            }
        }

        public override void Update() {
            base.Update();
        }

        public override void Selection() {
            switch (selectionIndex) {
                case 0:
                    if (confirmScene == null) {
                        Game.TriggerChangeScene(null);
                        return;
                    }
                    GameData.SetPlayerCount(1);
                    Game.TriggerChangeScene(Activator.CreateInstance(confirmScene) as Scene);
                    break;
                case 1:
                    if (confirmScene == null) {
                        Game.TriggerChangeScene(null);
                        return;
                    }
                    GameData.SetPlayerCount(2);
                    Game.TriggerChangeScene(Activator.CreateInstance(confirmScene) as Scene);
                    break;
                case 2:
                    if (leaderboardScene == null) {
                        Game.TriggerChangeScene(null);
                        return;
                    }
                    Game.TriggerChangeScene(Activator.CreateInstance(leaderboardScene) as Scene);
                    break;
                case 3:
                    Game.TriggerChangeScene(null);
                    break;
            }
        }

        public override void UpdateText() {
            for (int i = 0; i < mainMenuSelections.Length; i++) {
                textbox[i].SetText(mainMenuSelections[i]);
            }
        }

    }
}
