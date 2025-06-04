using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class CircleCollider : Collider, IDrawable {

        private float radius;
        public float Radius {
            get { return radius * transform.Scale.X; }
        }

        DrawLayer IDrawable.Layer => DrawLayer.GUI;

        public CircleCollider(GameObject owner, Vector2 offset, float radius) : base(owner, offset) {
            this.radius = radius;
#if DEBUG
            DrawMgr.AddItem(this);
#endif
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

        public void Draw() {
#if DEBUG
            Sprite sprite = new Sprite(Radius * 2, Radius * 2);
            sprite.position = Position;
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
            sprite.DrawWireframe(new Vector4(1, 1, 1, 1));
#endif
        }
    }
}
