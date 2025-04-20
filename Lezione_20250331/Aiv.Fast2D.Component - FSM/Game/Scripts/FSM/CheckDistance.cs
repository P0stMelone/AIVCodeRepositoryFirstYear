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
        private float distanceToCheck;

        private float distanceToCheckSquared;

        public CheckDistance (Transform from, Transform to, Comparer comparer, float distanceToCheck) {
            this.from = from;
            this.to = to;
            this.comparer = comparer;
            this.distanceToCheck = distanceToCheck;
        }

        public override void OnEnter () {
            distanceToCheckSquared = distanceToCheck * distanceToCheck;
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
}
