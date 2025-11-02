namespace Aiv.Fast2D.Component {
    internal class HealthModule : UserComponent {

        private int maxEnergy;
        public int MaxEnergy { get { return maxEnergy; } }

        private int currentEnergy;
        public int CurrenteEnergy { get { return currentEnergy; } }


        public HealthModule(GameObject gameObject, int maxEnergy) : base(gameObject) {
            this.maxEnergy = maxEnergy;
            currentEnergy = maxEnergy;
        }

        public bool OnHit(int damage) {
            currentEnergy -= damage;
            currentEnergy = currentEnergy < 0 ? 0 : currentEnergy;
            return currentEnergy <= 0;
        }

        public void Heal(int healAmt) {
            currentEnergy += healAmt;
            currentEnergy = currentEnergy > maxEnergy ? maxEnergy : currentEnergy;
        }
    }
}
