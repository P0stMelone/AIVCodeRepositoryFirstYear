using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20241211_Debug {
    class Program {
        static void Main(string[] args) {

            Debug();

        }

        static void Debug () {
            string userInput = Console.ReadLine();
            int number = int.Parse(userInput);
            Func1();
            Func2(ref number);
            bool isRight =  Func3(number);
            Console.WriteLine("Fine bro");
        }


        static void Func1 () {
            Console.WriteLine("Salve");
        }


        static void Func2 (ref int number) {
            number++;
        }

        static bool Func3 (int number) {
            return number % 2 == 0;
        }
    }


}
