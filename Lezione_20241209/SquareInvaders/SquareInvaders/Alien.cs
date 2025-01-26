

namespace SquareInvaders {
    public class Alien {

        private int width;
        private int height;

        private Pixel[] visual;
        private int distToSide;

        private BulletPooler pooler;
        private bool canShoot;

        public bool IsAlive;
        public bool IsVisible;

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



            //Array di byte che mi dice, su un quadrato, quali sono i pixel da disengare (1) 
            //e dove invece non c'è nessn pixel da disengare (0)
            byte[] pixelArr = {  0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                                 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
                                 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0,
                                 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0,
                                 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                                 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1,
                                 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1,
                                 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0
            };

            int numPixel = 0;
            for (int i = 0; i < pixelArr.Length;i++) {
                if (pixelArr[i] == 1) numPixel++;
            }

            visual = new Pixel[numPixel];

            int pixelSize = 5;
            int numCol = 11;
            float startXPos = Position.X - width / 2f;
            float posY = Position.Y - height / 2f;
            int pixelIndex = 0;

            for (int i = 0; i < pixelArr.Length; i++) {
                if (i != 0 && i % numCol == 0) {
                    posY += pixelSize;
                }
                if (pixelArr[i] == 0) continue;
                visual[pixelIndex] = new Pixel(new Vector2(startXPos + (i % numCol) * pixelSize, posY),
                    pixelSize, col);
                pixelIndex++;
            }



            IsAlive = true;
            IsVisible = true;
            InternalUpdateNextShoot();
        }

        public bool Update (out float overFlowX) {
            if (IsAlive) {
                return AliveUpdate(out overFlowX);
            } else {
                overFlowX = 0;
                NotAliveUpdate();
                return false;
            }
        }

        private bool AliveUpdate (out float overFlowX) {
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

        private void NotAliveUpdate () {
            bool noonevisible = true;
            for (int i = 0; i < visual.Length; i++) {
                if (!visual[i].GetIsVisible()) continue;
                noonevisible = false;
                visual[i].Update();
            }
            IsVisible = !noonevisible;
        }

        private void InternalUpdateNextShoot () {
            counter = 0;
            nextShootTime = RandomGenerator.GetRandom(5, 10);
        }

        public void Draw () {
            for (int i = 0; i < visual.Length; i++) {
                visual[i].DrawPixel();
            }
        }

        public void Translate (Vector2 transVect) {
            Position.X += transVect.X;
            Position.Y += transVect.Y;
            TranslateSprite(transVect);
        }

        public void OnHit () {
            IsAlive = false;
            for (int i = 0; i < visual.Length; i++) {
                Vector2 explosionDirection = visual[i].GetPosition() - Position;
                Vector2 randomStartVelocity = explosionDirection;
                randomStartVelocity.X *= RandomGenerator.GetRandom(4, 15);
                randomStartVelocity.Y *= RandomGenerator.GetRandom(4, 23);
                visual[i].SetVelocity(randomStartVelocity);
                visual[i].SetIsGravity(true);
            }
        }

        private void TranslateSprite (Vector2 transVect) {
            for (int i = 0; i < visual.Length; i++) {
                visual[i].Translate(transVect);
            }
        }

        public int GetWidth () {
            return width;
        }

    }
}
