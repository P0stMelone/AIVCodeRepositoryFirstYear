using System.Collections.Generic;
using OpenTK;
using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    public static class BulletMgr {

        const int poolSize = 20;

        static Queue<Bullet> bullets;
        static Bullet[] bulletsArray;


        public static void Init () {
            bullets = new Queue<Bullet>(poolSize);
            bulletsArray = new Bullet[poolSize];
            Texture blueLaserTexture = new Texture("Assets/blueLaser.png");
            for (int i = 0; i < poolSize; i++) {
                Bullet b = new Bullet(blueLaserTexture, 1, 500);
                bullets.Enqueue(b);
                bulletsArray[i] = b;
            }
        }

        public static Bullet GetBullet () {
            if (bullets.Count == 0) return null;
            Bullet b = bullets.Dequeue();
            return b;
        }

        public static void RestoreBullet (Bullet b) {
            bullets.Enqueue(b);
        }

        public static void Update () {
            foreach(Bullet b in bulletsArray) {
                if (!b.IsActive) continue;
                b.Update();
            }
        }


        public static void Draw () {
            foreach(Bullet b in bulletsArray) {
                if (!b.IsActive) continue;
                b.Draw();
            }
        }


    }
}
