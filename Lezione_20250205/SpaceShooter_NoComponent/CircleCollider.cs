using OpenTK;
using System;

namespace SpaceShooter_NoComponent {
    public class CircleCollider : Collider {

        public float Radius;

        public CircleCollider (Rigidbody owner, float radius) : base (owner) {
            Radius = radius;
        }

        public override bool Collides(Collider collider) {
            return collider.Collides(this);
        }

        public override bool Collides(CircleCollider circle) {
            Vector2 diff = circle.Position - Position;
            return diff.LengthSquared <= Math.Pow(circle.Radius + Radius,2);
        }

        public override bool Collides(BoxCollider box) {
            return box.Collides(this);
        }

        public override bool Contains(Vector2 point) {
            Vector2 diff = point - Position;
            return diff.LengthSquared <= Radius * Radius;
        }
    }
}
