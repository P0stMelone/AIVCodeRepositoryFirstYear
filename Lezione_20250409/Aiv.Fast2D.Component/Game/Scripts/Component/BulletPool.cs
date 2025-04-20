using OpenTK;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public class BulletPool : UserComponent {

        private Bullet[,] bullets;

        public BulletPool (GameObject owner, int poolSize) : base (owner) {
            Init(poolSize);
        }

        public void Init(int poolSize) {
            bullets = new Bullet[(int)BulletType.Last, poolSize];
            for (int i = 0; i < (int)BulletType.Last; i++) {
                for (int j = 0; j< poolSize; j++) {
                    switch ((BulletType) i) {
                        case BulletType.BlueLaser:
                            bullets[i, j] = CreateBulletBlueLaser(j);
                            break;
                        case BulletType.GreenGlobe:
                            bullets[i, j] = CreateGreenGlobe(j);
                            break;
                        case BulletType.FireGlobe:
                            bullets[i, j] = CreateFireGlobe(j);
                            break;
                        case BulletType.IceGlobe:
                            bullets[i, j] = CreateIceGlobe(j);
                            break;
                    }
                    bullets[i, j].gameObject.Tag = "EnemyBullet";
                }
            }
        }

        #region BulletFactory
        private Bullet CreateBulletBlueLaser (int index) {
            GameObject temp = GameObject.CreateGameObject("blueLaser_" + index, Vector2.Zero);
            temp.AddComponent(new SpriteRenderer(temp, "blueLaser", new Vector2(0.1f, 0.5f), DrawLayer.Playground));
            temp.AddComponent(ColliderFactory.CreateCircleFor(temp));
            temp.AddComponent(typeof(Rigidbody));
            temp.AddComponent(new PlayerBullet(temp, BulletType.BlueLaser, 25, 500));
            return temp.GetComponent(typeof(PlayerBullet)) as PlayerBullet;
        }

        private Bullet CreateGreenGlobe(int index) {
            GameObject temp = GameObject.CreateGameObject("greenGlobe_" + index, Vector2.Zero);
            temp.AddComponent(new SpriteRenderer(temp, "greenGlobe", new Vector2(0.1f, 0.5f), DrawLayer.Playground));
            temp.AddComponent(ColliderFactory.CreateCircleFor(temp));
            temp.AddComponent(typeof(Rigidbody));
            temp.AddComponent(new PlayerBullet(temp, BulletType.GreenGlobe, 30, 600));
            return temp.GetComponent(typeof(PlayerBullet)) as PlayerBullet;
        }

        private Bullet CreateFireGlobe(int index) {
            GameObject temp = GameObject.CreateGameObject("fireGlobe_" + index, Vector2.Zero);
            temp.AddComponent(new SpriteRenderer(temp, "fireGlobe", new Vector2(0.1f, 0.5f), DrawLayer.Playground));
            temp.AddComponent(ColliderFactory.CreateCircleFor(temp));
            temp.AddComponent(typeof(Rigidbody));
            temp.AddComponent(new EnemyBullet(temp, BulletType.FireGlobe, 20, 500));
            return temp.GetComponent(typeof(EnemyBullet)) as EnemyBullet;
        }

        private Bullet CreateIceGlobe(int index) {
            GameObject temp = GameObject.CreateGameObject("iceGlobe_" + index, Vector2.Zero);
            SpriteRenderer sr = new SpriteRenderer(temp, "fireGlobe", new Vector2(0.1f, 0.5f), DrawLayer.Playground);
            temp.AddComponent(sr);
            sr.SetAdditiveColor(new Vector4(128, 255, 255, 0));
            temp.AddComponent(ColliderFactory.CreateCircleFor(temp));
            temp.AddComponent(typeof(Rigidbody));
            temp.AddComponent(new EnemyBullet(temp, BulletType.IceGlobe, 15, 500));
            return temp.GetComponent(typeof(EnemyBullet)) as EnemyBullet;
        }
        #endregion

        public Bullet GetBullet (BulletType type) {
            int bulletType = (int)type;
            for (int i = 0; i < bullets.GetLength(1); i++) {
                if (!bullets[bulletType, i].gameObject.IsActive) return bullets[bulletType, i];
            }
            return null;
        }
    }
}
