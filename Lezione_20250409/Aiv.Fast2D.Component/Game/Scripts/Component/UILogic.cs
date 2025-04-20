using OpenTK;

namespace Aiv.Fast2D.Component {
    public class UILogic : UserComponent {

        private int scoreDigit = 6;

        private SpriteRenderer[] hpSR = new SpriteRenderer[2];
        private TextBox[] scoreText = new TextBox[2];

        public UILogic(GameObject owner) : base (owner) {

        }
        //si potrebbe usare una lista ma qua so che ci sono massimo 2 player
        public override void Awake() {

            hpSR[0] = GameObject.Find("HealthBar1").GetComponent<SpriteRenderer>();
            scoreText[0] = GameObject.Find("ScoreText1").GetComponent<TextBox>();

            if (GameData.IsMultiplayer) {
                hpSR[1] = GameObject.Find("HealthBar2").GetComponent<SpriteRenderer>();
                scoreText[1] = GameObject.Find("ScoreText2").GetComponent<TextBox>();
            }

        }

        public void UpdateMe (float energyPerc, int score, int playerIndex) {
            //controllo per sicurezza che siano correttamente creati
            if (hpSR[playerIndex] == null || scoreText[playerIndex] == null) return;

            hpSR[playerIndex].gameObject.transform.Scale = new Vector2(energyPerc, 1);

            string scoreString = score.ToString();
            int missingDigit = scoreDigit - scoreString.Length;
            for (int i = 0; i< missingDigit; i++) {
                scoreString = "0" + scoreString;
            }
            scoreText[playerIndex].SetText(scoreString);
        }

    }
}
