using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter_NoComponent {
    public class RandomTimer {

        private int timeMin;
        private int timeMax;
        private float remainingSeconds;

        public RandomTimer (int timeMin, int timeMax) {
            this.timeMin = timeMin;
            this.timeMax = timeMax;

            Reset();
        }

        public void Tick () {
            remainingSeconds -= Game.Win.DeltaTime;
            if (remainingSeconds < 0) remainingSeconds = 0;
        }

        public bool IsOver () {
            return remainingSeconds <= 0;
        }

        public void Reset () {
            remainingSeconds = (float)RandomGenerator.GetRandomInt(timeMin, timeMax);
        }

    }
}
