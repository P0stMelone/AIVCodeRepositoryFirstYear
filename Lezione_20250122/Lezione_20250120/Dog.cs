using System;

namespace Lezione_20250120 {
    public class Dog : Animal {

        public Dog(string name, int age) : base (name, age) {
            species = "Cane";
        }

        public void ShowDetails () {
            Console.WriteLine($"Nome: {Name} Età: {Age}");
            DisplaySpeciesInfo();
        }

        public void Bark () {
            Console.WriteLine($"{Name} dice: Bau!");
        }

        public override void Speak() {
            Console.WriteLine($"{Name} dice: Bau!");
        }

        public new void NotVirtualMethod () {
            Console.WriteLine($"NotVirtualMethod di dog");
        }

        public override void Die() {
            Console.WriteLine($"{Name} mortissimo!");
        }
    }
}
