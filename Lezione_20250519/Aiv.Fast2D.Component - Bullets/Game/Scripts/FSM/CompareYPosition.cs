namespace Aiv.Fast2D.Component {
    public class CompareYPosition : Condition{

        private StateMachineVariable<Transform> owner;
        private StateMachineVariable<Transform> target;
        private ComparerType comparer;
        private StateMachineVariable<float> offset;

        private CompareYPosition (StateMachineVariable<Transform> owner, StateMachineVariable<Transform> target,
            ComparerType comparer, StateMachineVariable<float> offset) {
            this.owner = owner;
            this.target = target;
            this.comparer = comparer;
            this.offset = offset;
        }

        public override bool Validate() {
            return ComparerUtility.Compare(owner.GetValue().Position.Y + offset, target.GetValue().Position.Y,
                comparer);
        }

        public static CompareYPosition Factory(Transform owner, Transform target, float offset, ComparerType comparer,
            string ownerVar = "", string targetVar = "", string offsetVar = "", FSMComponent fsm = null) {

            var ownerV = string.IsNullOrEmpty(ownerVar) ? new StateMachineVariable<Transform>(owner) : new StateMachineVariable<Transform>(fsm, ownerVar);
            var targetV = string.IsNullOrEmpty(targetVar) ? new StateMachineVariable<Transform>(target) : new StateMachineVariable<Transform>(fsm, targetVar);
            var offsetV = string.IsNullOrEmpty(offsetVar) ? new StateMachineVariable<float>(offset) : new StateMachineVariable<float>(fsm, offsetVar);

            return new CompareYPosition(ownerV, targetV, comparer, offsetV);
        }
    }
}
