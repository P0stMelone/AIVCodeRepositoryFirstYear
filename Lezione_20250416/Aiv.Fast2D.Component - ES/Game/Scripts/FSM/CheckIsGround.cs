namespace Aiv.Fast2D.Component {
    public class CheckIsGround : Condition {


        private FakeGround objectToCheck;

        public CheckIsGround (FakeGround objectToCheck) {
            this.objectToCheck = objectToCheck;
        }

        public override bool Validate() {
            return objectToCheck.IsGround;
        }

    }
}
