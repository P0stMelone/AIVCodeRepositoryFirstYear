using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class BoxCollider : Collider, IDrawable {

        protected float halfWidth;
        protected float halfHeight;
        public float Height {
            get { return halfHeight * 2 * transform.Scale.Y; }
        }
        public float Width {
            get { return halfWidth * 2 * transform.Scale.X; }
        }

        DrawLayer IDrawable.Layer => DrawLayer.GUI;

        public BoxCollider (GameObject owner, Vector2 offset, float w, float h) : base (owner, offset) {
            halfWidth = w / 2;
            halfHeight = h / 2;
            DrawMgr.AddItem(this);
        }

        private BoxCollider(GameObject owner, Vector2 offset) : base(owner, offset) {

        }

        public override bool Collides(Collider collider, ref Collision collisionInfo) {
            return collider.Collides(this, ref collisionInfo);
        }

        public override bool Collides(BoxCollider rect, ref Collision collisionInfo) {
            collisionInfo.Type = CollisionType.None;
            float deltaX = Math.Abs(rect.Position.X - Position.X) - (Width /2 + rect.Width / 2);
            if (deltaX > 0) return false;
            float deltaY = Math.Abs(rect.Position.Y - Position.Y) - (Height / 2 + rect.Height / 2);
            if (deltaY > 0) return false;

            collisionInfo.Type = CollisionType.RectsIntersection;
            collisionInfo.Delta = new Vector2(-deltaX, -deltaY);
            return true;
        }

        public override bool Collides(CircleCollider circle, ref Collision collisionInfo) {
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

        public void Draw() {
#if DEBUG
            Sprite sprite = new Sprite(Width, Height);
            sprite.position = Position;
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
            sprite.DrawWireframe(new Vector4(1, 1, 1, 1));
#endif
        }
    }
}
