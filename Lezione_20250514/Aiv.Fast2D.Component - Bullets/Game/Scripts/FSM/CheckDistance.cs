namespace Aiv.Fast2D.Component {

    public class CheckDistance : Condition {

        private StateMachineVariable<Transform> from;
        private StateMachineVariable<Transform> to;
        private StateMachineVariable<float> distanceToCompare;
        private ComparerType comparer;

        private float distanceToCompareSquared;

        public CheckDistance (StateMachineVariable<Transform> from, StateMachineVariable<Transform> to, 
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
    }
}