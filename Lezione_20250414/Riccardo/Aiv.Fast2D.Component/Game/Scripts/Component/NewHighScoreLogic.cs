using System;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public class NewHighScoreLogic : UserComponent {


        private TextBox scoreText;
        private TextBox tagText;

        private TextBoxInput nameInput;

        private int currentScoreIndex = 0; 
        private List<int> highScores; 


        public NewHighScoreLogic(GameObject gameObject) : base(gameObject) {
        }

        public override void Awake() {
            nameInput = GameObject.Find("TextName").GetComponent<TextBoxInput>();
            scoreText = GameObject.Find("NewHighScoreText").GetComponent<TextBox>();
            if (GameData.IsMultiplayer) {
                tagText = GameObject.Find("TagText").GetComponent<TextBox>();
            }
            highScores = GameData.PendingHighScores;
            if(highScores.Count > 0) {
                scoreText.SetText(PaddedScore(GameData.Scores[highScores[currentScoreIndex]]));
            }
        }

        public override void OnEnable() {
            nameInput.onConfirm += OnConfirm;
        }

        public override void OnDisable() {
            nameInput.onConfirm -= OnConfirm;
        }

        private void OnConfirm(string name) {
            SaveSelection(name);
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




        public void SaveSelection(string name) {
            GameData.leaderboardData.SetScore(GameData.Scores[highScores[currentScoreIndex]], name);
            currentScoreIndex++;
            if (currentScoreIndex < highScores.Count) {
                scoreText.SetText(PaddedScore(GameData.Scores[highScores[currentScoreIndex]]));
                nameInput.ResetName();
            } else {
                GameData.leaderboardData.SaveToFile();
                Game.TriggerChangeScene(new GameOverScene());
            }
        }

    }
}
