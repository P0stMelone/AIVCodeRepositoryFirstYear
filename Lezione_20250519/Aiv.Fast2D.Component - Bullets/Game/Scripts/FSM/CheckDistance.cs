namespace Aiv.Fast2D.Component {

    public class CheckDistance : Condition {

        private StateMachineVariable<Transform> from;
        private StateMachineVariable<Transform> to;
        private StateMachineVariable<float> distanceToCompare;
        private ComparerType comparer;

        private float distanceToCompareSquared;

        private CheckDistance (StateMachineVariable<Transform> from, StateMachineVariable<Transform> to, 
            StateMachineVariable<float> distanceToCompare, ComparerType comparer) {
            this.from = from;
            this.to = to;
            this.distanceToCompare = distanceToCompare;
            this.comparer = comparer;
        }

        public override void OnEnter() {
            distanceToCompareSquared = distanceToCompare.GetValue() * distanceToCompare.GetValue();
        }

        public override bool Validate() {
            float distance = (from.GetValue().Position - to.GetValue().Position).LengthSquared;
            return ComparerUtility.Compare(distance, distanceToCompareSquared, comparer);
        }

        public static CheckDistance Factory(Transform from, Transform to, float distanceToCompare, ComparerType comparer,
            string fromVar = "", string toVar = "", string distanceVar = "", FSMComponent fsm = null) {

            var fromV = string.IsNullOrEmpty(fromVar) ? new StateMachineVariable<Transform>(from) : new StateMachineVariable<Transform>(fsm, fromVar);
            var toV = string.IsNullOrEmpty(toVar) ? new StateMachineVariable<Transform>(to) : new StateMachineVariable<Transform>(fsm, toVar);
            var distV = string.IsNullOrEmpty(distanceVar) ? new StateMachineVariable<float>(distanceToCompare) : new StateMachineVariable<float>(fsm, distanceVar);

            return new CheckDistance(fromV, toV, distV, comparer);
        }
    }
}