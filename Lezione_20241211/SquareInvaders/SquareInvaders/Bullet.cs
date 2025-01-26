using Aiv.Draw;
using System;

namespace SquareInvaders {
    public class Bullet {

        private Sprite[] animatedVisual;
        private Vector2 velocity;

        public Vector2 Position;
        public bool IsAlive;

        private int fps = 12;
        private float timeBetweenFrame;
        private float counter;

        private int currentIndexToDraw;

        public int GetHeight () {
            return animatedVisual[currentIndexToDraw].Height;
        }
        public int GetWidth () {
            return animatedVisual[currentIndexToDraw].Width;
        }

        public Bullet(int w, int h, Color col) {
            animatedVisual = new Sprite[] {
                new Sprite("Assets\\alienBullet_0.png"),
                new Sprite("Assets\\alienBullet_1.png")
            };
            timeBetweenFrame = 1f / fps;
            counter = 0;
            currentIndexToDraw = 0;
            IsAlive = false;
        }

        public void Update() {
            if (!IsAlive) return;
            counter += GfxTools.Win.DeltaTime;
            if (counter > timeBetweenFrame) {
                counter = 0;
                currentIndexToDraw++;
                currentIndexToDraw = currentIndexToDraw % animatedVisual.Length;
            }
            Position += velocity * GfxTools.Win.DeltaTime;
            if (Position.Y > GfxTools.Win.Height || 
                Position.Y + GetHeight() <0) {
                IsAlive = false;
            }
        }

        public void Draw () {
            if (!IsAlive) return;
            GfxTools.DrawSprite(animatedVisual[currentIndexToDraw], Position);
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
