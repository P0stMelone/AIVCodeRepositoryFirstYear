using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20241106
{
    struct Scacchiera
    {
        Posizione2D Dimensione;

        public Scacchiera(int _Base, int _Altezza)
        {
            Dimensione.X = _Base < 0 ? 0 : _Base;
            Dimensione.Y = _Altezza < 0 ? 0 : _Altezza;
        }

        public Posizione2D GetSize()
        {
            return Dimensione;
        }

        public void Disegna(Warrior player1, Warrior player2)
        {
            Posizione2D CellaCorrente;

            ConsoleColor consoleColor = Console.BackgroundColor;
            for (CellaCorrente.Y = 0; CellaCorrente.Y < Dimensione.Y; CellaCorrente.Y++)
            {
                bool isBlack = CellaCorrente.Y % 2 == 0;
                for (CellaCorrente.X = 0; CellaCorrente.X < Dimensione.X; CellaCorrente.X++)
                {
                    Console.BackgroundColor = isBlack ? ConsoleColor.Black : ConsoleColor.White;

                    if(CellaCorrente == player1.Posizione)
                    {
                        Console.Write(player1.GetPawn());
                    }
                    else if (CellaCorrente == player2.Posizione)
                    {
                        Console.Write(player2.GetPawn());
                    }
                    else
                    {
                        Console.Write(' ');
                    }

                    isBlack = !isBlack;
                }
                Console.Write('\n');
            }
            Console.BackgroundColor = consoleColor;
        }
    }
}
