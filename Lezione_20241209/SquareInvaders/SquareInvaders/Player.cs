using Aiv.Draw;

namespace SquareInvaders {
    public class Player {

        const float maxSpeed = 180f;

        private Vector2 position;
        private int width;
        private int height;
        private Rect[] playerVisual;
        private float speed;
        private int distToSide;
        private float counter;
        private float shootDelay;

        private BulletPooler pooler;

        private int hp;

        public Player (Vector2 pos, int width, int height, Color col, BulletPooler pooler, int hp) {
            this.pooler = pooler;
            position = pos;
            this.width = width;
            this.height = height;
            this.distToSide = Game.distToSide;
            shootDelay = 0.5f;
            //Disegno
            playerVisual = new Rect[2];
            //base
            playerVisual[0] = new Rect(position.X - width / 2, position.Y,
                width, height / 2, col);
            int cannonWidth = width / 2;
            //cannon
            playerVisual[1] = new Rect(position.X - cannonWidth / 2, position.Y - height / 2,
                cannonWidth, height / 2, col);
            this.hp = hp;
        }

        /// <summary>
        /// This method make the player shoot
        /// </summary>
        private void Shoot () {
            Bullet b = pooler.GetPlayerBullet();
            if (b == null) return;
            b.Shoot(new Vector2(position.X - b.GetWidth() / 2, position.Y - height - b.GetHeight()), 
                new Vector2(0, -100));
            counter = 0;
        }

        public void Input () {
            //Handle movement
            if (GfxTools.Win.GetKey(KeyCode.Right) || GfxTools.Win.GetKey(KeyCode.D)) {
                speed = maxSpeed;
            } else if (GfxTools.Win.GetKey(KeyCode.Left) || GfxTools.Win.GetKey(KeyCode.A)) {
                speed = -maxSpeed;
            } else {
                speed = 0;
            }
            //Handle shoot
            counter += GfxTools.Win.DeltaTime;
            if (counter  < shootDelay) return;
            if (!GfxTools.Win.GetKey(KeyCode.Space)) return;
            Shoot();
        }

        public void Update () {
            float deltaX = speed * GfxTools.Win.DeltaTime;
            position.X += deltaX;

            float maxX = position.X + width / 2;
            float minX = position.X - width / 2;

            if (maxX > GfxTools.Win.Width - distToSide) {
                float overflowX = maxX - (GfxTools.Win.Width - distToSide);
                position.X -= overflowX;
                deltaX -= overflowX;
            } else if (minX < distToSide) {
                float overflowX = distToSide - minX;
                position.X += overflowX;
                deltaX += overflowX;
            }
            for (int i = 0; i < playerVisual.Length; i++) {
                playerVisual[i].Translate(deltaX, 0);
            }
        }

        public void Draw () {
            for (int i = 0; i < playerVisual.Length; i++) {
                playerVisual[i].Draw();
            }
        }

        public Vector2 GetPosition () {
            return position;
        }

        public int GetWidth () {
            return width;
        }

        public bool IsAlive () {
            return hp > 0;
        }

        public void OnHit () {
            hp--;
        }
    }
}
