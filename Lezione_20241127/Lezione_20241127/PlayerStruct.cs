using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20241127 {
    public struct PlayerStruct {

        int hp;

        public PlayerStruct(int hp) {
            this.hp = hp;
        }

        public void TakeDamage(int damage) {
            hp -= damage;
        }

        public void PrintHp() {
            Console.WriteLine(hp);
        }

    }
}
