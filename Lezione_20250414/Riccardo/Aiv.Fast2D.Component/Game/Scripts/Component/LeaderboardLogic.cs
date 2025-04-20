namespace Aiv.Fast2D.Component {
    internal class LeaderboardLogic : UserComponent {

        string[] names = GameData.leaderboardData.Names;
        int[] scores = GameData.leaderboardData.Scores;
        public TextBox[] scoreText = new TextBox[GameData.leaderboardData.Names.Length];
        public TextBox[] nameText = new TextBox[GameData.leaderboardData.Scores.Length];
        public TextBox pageName;

        public LeaderboardLogic(GameObject gameObject) : base(gameObject) {
        }

        public override void Awake() {
            for (int i = 0; i < scores.Length; i++) {
                nameText[i] = GameObject.Find("LNameBox" + (i + 1)).GetComponent<TextBox>();
                scoreText[i] = GameObject.Find("LScoreBox" + (i + 1)).GetComponent<TextBox>();
            }
            pageName = GameObject.Find("PageName").GetComponent<TextBox>();
        }

        public override void Start() {
            base.Start();
            UpdateMe();
        }

        public override void Update() {
            if (Input.GetInputActionButton("Cancel")) {
                Game.TriggerChangeScene(new MainMenuScene());
            }
            UpdateMe();
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

        public void UpdateMe() {
            for (int i = 0; i < scores.Length; i++) {
                nameText[i].SetText(PaddedScore(scores[i]));
                scoreText[i].SetText(names[i]);
            }
            pageName.SetText("CLASSIFICA:");
        }
    }
}
