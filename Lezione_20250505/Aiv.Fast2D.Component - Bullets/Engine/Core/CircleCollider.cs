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
            collisionInfo.Type = CollisionType.CircleIntersection;
            float distanceSquared = (circle.Position - Position).LengthSquared;
            //collisionInfo.Delta = 
            return distanceSquared <= Math.Pow(Radius + circle.Radius, 2);
        }

        public override bool Contains(Vector2 point) {
            float distanceSquared = (point - Position).LengthSquared;
            return distanceSquared <= Math.Pow(Radius, 2);
        }
    }
}
