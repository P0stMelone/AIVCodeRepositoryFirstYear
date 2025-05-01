namespace Aiv.Fast2D.Component {
    public class RandomTimer {

        private int timeMin;
        private int timeMax;

        private float remainingSeconds;

        public RandomTimer (int timeMin, int timeMax) {
            this.timeMin = timeMin;
            this.timeMax = timeMax;
        }

        public void Tick () {
            remainingSeconds -= Game.DeltaTime;
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
