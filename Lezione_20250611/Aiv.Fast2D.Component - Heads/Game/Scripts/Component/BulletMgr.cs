using OpenTK;
namespace Aiv.Fast2D.Component {

    public enum BulletType {
        Player,
        Enemy,
        Last
    }

    public class BulletMgr : UserComponent {

        private Bullet[,] bullets;

        public BulletMgr (GameObject owner, int numberOfBullets) : base (owner) {
            bullets = new Bullet[(int)BulletType.Last, numberOfBullets];
            for (int i = 0; i < bullets.GetLength(0); i++) {
                for (int j = 0; j < bullets.GetLength(1); j++) {
                    bullets[i, j] = i == 0 ? CreatePlayerBullet(j) : CreateEnemyBullet(j);
                }
            }
        }

        public Bullet GetBullet (BulletType type) {
            for (int i = 0; i < bullets.GetLength(1); i++) {
                if (bullets[(uint)type, i].gameObject.IsActive) continue;
                return bullets[(uint)type, i];
            }
            return null;
        }

        private Bullet CreatePlayerBullet (int index) {
            GameObject pb = GameObject.CreateGameObject("PlayerBullet_" + index, Vector2.Zero);
            pb.Layer = (uint)Layer.PlayerBullet;
            pb.AddCollisionLayer((uint)Layer.Enemy);
            pb.AddCollisionLayer((uint)Layer.EnemyBullet);
            pb.AddComponent<Rigidbody>();
            pb.AddComponent(new SpriteRenderer(pb, "Fireball", Vector2.One / 2, DrawLayer.Playground));
            pb.AddComponent(ColliderFactory.CreateCircleFor(pb));
            pb.AddComponent(new Bullet(pb, 15, 5));
            return pb.GetComponent<Bullet>();
        }

        private Bullet CreateEnemyBullet(int index) {
            GameObject pb = GameObject.CreateGameObject("PlayerBullet_" + index, Vector2.Zero);
            pb.Layer = (uint)Layer.EnemyBullet;
            pb.AddCollisionLayer((uint)Layer.Player);
            pb.AddComponent<Rigidbody>();
            pb.AddComponent(new SpriteRenderer(pb, "Fireball", Vector2.One / 2, DrawLayer.Playground));
            pb.AddComponent(ColliderFactory.CreateCircleFor(pb));
            pb.AddComponent(new Bullet(pb, 15, 5));
            return pb.GetComponent<Bullet>();
        }


    }
}
