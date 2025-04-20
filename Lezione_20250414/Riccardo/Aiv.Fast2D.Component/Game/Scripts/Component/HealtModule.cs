namespace Aiv.Fast2D.Component {
    public class HealthModule {

        private int energy;
        private int currentEnergy;
        public int CurrentEnergy {
            get { return currentEnergy; }
        }
        public float EnergyPerc {
            get { return (float)currentEnergy / energy; }
        }

        public HealthModule (int energy)  {
            this.energy = energy;
            currentEnergy = energy;
        }

        public void Reset () {
            currentEnergy = energy;
        }

        public bool OnHit (int damage) {
            currentEnergy -= damage;
            currentEnergy = currentEnergy < 0 ? 0 : currentEnergy;
            return currentEnergy <= 0;
        }

        public void Heal (int healAmount) {
            currentEnergy += healAmount;
            currentEnergy = currentEnergy > energy ? energy : currentEnergy;
        }

    }
}
