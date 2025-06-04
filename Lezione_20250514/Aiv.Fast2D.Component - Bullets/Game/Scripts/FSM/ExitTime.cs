namespace Aiv.Fast2D.Component {
    public class ExitTime : Condition {

        private StateMachineVariable<float> time;
        private StateMachineVariable<bool> rememberTime;

        private float counter;

        public override void OnEnter() {
            if (rememberTime) return;
            counter = 0;
        }

        public override bool Validate() {
            counter += Game.DeltaTime;
            return counter >= time;
        }

    }
}
