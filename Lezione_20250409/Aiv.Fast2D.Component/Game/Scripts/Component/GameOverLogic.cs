using System;

namespace Aiv.Fast2D.Component {
    public class GameOverLogic : SimpleMenuLogic {

        string[] gameOverSelections = { "GIOCA ANCORA", "MENU PRINCIPALE", "ESCI" };

        TextBox[] scoreTextbox;

        public GameOverLogic(GameObject owner, Type confirmScene, Type menuScene, Type leaderboardScene, string confirmButton, string upButton, string downButton, int numberOfSelection)
            : base(owner, confirmScene, menuScene, leaderboardScene, confirmButton, upButton, downButton, numberOfSelection) {
            textbox = new TextBox[gameOverSelections.Length];
            scoreTextbox = new TextBox[GameData.PlayerCount];
        }

        public override void Awake() {
            for (int i = 0; i < textbox.Length; i++) {
                textbox[i] = GameObject.Find("MenuText" + (i + 1)).GetComponent<TextBox>();
            }
            for (int i = 0; i < scoreTextbox.Length; i++) {
                scoreTextbox[i] = GameObject.Find("ScoreText" + (i + 1)).GetComponent<TextBox>();
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
                    Game.TriggerChangeScene(Activator.CreateInstance(confirmScene) as Scene);
                    break;
                case 1:
                    if (menuScene == null) {
                        Game.TriggerChangeScene(null);
                        return;
                    }
                    Game.TriggerChangeScene(Activator.CreateInstance(menuScene) as Scene);
                    break;
                case 2:
                    Game.TriggerChangeScene(null);
                    break;
            }
        }

        public override void UpdateText() {
            for (int i = 0; i < gameOverSelections.Length; i++) {
                textbox[i].SetText(gameOverSelections[i]);
            }
            for (int i = 0; i < scoreTextbox.Length; i++) {
                scoreTextbox[i].SetText($"P{i+1}: {PaddedScore(GameData.Scores[i])}");
            }
        }

        public string PaddedScore(int score) {
            int scoreDigit = 6;
            string scoreString = score.ToString();
            int missingDigit = scoreDigit - scoreString.Length;
            for (int i = 0; i < missingDigit; i++) {
                scoreString = "0" + scoreString;
            }
            return scoreString;
        }

    }
}
