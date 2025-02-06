using System.Collections.Generic;
using OpenTK;
using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    public static class BulletMgr {

        const int poolSize = 20;

        static Queue<Bullet>[] bullets;
        //static Bullet[] bulletsArray;


        public static void Init () {
            bullets = new Queue<Bullet>[(int)BulletType.Last];
            //bulletsArray = new Bullet[poolSize * bullets.Length];
            Texture blueLaserTexture = new Texture();
            Texture greenGlobeTexture = new Texture();
            Texture fireGlobeTexture = new Texture();
            for (int i = 0; i < bullets.Length; i++) {
                bullets[i] = new Queue<Bullet>();
                for (int j = 0; j < poolSize; j++) {
                    Bullet b;
                    switch (i) {
                        case (int)BulletType.FireGlobe:
                            b = new FireGlobe("fireglobe");
                            break;
                        case (int)BulletType.GreenGlobe:
                            b = new GreenGlobe("greenglobe");
                            break;
                        case (int)BulletType.BlueLaser:
                            b = new BlueLaser("bluelaser");
                            break;
                        default:
                            b = new IceGlobe("fireglobe");
                            break;
                    }
                    bullets[i].Enqueue(b);
                    //bulletsArray[j + i * poolSize] = b;
                }
            }
        }

        public static Bullet GetBullet (BulletType type) {
            if (bullets[(int)type].Count == 0) return null;
            return bullets[(int)type].Dequeue();
        }

        public static void RestoreBullet (Bullet b, BulletType type) {
            bullets[(int)type].Enqueue(b);
        }

        //public static void Update () {
        //    foreach(Bullet b in bulletsArray) {
        //        if (!b.IsActive) continue;
        //        b.Update();
        //    }
        //}


        //public static void Draw () {
        //    foreach(Bullet b in bulletsArray) {
        //        if (!b.IsActive) continue;
        //        b.Draw();
        //    }
        //}


    }
}
