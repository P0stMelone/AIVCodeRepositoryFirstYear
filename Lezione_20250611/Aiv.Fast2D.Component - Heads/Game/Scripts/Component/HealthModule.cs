namespace Aiv.Fast2D.Component {
    public class HealthModule  {

        private int maxEnergy;
        public int MaxEnergy { get { return maxEnergy; } }

        private int currentEnergy;
        public int CurrenteEnergy { get { return currentEnergy; } }
        public float CurrentEnergyPercentage { get { return (float)currentEnergy / maxEnergy; } }

        public HealthModule(int maxEnergy) {
            this.maxEnergy = maxEnergy;
            currentEnergy = maxEnergy;
        }

        public bool TakeDamage(int damage) {
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
