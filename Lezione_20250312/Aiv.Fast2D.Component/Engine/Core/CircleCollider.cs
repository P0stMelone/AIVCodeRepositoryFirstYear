using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class CircleCollider : Collider {

        public float Radius;

        public CircleCollider(GameObject owner, Vector2 offset, float radius) : base(owner, offset) {
            Radius = radius;
        }

        public override bool Collides(Collider collider) {
            return collider.Collides(this);
        }

        public override bool Collides(BoxCollider rect) {
            return rect.Collides(this);
        }

        public override bool Collides(CircleCollider circle) {
            float distanceSquared = (circle.Position - Position).LengthSquared;
            return distanceSquared <= Math.Pow(Radius + circle.Radius, 2);
        }

        public override bool Contains(Vector2 point) {
            float distanceSquared = (point - Position).LengthSquared;
            return distanceSquared <= Math.Pow(Radius, 2);
        }
    }
}
