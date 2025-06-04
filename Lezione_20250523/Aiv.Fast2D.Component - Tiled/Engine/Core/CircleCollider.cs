using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class CircleCollider : Collider {

        private float radius;
        public float Radius {
            get { return radius * transform.Scale.X; }
        }

        public CircleCollider(GameObject owner, Vector2 offset, float radius) : base(owner, offset) {
            this.radius = radius;
        }

        public override bool Collides(Collider collider, ref Collision collisionInfo) {
            return collider.Collides(this, ref collisionInfo);
        }

        public override bool Collides(BoxCollider rect, ref Collision collisionInfo) {
            return rect.Collides(this, ref collisionInfo);
        }

        public override bool Collides(CircleCollider circle, ref Collision collisionInfo) {
            float distanceSquared = (circle.Position - Position).LengthSquared;
            float sumRadiusSquared = (float)Math.Pow(Radius + circle.Radius, 2);
            if (distanceSquared <= sumRadiusSquared) {
                collisionInfo.Type = CollisionType.CircleIntersection;
                collisionInfo.DeltaCircle = (float)Math.Sqrt(distanceSquared) - (Radius + circle.Radius);
                return true;
            } else {
                collisionInfo.Type = CollisionType.None;
                return false;
            }
        }

        public override bool Contains(Vector2 point) {
            float distanceSquared = (point - Position).LengthSquared;
            return distanceSquared <= Math.Pow(Radius, 2);
        }
    }
}
