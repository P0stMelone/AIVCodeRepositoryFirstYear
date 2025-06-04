using OpenTK;

namespace Aiv.Fast2D.Component {
    public class ShootModule : UserComponent {

        private BulletType bulletType;
        public BulletType BulletType {
            get { return bulletType; }
            set { bulletType = value; }
        }


        private BulletMgr bulletMgr;

        public ShootModule (GameObject owner, BulletType bulletType) : base (owner) {
            this.bulletType = bulletType;
        }

        public override void Awake() {
            bulletMgr = GameObject.Find("BulletMgr").GetComponent<BulletMgr>();
        }

        public bool Shoot (Vector2 startPosition, Vector2 direction) {
            Bullet bullet = bulletMgr.GetBullet(bulletType);
            if (bullet == null) return false;
            bullet.Shoot(startPosition, direction, gameObject);
            return true;
        }

    }
}
