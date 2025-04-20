using OpenTK;

namespace Aiv.Fast2D.Component {
    public class ShootModule : UserComponent {

        private BulletPool bulletPool;

        public ShootModule (GameObject owner): base (owner) {

        }

        public override void Start() {
            bulletPool = GameObject.Find("BulletPool").GetComponent(typeof(BulletPool)) as BulletPool;
        }


        public bool Shoot(BulletType bulletType, Vector2 position, GameObject shotBy) {
            switch(bulletType) {
                case BulletType.GreenGlobe:
                    return TripleShoot(bulletType, position, shotBy);
                default:
                    return SingleShoot(bulletType, position, shotBy);
            }
        }

        private bool TripleShoot (BulletType bulletType, Vector2 position, GameObject shotBy) {
            Vector2 direction = Vector2.One;
            for (int i = 0; i < 3; i++) {
                Bullet bullet = GetBullet(bulletType);
                if (bullet == null) return false;
                bullet.Shoot(position, direction, shotBy);
                direction.Y--;
            }
            return true;
        }

        private bool SingleShoot (BulletType bulletType, Vector2 position, GameObject shotBy) {
            Bullet bullet = GetBullet(bulletType);
            if (bullet == null) return false;
            bullet.Shoot(position, shotBy);
            return true;
        }

        private Bullet GetBullet (BulletType bulletType) {
            return bulletPool.GetBullet(bulletType);
        }
    }
}
