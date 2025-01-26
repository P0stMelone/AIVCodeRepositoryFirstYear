using Aiv.Draw;

namespace SquareInvaders {
    public class Player {

        const float maxSpeed = 180f;

        private Vector2 position;
        private int width;
        private int height;
        private Rect baseRect;
        private Rect cannonRect;
        private float speed;
        private int distToSide;
        private float counter;
        private float shootDelay;

        //ObjectPooling
        private Bullet[] bullets;

        public Player (Vector2 pos, int width, int height, Color col, int distToSide) {
            position = pos;
            this.width = width;
            this.height = height;
            this.distToSide = distToSide;
            shootDelay = 0.5f;
            //Disegno
            baseRect = new Rect(position.X - width / 2, position.Y - height / 2,
                width, height / 2, col);
            int cannonWidth = width / 2;
            cannonRect = new Rect(position.X - cannonWidth / 2, position.Y - height,
                cannonWidth, height / 2, col);
            //Pool
            bullets = new Bullet[30];
            Color bulletColor = new Color(200, 0, 0);
            for (int i = 0; i < bullets.Length; i++) {
                bullets[i] = new Bullet(10, 20, bulletColor);
            }
        }
        
        private Bullet GetFreeBullet () {
            for (int i = 0; i < bullets.Length; i++) {
                if (!bullets[i].IsAlive) {
                    return bullets[i];
                }
            }
            return null; //una pool di un elemnto di gameplay dovrebbe essere in grado di estendere la pool.
        }

        private void Shoot () {
            Bullet b = GetFreeBullet();
            if (b == null) return;
            b.Shoot(new Vector2(position.X - b.GetWidth() / 2, position.Y - height - b.GetHeight() / 2), 
                new Vector2(0, -250));
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

            baseRect.Translate(deltaX, 0);
            cannonRect.Translate(deltaX, 0);

            for (int i = 0; i < bullets.Length; i++) {
                bullets[i].Update();
            }
        }

        public void Draw () {
            baseRect.Draw();
            cannonRect.Draw();

            for (int i = 0; i< bullets.Length; i++) {
                bullets[i].Draw();
            }
        }
    }
}
