using System;

namespace Lezione_20250120 {
    public abstract class Animal {

        protected string species;

        public string Name { get; set; }
        public int Age { get; set; }


        public Animal(string name, int age/*, string species*/) {
            Name = name;
            Age = age;
            //this.species = species;
        }

        public abstract void Die();

        protected void DisplaySpeciesInfo () {
            Console.WriteLine($"Specie: {species}");
        }

        public void Eat() {
            Console.WriteLine($"{Name} sta mangiando");
        }

        public void Sleep() {
            Console.WriteLine($"{Name} sta dormendo.");
        }

        public virtual void Speak () {
            Console.WriteLine($"{Name} emette un suono generico");
        }

        public void NotVirtualMethod () {
            Console.WriteLine($"NotVirtualMethod di Animal");
        }

    }
}
