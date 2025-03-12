using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayerBullet : Bullet{

        public PlayerBullet(GameObject owner, BulletType bulletType, int damage, float speed) :
            base(owner, bulletType, damage, speed) {

        }

        public override void Start() {
            base.Start();
            gameObject.Layer = (uint)Layer.PlayerBullet;
            gameObject.AddCollisionLayer((uint)Layer.Enemy);
            gameObject.AddCollisionLayer((uint)Layer.EnemyBullet);
            rb.Velocity = new Vector2(speed, 0);
        }

        public override void LateUpdate() {
            if (sr.TopLeft.X > Game.Win.Width) {
                DestroyMe();
            }
        }

        public override void OnCollide(GameObject other) {
            //collision con enemy o enemybullet
        }

    }
}
