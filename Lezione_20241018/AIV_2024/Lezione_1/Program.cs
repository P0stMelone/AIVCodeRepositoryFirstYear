using System;

namespace Lezione_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*int numero = 3;
            float numero_con_virgola = 1.3f;
            double numero_grande_con_virgola = 10.5;
            char lettera = 'A';
            string parola = "sella";

            Console.WriteLine(parola.Length);

            numero = numero + 5;

            Console.WriteLine(numero);*/

            /*Console.Write("Ciao ");
            Console.Write(args[0]);
            Console.WriteLine("!");*/

            //Metodo 1
            string numero1 = args[0];//Console.ReadLine();
            string numero2 = args[1];//Console.ReadLine();
            //leggere numero1 e numero2
            double veroNumero1 = double.Parse(numero1); //parse significa tradurre (circa)
            double veroNumero2 = double.Parse(numero2);

            Console.WriteLine(veroNumero1 + veroNumero2);

          


        }
    }
}
