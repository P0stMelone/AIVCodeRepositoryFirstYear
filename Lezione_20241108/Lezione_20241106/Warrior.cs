using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20241106
{
    struct Warrior
    {
        public struct MovementInputBindings
        {
            public ConsoleKey Up;
            public ConsoleKey Down;
            public ConsoleKey Left;
            public ConsoleKey Right;
        }

        public Posizione2D SpawnPosition;
        public Posizione2D Posizione;
        public string Nome;
        public MovementInputBindings MovementBindings;

        int Hp;

        public Warrior(string Nome, int Hp, Posizione2D Posizione, MovementInputBindings MovementBindings)
        {
            SpawnPosition = Posizione;
            this.Posizione = Posizione;
            this.Hp = Hp < 0 ? 0 : Hp;
            this.Nome = Nome;
            this.MovementBindings = MovementBindings;
        }

        public bool ProcessInput(ConsoleKey KeyPressed)
        {
            if(KeyPressed == MovementBindings.Up)
            {
                Posizione.Y--;
                return true;
            }
            else if (KeyPressed == MovementBindings.Down)
            {
                Posizione.Y++;
                return true;
            }
            else if (KeyPressed == MovementBindings.Left)
            {
                Posizione.X--;
                return true;
            }
            else if (KeyPressed == MovementBindings.Right)
            {
                Posizione.X++;
                return true;
            }
            return false;
        }

        public void Respawn()
        {
            Posizione = SpawnPosition;
        }

        public bool IsAlive()
        {
            return Hp > 0;
        }

        public bool IsDead()
        {
            return Hp == 0;
        }

        public int GetHP()
        {
            return Hp;
        }

        public void Damage(int damage)
        {
            Hp -= damage;
            if(Hp < 0)
            {
                Hp = 0;
            }
        }

        public char GetPawn()
        {
            return Nome[0];
        }

        public string Status()
        {
            return Nome + ": " + Hp;
        }
    }
}
