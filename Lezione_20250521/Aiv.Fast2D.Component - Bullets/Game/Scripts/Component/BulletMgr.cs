using OpenTK;

namespace Aiv.Fast2D.Component {
    public class BulletMgr : UserComponent {

        private Bullet[,] bullets;
        int poolSize;

        public BulletMgr(GameObject owner, int poolSize) : base (owner) {
            this.poolSize = poolSize;
        }

        public void Init () {
            bullets = new Bullet[(int)BulletType.Last, poolSize];
            for (int i = 0; i < bullets.GetLength(0); i++) {
                for (int j = 0; j< bullets.GetLength(1); j++ ) {
                    switch(i) {
                        case 0:
                            bullets[i, j] = CreateGreenGlobe(j);
                            break;
                        case 1:
                            bullets[i, j] = CreateRedGlobe(j);
                            break;
                    }
                }
            }
        }
        public Bullet GetBullet (BulletType type) {
            for (int i = 0; i < bullets.GetLength(1); i++) {
                if (bullets[(int)type, i].gameObject.IsActive) continue;
                return bullets[(int)type, i];
            }
            return null;
        }

        private Bullet CreateGreenGlobe(int index) {
            GameObject bullet = GameObject.CreateGameObject("GreenGlobe_" + index, Vector2.Zero);
            bullet.Tag = "PlayerBullet";
            bullet.AddComponent(new SpriteRenderer
                (bullet, "GreenGlobe", new Vector2(0.1f, 0.5f), DrawLayer.Playground));
            bullet.AddComponent<Rigidbody>();
            bullet.Layer = (uint)Layer.PlayerBullet;
            bullet.AddCollisionLayer((uint)Layer.Tile);
            bullet.AddCollisionLayer((uint)Layer.Enemy);
            bullet.AddCollisionLayer((uint)Layer.EnemyBullet);
            bullet.AddComponent(ColliderFactory.CreateCircleFor(bullet, 0.09f));
            return bullet.AddComponent<Bullet>(BulletType.GreenGlobe, 100, 10);
        }

        private Bullet CreateRedGlobe (int index) {
            GameObject bullet = GameObject.CreateGameObject("RedGlobe_" + index, Vector2.Zero);
            bullet.Tag = "EnemyBullet";
            bullet.AddComponent(new SpriteRenderer
                (bullet, "GreenGlobe", new Vector2(0.1f, 0.5f), DrawLayer.Playground));
            bullet.GetComponent<SpriteRenderer>().Sprite.SetMultiplyTint(1, 0.1f, 0.2f, 1);
            bullet.AddComponent<Rigidbody>();
            bullet.Layer = (uint)Layer.EnemyBullet;
            bullet.AddCollisionLayer((uint)Layer.Tile);
            bullet.AddCollisionLayer((uint)Layer.Player);
            bullet.AddComponent(ColliderFactory.CreateCircleFor(bullet, 0.09f));
            return bullet.AddComponent<Bullet>(BulletType.RedGlobe, 100, 10);
        }

    }
}
