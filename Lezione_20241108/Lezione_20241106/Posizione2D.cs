using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20241106
{
    struct Posizione2D
    {
        public int X;
        public int Y;

        public static readonly Posizione2D Zero = new Posizione2D(0, 0);

        public static Posizione2D Rand(int minX, int minY, int maxX, int maxY)
        {
            Random rng = new Random();
            return new Posizione2D(rng.Next(minX, maxX), rng.Next(minY, maxY));
        }

        public static Posizione2D Rand(int maxX, int maxY)
        {
            return Rand(0, 0, maxX, maxY);
        }

        public static Posizione2D Rand(Posizione2D min, Posizione2D max)
        {
            return Rand(min.X, min.Y, max.X, max.Y);
        }

        public static Posizione2D Rand(Posizione2D max)
        {
            return Rand(Zero, max);
        }

        public static Posizione2D Rand()
        {
            return Rand(0, 0, Int32.MaxValue, Int32.MaxValue);
        }

        public Posizione2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        // (Lhs == Rhs) -> true se Lhs.X == Rhs.X e Lhs.Y == Rhs.Y
        public static bool operator==(Posizione2D Lhs, Posizione2D Rhs)
        {
            return Lhs.X == Rhs.X && Lhs.Y == Rhs.Y;
        }

        public static bool operator!=(Posizione2D Lhs, Posizione2D Rhs)
        {
            return !(Lhs == Rhs);
        }

        public static Posizione2D operator+(Posizione2D Lhs, Posizione2D Rhs)
        {
            return new Posizione2D(Lhs.X + Rhs.X, Lhs.Y + Rhs.Y);
        }

        public static Posizione2D operator-(Posizione2D Lhs)
        {
            return new Posizione2D(-Lhs.X, -Lhs.Y);
        }

        public static Posizione2D operator-(Posizione2D Lhs, Posizione2D Rhs)
        {
            return Lhs + -Rhs;
        }

        public static Posizione2D operator*(Posizione2D Lhs, Posizione2D Rhs)
        {
            return new Posizione2D(Lhs.X * Rhs.X, Lhs.Y * Rhs.Y);
        }

        public static Posizione2D operator*(Posizione2D Lhs, int Rhs)
        {
            return new Posizione2D(Lhs.X * Rhs, Lhs.Y * Rhs);
        }

        public static Posizione2D operator/(Posizione2D Lhs, Posizione2D Rhs)
        {
            return new Posizione2D(Lhs.X / Rhs.X, Lhs.Y / Rhs.Y);
        }

        public static Posizione2D operator/(Posizione2D Lhs, int Rhs)
        {
            return new Posizione2D(Lhs.X / Rhs, Lhs.Y / Rhs);
        }

    }
}
