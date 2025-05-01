using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class BoxCollider : Collider {

        protected float halfWidth;
        protected float halfHeight;
        public float Height {
            get { return halfHeight * 2 * transform.Scale.Y; }
        }
        public float Width {
            get { return halfWidth * 2 * transform.Scale.X; }
        }

        public BoxCollider (GameObject owner, Vector2 offset, float w, float h) : base (owner, offset) {
            halfWidth = w / 2;
            halfHeight = h / 2;
        }

        private BoxCollider(GameObject owner, Vector2 offset) : base(owner, offset) {

        }

        public override bool Collides(Collider collider) {
            return collider.Collides(this);
        }

        public override bool Collides(BoxCollider rect) {
            float deltaX = rect.Position.X - Position.X;
            float deltaY = rect.Position.Y - Position.Y;
            float halfWidth = Width / 2;
            float halfHeight = Height / 2;
            return (Math.Abs(deltaX) <= halfWidth + rect.halfWidth) &&
                (Math.Abs(deltaY) <= halfHeight + rect.halfHeight);
        }

        public override bool Collides(CircleCollider circle) {
            float halfWidth = Width / 2;
            float halfHeight = Height / 2;
            float deltaX = circle.Position.X - 
                Math.Max(Position.X - halfWidth, Math.Min(circle.Position.X, Position.X + halfWidth));
            float deltaY = circle.Position.Y -
                Math.Max(Position.Y - halfHeight, Math.Min(circle.Position.Y, Position.Y + halfHeight));
            return (deltaX * deltaX + deltaY * deltaY) < (circle.Radius * circle.Radius);
        }

        public override bool Contains(Vector2 point) {
            float halfWidth = Width / 2;
            float halfHeight = Height / 2;
            return
                point.X >= Position.X - halfWidth &&
                point.X <= Position.X + halfWidth &&
                point.Y >= Position.Y - halfHeight &&
                point.Y <= Position.Y + halfHeight;
        }

        public override Component Clone (GameObject owner) {
            BoxCollider boxCollider = new BoxCollider(owner, Offset);
            boxCollider.halfWidth = halfWidth;
            boxCollider.halfHeight = halfHeight;
            return boxCollider;
        }

    }
}
