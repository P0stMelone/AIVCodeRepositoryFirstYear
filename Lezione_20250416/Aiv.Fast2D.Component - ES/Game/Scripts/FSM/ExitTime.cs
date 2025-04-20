namespace Aiv.Fast2D.Component {
    public class ExitTime : Condition {

        private StateMachineVariable<float> timeToWait;

        private float counter;


        public ExitTime(StateMachineVariable<float> timeToWait) {
            this.timeToWait = timeToWait;
        }

        public override void OnEnter() {
            counter = 0;
        }

        public override bool Validate() {
            counter += Game.DeltaTime;
            return counter >= timeToWait.GetValue();
        }



    }
}
