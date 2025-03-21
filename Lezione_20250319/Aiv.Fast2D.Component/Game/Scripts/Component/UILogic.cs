using OpenTK;

namespace Aiv.Fast2D.Component {
    public class UILogic : UserComponent {

        private int scoreDigit = 6;

        private SpriteRenderer hpSR;
        private TextBox scoreText;

        public UILogic(GameObject owner) : base (owner) {

        }

        public override void Awake() {
            hpSR = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
            scoreText =  GameObject.Find("ScoreText").GetComponent<TextBox>();
        }

        public void UpdateMe (float energyPerc, int score) {
            hpSR.gameObject.transform.Scale = new Vector2(energyPerc, 1);

            string scoreString = score.ToString();
            int missingDigit = scoreDigit - scoreString.Length;
            for (int i = 0; i< missingDigit; i++) {
                scoreString = "0" + scoreString;
            }
            scoreText.SetText(scoreString);
        }

    }
}
