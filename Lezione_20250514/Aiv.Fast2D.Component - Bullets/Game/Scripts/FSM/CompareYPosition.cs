namespace Aiv.Fast2D.Component {
    public class CompareYPosition : Condition{

        private StateMachineVariable<Transform> owner;
        private StateMachineVariable<Transform> target;
        private ComparerType comparer;
        private StateMachineVariable<float> offset;

        public CompareYPosition (StateMachineVariable<Transform> owner, StateMachineVariable<Transform> target,
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
    }
}
