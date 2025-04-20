namespace Aiv.Fast2D.Component {
    public class AchievementSystem : UserComponent {

        private int playerDeathCounter;

        public AchievementSystem (GameObject owner) : base (owner) {

        }

        public override void Awake() {
            EventManager.AddListener(EventName.playerDeath, OnPlayerDeath);
        }

        public override void OnDestroy() {
            EventManager.RemoveListener(EventName.playerDeath, OnPlayerDeath);
        }

        private void OnPlayerDeath(EventArgs _) {
            playerDeathCounter++;
            //logica per veder se ho sbloccato l'achievement
        }
    }
}
