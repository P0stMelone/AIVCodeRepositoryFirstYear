using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20250113 {
    public class BulletPool {

        private const int startPoolSize = 30;

        private List<Bullet> pool;

        private Bullet bulletTemplate;

        public BulletPool(Bullet bulletTemplate) {
            this.bulletTemplate = bulletTemplate;
            pool = new List<Bullet>(startPoolSize);
            for (int i = 0; i < startPoolSize; i++) {
                pool.Add(bulletTemplate.Clone());
            }
        }


        public Bullet GetFreeBullet () {
            for (int i = 0; i < pool.Count; i++) {
                if (pool[i].Enabled) continue;
                return pool[i];
            }
            return InternalIncreaseSize();
        }

        private Bullet InternalIncreaseSize () {
            Bullet firstBulletCreated = null;
            for (int i = 0; i < startPoolSize; i++) {
                Bullet newBullet = bulletTemplate.Clone();
                pool.Add(newBullet);
                if (i == 0) firstBulletCreated = newBullet;
            }
            return firstBulletCreated;
        }

    }
}
