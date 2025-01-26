using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20250120 {
    public class Crocodile : Animal {

        private bool canSpeak;

        public Crocodile(string name, int age, bool canSpeak) : base(name, age) {
            species = "Crocodile";
            this.canSpeak = canSpeak;
        }

        public override void Speak() {
            if (canSpeak) {
                Console.WriteLine($"{Name} dice: il coccodrillo come fa?");
            } else {
                base.Speak();
            }
        }



    }
}
