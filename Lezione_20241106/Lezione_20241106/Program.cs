using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20241106 {
    class Program {
        static void Main(string[] args) {
            //NestedExample();
            //Esercizio2();
            //Esercizio3();
            Esercizio4();
            Console.ReadLine();
        }
        
        static void NestedExample() {
            int i;//, j;
            for (i = 0; i < 5; i++) {
                for (int j = 0; j < 3; j++) {
                    Console.WriteLine("i = " + i + "; j = " + j);
                }
            }
        }

        static void Esercizio1() {
            int n = 10;
            for (int i = 0; i < n; i++) {
                for (int j = 0; j <= i; j++) {
                    Console.Write('*');
                }
                Console.Write('\n');
            }
        }

        static void Esercizio2() {
            int b = 12;
            int h = 4;
            for (int i = 0; i < h; i++) {
                for (int j = 0; j < b; j++) {
                    Console.Write('*');
                }
                Console.Write('\n');
            }
        }


        static void Esercizio3 () {
            int h = int.Parse(Console.ReadLine());
            int b = h * 2 - 1;
            int centro = h - 1;
            for (int i = 0; i < h; i++) {
                for (int j = 0; j<b; j ++) {
                    if (j >= centro - i && j <= centro + i) {
                        Console.Write('*');
                    } else {
                        Console.Write('-');
                    }
                }
                Console.Write('\n');
            }
        }

        static void Esercizio4 () {
            int h = 10;
            int b = 12;
            bool isBlack;
            for (int i = 0;  i <h; i++) {
                isBlack = i % 2 == 0;
                for (int j = 0; j < b; j++) {
                    if (isBlack) {
                        Console.BackgroundColor = ConsoleColor.Black;
                    } else {
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    Console.Write(' ');
                    isBlack = !isBlack;
                }
                Console.Write('\n');
            }
        }


    }
}
