using OpenTK;

namespace Lezione_20250113 {
    public class Gun {

        private string bulletName;
        private Pooler pooler;
        
        public Gun (string bulletName, Bullet bulletTemplate, Pooler pooler) {
            this.bulletName = bulletName;
            this.pooler = pooler;
            pooler.AddPoolOfBullets(bulletName, bulletTemplate);
        }

        public void Shoot (Vector3 startBulletpostion, Vector3 bulletVelocity) {
            Bullet bullet = pooler.GetBullet(bulletName);
            if (bullet == null) return; //lanciamo una eccezione, significa che quest'arma non ha richiesto la sua pool
            bullet.Shoot();
        }

    }
}
