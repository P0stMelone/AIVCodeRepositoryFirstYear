namespace Aiv.Fast2D.Component {
    public class ExitTime : Condition {

        private StateMachineVariable<float> time;
        private StateMachineVariable<bool> rememberTime;

        private float counter;


        private ExitTime(StateMachineVariable<float> time, StateMachineVariable<bool> rememberTime) {
            this.time = time;
            this.rememberTime = rememberTime;
        }

        public static ExitTime Factory(float time, bool rememberTime,
            string timeVar = "", string rememberVar = "", FSMComponent fsm = null) {

            var timeV = string.IsNullOrEmpty(timeVar) ? new StateMachineVariable<float>(time) : new StateMachineVariable<float>(fsm, timeVar);
            var rememberV = string.IsNullOrEmpty(rememberVar) ? new StateMachineVariable<bool>(rememberTime) : new StateMachineVariable<bool>(fsm, rememberVar);

            return new ExitTime(timeV, rememberV);
        }

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
