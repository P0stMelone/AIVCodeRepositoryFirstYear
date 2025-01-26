using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20250120 {
    public class Human: Animal {

        public Human(string name, int age): base (name, age) {
            species = "Human";
        }

        public override void Speak() {
            Console.WriteLine($"{Name} dice: ciao so parlare, sono un essere intelligente, " +
                $"talmente intelligente che sto per distruggere il mondo grazie al riscaldamento climatico");
        }

    }
}
