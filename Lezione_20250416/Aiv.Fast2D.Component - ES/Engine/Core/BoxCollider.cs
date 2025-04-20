using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class BoxCollider : Collider {

        protected float halfWidth;
        protected float halfHeight;
        public float Height {
            get { return halfHeight * 2; }
            set {
                if (value <= 0) return;
                halfHeight = value / 2;
            }
        }
        public float Width {
            get { return halfWidth * 2; }
            set {
                if (value <= 0) return;
                halfWidth = value / 2;
            }
        }

        public BoxCollider (GameObject owner, Vector2 offset, float w, float h) : base (owner, offset) {
            Width = w;
            Height = h;
        }

        public override bool Collides(Collider collider) {
            return collider.Collides(this);
        }

        public override bool Collides(BoxCollider rect) {
            float deltaX = rect.Position.X - Position.X;
            float deltaY = rect.Position.Y - Position.Y;

            return (Math.Abs(deltaX) <= halfWidth + rect.halfWidth) &&
                (Math.Abs(deltaY) <= halfHeight + rect.halfHeight);
        }

        public override bool Collides(CircleCollider circle) {
            float deltaX = circle.Position.X - 
                Math.Max(Position.X - halfWidth, Math.Min(circle.Position.X, Position.X + halfWidth));
            float deltaY = circle.Position.Y -
                Math.Max(Position.Y - halfHeight, Math.Min(circle.Position.Y, Position.Y + halfHeight));
            return (deltaX * deltaX + deltaY * deltaY) < (circle.Radius * circle.Radius);
        }

        public override bool Contains(Vector2 point) {
            return
                point.X >= Position.X - halfWidth &&
                point.X <= Position.X + halfWidth &&
                point.Y >= Position.Y - halfHeight &&
                point.Y <= Position.Y + halfHeight;
        }

    }
}
