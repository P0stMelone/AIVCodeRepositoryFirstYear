using OpenTK;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public class PlayerController : UserComponent {


        private BulletType bulletType;
        public BulletType BulletType {
            get { return bulletType; }
            set { bulletType = value; }
        }
        public int PlayerIndex {
            get;
            private set;
        }

        private float reloadTime;
        private float currentReloadTime;
        private string shootAction;

        private string horizontalAction, verticalAction;
        private float speed;
        private HealthModule healthModule;

        private Rigidbody rb;
        private ShootModule sm;
        private SpriteRenderer sr;
        private Collider collider;

        private UILogic UILogic;

        private int score;
        public int Score {
            get { return score; }
            set {
                score = value;
                UpdateUI();
            }
        }


        public PlayerController(GameObject gameObject, float speed, string horizontalAction,
            string verticalAction, float reloadTime, string shootAction, int energy, int playerIndex) : base(gameObject) {
            this.speed = speed;
            this.horizontalAction = horizontalAction;
            this.verticalAction = verticalAction;
            this.shootAction = shootAction;
            this.reloadTime = reloadTime;
            currentReloadTime = reloadTime;
            bulletType = BulletType.BlueLaser;
            healthModule = new HealthModule(energy);
            PlayerIndex = playerIndex;
        }

        public override void Awake() {
            rb = GetComponent<Rigidbody>();
            sm = GetComponent<ShootModule>();
            sr = GetComponent<SpriteRenderer>();
            collider = GetComponent<Collider>();
            UILogic = GameObject.Find("UILogic").GetComponent<UILogic>();
        }

        public override void Start() {
            Score = 0;
        }


        public override void Update () {
            if (Input.GetInputActionButtonDown("Cancel")) { 
                Game.TriggerChangeScene(new MainMenuScene());
            }
            HandleMovement();
            if (!sm.Enabled) return;
            HandleShoot();
        }

        private void HandleMovement () {
            Vector2 input = Vector2.UnitX * Input.GetAxis(horizontalAction) + 
                Vector2.UnitY * Input.GetAxis(verticalAction);
            if (input != Vector2.Zero) input.Normalize();
            rb.Velocity = input * speed;
        }

        private void HandleShoot () {
            currentReloadTime -= Game.DeltaTime;
            if (currentReloadTime > 0) return;
            if (!Input.GetInputActionButton(shootAction)) return;
            if (!sm.Shoot(bulletType, transform.Position + Vector2.UnitX * sr.Width / 2, gameObject)) return;
            currentReloadTime = reloadTime;
        }


        #region PlayerTakeDamage
        //questo sta comunque facendo tanto, ma almeno è leggibile
        public void TakeDamage(int dmg) {
            if (!healthModule.OnHit(dmg)) {
                UpdateUI();
                return;
            }

            PlayerDeath();
            UpdateUI();

            if (AllPlayersAreDead()) {
                ManageGameOver();
            }
        }

        private void PlayerDeath() {
            for (int i = 0; i < GameData.PlayerCount; i++) {
                PlayerController player = GetPlayerController(i);
                if (player.healthModule.CurrentEnergy <= 0) {
                    player.DisablePlayerComponents();
                }
            }
        }

        private void DisablePlayerComponents() {
            sr.Enabled = false;
            rb.Enabled = false;
            sm.Enabled = false;
            collider.Enabled = false;
        }

        private bool AllPlayersAreDead() {
            bool allDead = true;
            int[] scores = new int[GameData.PlayerCount];

            for (int i = 0; i < GameData.PlayerCount; i++) {
                PlayerController player = GetPlayerController(i);
                scores[i] = player.Score;

                if (player.healthModule.CurrentEnergy > 0) {
                    allDead = false;
                }
            }

            GameData.SetScores(scores);
            return allDead;
        }

        private void ManageGameOver() {
            List<int> highScore = new List<int>();

            for (int i = 0; i < GameData.PlayerCount; i++) {
                int playerScore = GameData.Scores[i];
                if (GameData.leaderboardData.IsNewHighScore(playerScore)) {
                    highScore.Add(i);
                }
            }
            if (highScore.Count > 0) {
                GameData.PendingHighScores = highScore;
                Game.TriggerChangeScene(new NewHighScoreScene());
            }
            else {
                Game.TriggerChangeScene(new GameOverScene());
            }
        }

        

        private PlayerController GetPlayerController(int index) {
            return GameObject.Find("Player" + (index + 1)).GetComponent<PlayerController>();
        }



        #endregion


        public void HealMe (int healAmount) {
            healthModule.Heal(healAmount);
            UpdateUI();
        }

        private void UpdateUI () {
            UILogic.UpdateMe(healthModule.EnergyPerc, score, PlayerIndex);
        }

    }
}
