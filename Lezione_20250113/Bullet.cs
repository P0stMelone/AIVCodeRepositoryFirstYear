using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20250113 {
    public class Bullet {

        private bool enabled;
        public bool Enabled {
            get {
                return enabled;
            }
        }

        public void Shoot () {
            enabled = true;
        }

        public void Despawn () {
            enabled = false;
        }

        public Bullet Clone () {
            return new Bullet();
        }

    }
}
