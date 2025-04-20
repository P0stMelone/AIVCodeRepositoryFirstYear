namespace Aiv.Fast2D.Component {
    public class FakeGround : UserComponent {


        private float groundPosition;
        public bool IsGround {
            get { return transform.Position.Y == groundPosition; }
        }

        public FakeGround(GameObject owner, float groundPosition) : base (owner) {
            this.groundPosition = groundPosition;
        }

        public override void Update () {
            if (transform.Position.Y > groundPosition) {
                transform.Position = new OpenTK.Vector2(transform.Position.X, groundPosition);
            }
        }

    }
}
