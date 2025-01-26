using System;

namespace SquareInvaders {
    public class Bullet {

        private Rect rect;
        private Vector2 velocity;

        public Vector2 Position;
        public bool IsAlive;

        public int GetHeight () {
            return rect.GetHeight();
        }
        public int GetWidth () {
            return rect.GetWidth();
        }

        public Bullet (int w, int h, Color col) {
            rect = new Rect(0, 0, w, h, col);
            IsAlive = false;
        }

        public void Update() {
            if (!IsAlive) return;
            Position += velocity * GfxTools.Win.DeltaTime;
            if (Position.Y > GfxTools.Win.Height || 
                Position.Y + rect.GetHeight() <0) {
                IsAlive = false;
            }
        }

        public void Draw () {
            if (!IsAlive) return;
            rect.SetPosition(Position);
            rect.Draw();
        }

        public void Shoot (Vector2 startPosition, Vector2 velocity) {
            Position = startPosition;
            this.velocity = velocity;
            IsAlive = true;
        }

        public bool Collide (Vector2 center, float radius) {
            float radiusSumSquared = (float)Math.Pow(radius + GetWidth() / 2, 2);
            return (center - Position).GetLenghtSquared() <= radiusSumSquared;
        }

    }
}
