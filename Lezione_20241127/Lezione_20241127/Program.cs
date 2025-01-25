using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20241127 {
    class Program {
        static void Main(string[] args) {
            //MyClassUsage();
            PlayerClassVsStruct();

            Console.ReadLine();
        }


        static void IncrementNumber (int number) {
            number++;
        }

        
        static void MyClassUsage () {
            MyClass variable = new MyClass(32);
            MyClass variable2 = new MyClass(28);
            variable.IncrementNumero();
            (new MyClass(120)).IncrementNumero();
            variable = variable2;
            GC.Collect();
        }

        static void PlayerClassVsStruct () {
            PlayerClass p1Class = new PlayerClass(40);
            PlayerClass p2Class = p1Class;

            PlayerStruct p1Struct = new PlayerStruct(90);
            PlayerStruct p2Struct = p1Struct;

            p1Class.TakeDamage(10);
            p1Struct.TakeDamage(5);

            Console.WriteLine("Sono un mago, perché gli hp di p2Class sono esattamente 30.");
            p2Class.PrintHp();
            Console.WriteLine("Sono un mago, perché gli hp di p2Struct sono esattamente 90.");
            p2Struct.PrintHp();
        }

        static void CarUsage () {
            Car golfy = new Car(80, "Volkswagen", "Golf", new Color(50, 50, 50), CarType._5_door);
            golfy.SetBrand (string.Empty);
            golfy.SetHorsePower(-50);
            Car tiHoFregato = new Car(-101010,"", " ", new Color(50, 50, 50), CarType._3_door);
        }
    }
}
