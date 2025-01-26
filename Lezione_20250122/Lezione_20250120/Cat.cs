using System;

namespace Lezione_20250120 {
    public class Cat : Animal {

        public Cat (string name, int age) : base (name, age) {
            species = "Gatto";
        }

        public void Meow () {
            Console.WriteLine($"{Name} dice: Miao!");
        }

        public override void Speak() {
            Console.WriteLine($"{Name} dice: Miao!");
        }

        public override void Die() {
            Console.WriteLine("maaaaaaaaaaaaaaaaorto");
        }

    }
}
