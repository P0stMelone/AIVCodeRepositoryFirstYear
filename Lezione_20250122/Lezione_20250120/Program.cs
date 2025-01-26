using System;

namespace Lezione_20250120 {
    class Program {
        static void Main(string[] args) {

            //Exemple1();
            //ExampleOverride();
            //PolimorfismoBeginner();
            //PolimorfismoLittlePro();
            PolimorfismoPro();
            //WhatIsMissing();
            Console.ReadLine();

        }

        static void Exemple1 () {
            Dog fido = new Dog("Fido", 10);
            Cat romeo = new Cat("Romeo", 2);

            fido.Eat();
            romeo.Eat();

            fido.Bark();
            romeo.Meow();

            fido.ShowDetails();
        }

        static void ExampleOverride () {
            Dog fido = new Dog("Fido", 10);

            fido.Speak();
            fido.NotVirtualMethod();
        }

        static void PolimorfismoBeginner () {
            Animal fido = new Dog("Fido", 10);
            Animal romeo = new Cat("Romeo", 2);
            Animal mino = new Dog("Mino", 30);
            //object gianfranco = new Dog("Giangi", 2);

            fido.Speak();
            romeo.Speak();
            //fido.ToString();
            //gianfranco.ToString();
            fido.NotVirtualMethod();
            Dog fidoDog = fido as Dog;
            if (fidoDog != null) {
                fidoDog.Speak();
                fidoDog.NotVirtualMethod();
                fidoDog.Bark();
            }
            //((Dog)fido).Speak();
            //((Dog)fido).NotVirtualMethod();
            //((Dog)fido).Bark();
            //((Cat)fido).Meow();
            Dog romeoDog = romeo as Dog;
            if (romeoDog != null) {
                romeoDog.Bark();
            }
            try {
                Dog ciaone = (Dog)romeo;
                ciaone.Bark();
            } catch (Exception e) {
                Console.WriteLine("Ops non è un cane");
            }
        }

        static void PolimorfismoLittlePro () {
            Dog fido = new Dog("Fido", 10);
            Cat romeo = new Cat("Romeo", 2);
            Dog mino = new Dog("Mino", 30);
            Human dave = new Human("Dave", 35);
            Crocodile giampiero = new Crocodile("Giampiero", 345, true);
            Crocodile gianfernando = new Crocodile("Gianfernando", 345, false);

            DoTheSpeak(fido);
            DoTheSpeak(romeo);
            DoTheSpeak(mino);
            DoTheSpeak(dave);
            DoTheSpeak(giampiero);
            DoTheSpeak(gianfernando);
        }


        static void PolimorfismoPro () {
            Animal[] animals = new Animal[6];
            animals[0] = new Dog("Fido", 10);
            animals[1] = new Cat("Romeo", 2);
            animals[2] = new Dog("Mino", 30);
            animals[3] = new Human("Dave", 35);
            animals[4] = new Crocodile("Giampiero", 345, true);
            animals[5] = new Crocodile("Gianfernando", 345, false);


            foreach(Animal animal in animals) {
                if (animal is Dog) {
                    Console.WriteLine("Ecco un cane parlante :) !!1! SKAFFALEH");
                }
                animal.Speak();
            }
        }

        static void WhatIsMissing() {
            //Animal genericAniaml = new Animal("Giampippo", 3);
        }

        static void DoTheSpeak (Animal animal) {
            animal.Speak();
        }
    }
}
