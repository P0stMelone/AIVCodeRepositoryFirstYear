using OpenTK;

namespace SpaceShooter_NoComponent {
    public class BoxCollider : Collider {

        protected float halfWidth;
        protected float halfHeight;

        public float Height {
            get { return halfHeight * 2; }
        }
        public float Width {
            get { return halfWidth * 2; }
        }

        public BoxCollider(Rigidbody owner, int w, int h) : base (owner) {
            halfWidth = w / 2;
            halfHeight = h / 2;
        }

        public override bool Collides(Collider collider) {
            return collider.Collides(this);
        }

        public override bool Collides(CircleCollider circle) {

            return false;
        }

        public override bool Collides(BoxCollider box) {

            return false;
        }

        public override bool Contains(Vector2 point) {

            return false;
        }
    }
}
