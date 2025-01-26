

namespace SquareInvaders {
    public class Alien {

        private int width;
        private int height;

        private Rect sprite;
        private int distToSide;

        private BulletPooler pooler;
        private bool canShoot;

        public bool IsAlive;

        public Vector2 Velocity;
        public Vector2 Position;


        private float nextShootTime;
        private float counter;

        public Alien (Vector2 pos, Vector2 vel, int w, int h, Color col, 
            BulletPooler pooler, bool canShoot) {
            this.canShoot = canShoot;
            this.pooler = pooler;
            Position = pos;
            Velocity = vel;
            width = w;
            height = h;
            distToSide = Game.distToSide;
            sprite = new Rect(Position.X - width / 2, Position.Y - height / 2, width, height, col);
            IsAlive = true;
            InternalUpdateNextShoot();
        }

        public bool Update (out float overFlowX) {
            overFlowX = 0;
            bool reachEnd = false;

            float deltaX = Velocity.X * GfxTools.Win.DeltaTime;
            float deltaY = Velocity.Y * GfxTools.Win.DeltaTime;

            Position.X += deltaX;
            Position.Y += deltaY;

            int minX = (int)Position.X - width / 2;
            int maxX = (int)Position.X + width / 2;

            if (minX < distToSide) {
                reachEnd = true;
                overFlowX = distToSide - minX;
            } else if (maxX > GfxTools.Win.Width - distToSide) {
                reachEnd = true;
                overFlowX = (GfxTools.Win.Width - distToSide) - maxX;
            }
            TranslateSprite(new Vector2(deltaX, deltaY));
            if (!canShoot) return reachEnd;
            counter += GfxTools.Win.DeltaTime;
            if (counter < nextShootTime) return reachEnd;
            Bullet bullet = pooler.GetEnemyBullet();
            if (bullet == null) return reachEnd;
            bullet.Shoot(new Vector2(Position.X + width / 2, Position.Y + height / 2), new Vector2(0, 100));
            InternalUpdateNextShoot();
            return reachEnd;
        }

        private void InternalUpdateNextShoot () {
            counter = 0;
            nextShootTime = RandomGenerator.GetRandom(5, 10);
        }

        public void Draw () {
            sprite.Draw();
        }

        public void Translate (Vector2 transVect) {
            Position.X += transVect.X;
            Position.Y += transVect.Y;
            TranslateSprite(transVect);
        }

        private void TranslateSprite (Vector2 transVect) {
            sprite.Translate(transVect.X, transVect.Y);
        }

    }
}
