using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20250113 {
    public class Pooler {

        private Dictionary<string, BulletPool> bulletsPool;

        public Pooler () {
            bulletsPool = new Dictionary<string, BulletPool>();
        }

        public void AddPoolOfBullets(string bulletName, Bullet bulletTemplate) {
            if (bulletsPool.ContainsKey(bulletName)) return;
            bulletsPool.Add(bulletName, new BulletPool(bulletTemplate));
        }

        public void RemovePoolOfBullets (string bulletName) {
            if (!bulletsPool.ContainsKey(bulletName)) return;
            bulletsPool.Remove(bulletName);
        }

        public Bullet GetBullet (string bulletName) {
            if (!bulletsPool.ContainsKey(bulletName)) return null;
            return bulletsPool[bulletName].GetFreeBullet();
        }
        
    }
}
