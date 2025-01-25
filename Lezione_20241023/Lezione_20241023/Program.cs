using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20241023 {
    class Program {
        static void Main(string[] args) {

            //MaxAndMin();
            //DontTryThisAtHome();
            //WhileExample();
            //Ex1();
            //Ex2();
            //Ex3While();
            //Ex3DoWhile();
            Ex4();

            Console.ReadLine();

        }

        static void MaxAndMin () {
            int n1 = int.Parse(Console.ReadLine());
            int n2 = int.Parse(Console.ReadLine());

            if (n1 > n2) {
                Console.WriteLine("Il numero maggiore è: " + n1);
            } else if (n2 > n1) {
                Console.WriteLine("Il numero maggiore è: " + n2);
            } else {
                Console.WriteLine("Ciaone :)");
            }
            
        }

        static void MaxThree () {
            int n1 = int.Parse(Console.ReadLine());
            int n2 = int.Parse(Console.ReadLine());
            int n3 = int.Parse(Console.ReadLine());

            int max = n1;
            if (n2 > max) {
                max = n2;
            }
            if (n3 > max) {
                max = n3;
            }

            Console.WriteLine("Il numero maggiore è: " + max);
        }

        static void IsRight () {


            int n1 = int.Parse(Console.ReadLine());
            if (n1 % 2 == 0) {
                Console.WriteLine("Il numero è pari");
            } else {
                Console.WriteLine("Il numero è dispari");
            }

        }

        static void PrintNoob () {
            Console.WriteLine("1");
            Console.WriteLine("2");
            Console.WriteLine("3");
            Console.WriteLine("4");
            Console.WriteLine("5");
            Console.WriteLine("6");
            Console.WriteLine("7");
            Console.WriteLine("8");
            Console.WriteLine("9");
            Console.WriteLine("10");
        }

        static void DontTryThisAtHome() {
            int i = 4;
            int j = i++ + ++i;

            Console.WriteLine("i =  " + i + "   j =  " + j);
        }

        static void WhileExample () {
            int i = 1;
            while (i <= 10) {
                Console.WriteLine(i);
                i++;
            }
        }

        static void Ex1 () {
            int n = int.Parse(Console.ReadLine());
            if (n == 0) {
                Console.WriteLine("Il numero che hai inserito è 0");
                return;
            }
            int i = 0;
            while (i <= n) {
                Console.WriteLine(i);
                i++;
            }
        }

        static void Ex2 () {
            Console.WriteLine("Inserisci un numero tra 0 e 2");
            int n = int.Parse(Console.ReadLine());
            while (n >= 0 && n <= 2) {
                if (n == 0) {
                    Console.WriteLine("Ciao");
                } else if (n == 1) {
                    Console.WriteLine("Salve");
                } else if (n == 2) {
                    Console.WriteLine("Buongiorno");
                }
                Console.WriteLine("Inserisci un numero tra 0 e 2");
                n = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("Programma terminato");
        }

        static void Ex2DoWhile () {
            int n;
            do {
                Console.WriteLine("Inserisci un numero tra 0 e 2");
                n = int.Parse(Console.ReadLine());
                if (n == 0) {
                    Console.WriteLine("Ciao");
                } else if (n == 1) {
                    Console.WriteLine("Salve");
                } else if (n == 2) {
                    Console.WriteLine("Buongiorno");
                }
            } while (n >= 0 && n <= 2);

            Console.WriteLine("Programma terminato");
        }

        static void Ex3While () {
            int n = 100;

            while (n >= 0) {
                Console.WriteLine(n);
                //n = n - 2;
                n -= 2;
            }
        }

        static void Ex3DoWhile () {
            int n = 100;
            do {
                Console.WriteLine(n);
                n -= 2;
            } while (n >= 0);
        }

        static void Ex4 () {
            int n;
            string userInput;
            int sum = 0;
            do {
                Console.WriteLine("Inserisci un numero (somma attuale: " + sum +")");
                userInput = Console.ReadLine();
                n = int.Parse(userInput);
                //sum = sum + n;
                sum += n;
            } while (sum != 0);
            Console.WriteLine("Terminato");
        }

    }
}
