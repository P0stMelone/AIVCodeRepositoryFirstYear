using OpenTK;
namespace Aiv.Fast2D.Component {

    public enum Comparer {
        Equal,
        Less,
        Greater,
        GreaterEqual,
        LessEqual
    }

    public class CheckDistance : Condition {

        private Transform from;
        private Transform to;
        private Comparer comparer;
        private StateMachineVariable<float> distanceToCheck;

        private float distanceToCheckSquared;

        public CheckDistance (Transform from, Transform to, Comparer comparer, StateMachineVariable<float> distanceToCheck) {
            this.from = from;
            this.to = to;
            this.comparer = comparer;
            this.distanceToCheck = distanceToCheck;
        }

        public override void OnEnter () {
            distanceToCheckSquared = distanceToCheck.GetValue() * distanceToCheck.GetValue();
        }


        public override bool Validate () {
            return InternalCompare();
        }

        private bool InternalCompare () {
            float distanceSquared = (from.Position - to.Position).LengthSquared;
            switch(comparer) {
                case Comparer.Equal:
                    return distanceSquared == distanceToCheckSquared;
                case Comparer.Greater:
                    return distanceSquared > distanceToCheckSquared;
                case Comparer.Less:
                    return distanceSquared < distanceToCheckSquared;
                case Comparer.GreaterEqual:
                    return distanceSquared >= distanceToCheckSquared;
                case Comparer.LessEqual:
                    return distanceSquared <= distanceToCheckSquared;
                default:
                    return false;

            }
        }
    }

    public class CheckDistanceVector2 : Condition {

        private Transform from;
        private StateMachineVariable<Vector2> to;
        private Comparer comparer;
        private StateMachineVariable<float> distanceToCheck;

        private float distanceToCheckSquared;

        public CheckDistanceVector2(Transform from, StateMachineVariable<Vector2> to, Comparer comparer, StateMachineVariable<float> distanceToCheck) {
            this.from = from;
            this.to = to;
            this.comparer = comparer;
            this.distanceToCheck = distanceToCheck;
        }

        public override void OnEnter() {
            distanceToCheckSquared = distanceToCheck.GetValue() * distanceToCheck.GetValue();
        }


        public override bool Validate() {
            return InternalCompare();
        }

        private bool InternalCompare() {
            float distanceSquared = (from.Position - to.GetValue()).LengthSquared;
            switch (comparer) {
                case Comparer.Equal:
                    return distanceSquared == distanceToCheckSquared;
                case Comparer.Greater:
                    return distanceSquared > distanceToCheckSquared;
                case Comparer.Less:
                    return distanceSquared < distanceToCheckSquared;
                case Comparer.GreaterEqual:
                    return distanceSquared >= distanceToCheckSquared;
                case Comparer.LessEqual:
                    return distanceSquared <= distanceToCheckSquared;
                default:
                    return false;

            }
        }
    }
}
