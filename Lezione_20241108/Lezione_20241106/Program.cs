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
            Random rng = new Random();

            Scacchiera scacchiera = new Scacchiera(rng.Next(10, 15), rng.Next(10, 15));

            Posizione2D SpawnP1 = Posizione2D.Rand(scacchiera.GetSize());
            Posizione2D SpawnP2 = Posizione2D.Rand(scacchiera.GetSize());

            // evitiamo che P1 e P2 spawnino nello stesso posto
            while(SpawnP1 == SpawnP2)
            {
                SpawnP2 = Posizione2D.Rand(scacchiera.GetSize());
            }

            Warrior.MovementInputBindings bindingsP1 = new Warrior.MovementInputBindings
            {
                Down = ConsoleKey.S,
                Up = ConsoleKey.W,
                Left = ConsoleKey.A,
                Right = ConsoleKey.D
            };
            Warrior.MovementInputBindings bindingsP2 = new Warrior.MovementInputBindings
            {
                Down = ConsoleKey.DownArrow,
                Up = ConsoleKey.UpArrow,
                Left = ConsoleKey.LeftArrow,
                Right = ConsoleKey.RightArrow
            };


            Warrior p1 = new Warrior("Pippo", rng.Next(5, 10), SpawnP1, bindingsP1);
            Warrior p2 = new Warrior("Tommaso", rng.Next(5, 10), SpawnP2, bindingsP2);

            // Buff buff = new Buff(roba che serve al buff);

            ConsoleKey input;
            do
            {
                Console.WriteLine(p1.Status() + '\t' + p2.Status());

                scacchiera.Disegna(p1, p2); // scacchier.Disegna(p1, p2, buff);
                input = Console.ReadKey().Key;

                bool bIsP1 = p1.ProcessInput(input);
                bool bIsP2 = p2.ProcessInput(input);

                if(p1.Posizione == p2.Posizione)
                {
                    if(bIsP1)
                    {
                        p2.Damage(1);
                        p2.Respawn();
                    }
                    else if(bIsP2)
                    {
                        p1.Damage(1);
                        p1.Respawn();
                    }
                }

                /*
                 * if(p1.Posizione == buff.Posizione)
                 * {
                 *     p1 = buff.DoBuff(p1);
                 * }
                 * else if(p2.Posizione == buff.Posizione)
                 * {
                 *     p2 = buff.DoBuff(p2);
                 * }
                 */

                Console.Clear();
            } while (input != ConsoleKey.Escape && p1.IsAlive() && p2.IsAlive());

            if(p1.IsAlive())
            {
                Console.WriteLine(p1.Nome + " wins");
            }
            else
            {
                Console.WriteLine(p2.Nome + " wins");
            }
        }

    }
}
