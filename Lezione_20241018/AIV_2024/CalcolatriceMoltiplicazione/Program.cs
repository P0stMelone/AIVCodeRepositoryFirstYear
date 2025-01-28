using System;

namespace CalcolatriceMoltiplicazione
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string numero1 = Console.ReadLine(); //args[0];
            string numero2 = Console.ReadLine(); //args[1];

            int veroNumero1 = int.Parse(numero1);
            int veroNumero2 = int.Parse(numero2);

            Console.WriteLine(veroNumero1 * veroNumero2);
        }
    }
}
