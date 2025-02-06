using OpenTK;
using System;

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
            Vector2 closestPoint;
            closestPoint.X = Math.Max(Position.X - halfWidth, Math.Min(Position.X + halfWidth, circle.Position.X));
            closestPoint.Y = Math.Max(Position.Y - halfHeight, Math.Min(Position.X + halfHeight, circle.Position.Y));
            return circle.Contains(closestPoint);
        }

        public override bool Collides(BoxCollider box) {
            float deltaX = Math.Abs(box.Position.X - Position.X);
            float deltaY = Math.Abs(box.Position.Y - Position.Y);
            return deltaX <= (box.halfWidth + halfWidth) &&
                deltaY <= (box.halfHeight + halfHeight);
        }

        public override bool Contains(Vector2 point) {
            float deltaX = Math.Abs(point.X - Position.X);
            float deltaY = Math.Abs(point.Y - Position.Y);
            return deltaX <= halfWidth && deltaY <= halfHeight;
        }
    }
}
