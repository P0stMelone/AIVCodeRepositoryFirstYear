using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20241127 {
    public class PlayerClass {

        int hp;
        PlayerAttributes attributes;

        public PlayerClass(int hp) {
            this.hp = hp;
        }

        internal void TakeDamage (int damage) {
            hp -= damage;
        }

        public void PrintHp () {
            Console.WriteLine(hp);
        }

        private class PlayerAttributes {

        }

    }
}
