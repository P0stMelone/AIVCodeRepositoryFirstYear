using OpenTK;

namespace Aiv.Fast2D.Component {
    public class ShootModule : UserComponent {

        private SpriteRenderer sr;
        private BulletMgr bm;

        public ShootModule(GameObject owner) : base (owner) {

        }

        public override void Awake() {
            sr = GetComponent<SpriteRenderer>();
            bm = GameObject.Find("BulletMgr").GetComponent<BulletMgr>();
        }

        public bool Shoot (BulletType type) {
            Bullet bullet = bm.GetBullet(type);
            if (bm == null) {
                return false;
            }
            bullet.ShootMe(transform.Position + sr.Width / 2f * transform.Forward, transform.Forward);
            return true;
        }

    }
}
