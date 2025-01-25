using System;

namespace Lezione_20241111 {
    class Program {
        static void Main(string[] args) {
            //ArrayExample();

            //Esercizio1();

            Esercizio3();

            Console.ReadLine();

        }

        static void ArrayExample () {
            int[] arrayInt = new int[10];
            arrayInt[2] = 3456;
            arrayInt[3] = arrayInt[2];
            arrayInt[3] = 56;
            //arrayInt[2] rimane comunque 3456
            int[] pippo = new int[10];
            for (int i = 0; i < pippo.Length; i++) {
                pippo[i] = arrayInt[i];
            }
            Console.WriteLine(arrayInt[2]);
        }
        
        static void ArrayUsageExample () {
            int[] primiDieciNumeriPositivi = new int[10];
            for (int i = 0; i < 10; i++) {
                primiDieciNumeriPositivi[i] = i;
            }
            for (int i = 9; i >= 0; i--) {
                Console.WriteLine(primiDieciNumeriPositivi[i]);
            }
        }

        static void Esercizio1 () {
            //VERSIONE LEGGIBILE
            int[] numeri = new int[10];
            for (int i = 0; i < numeri.Length; i++) {
                Console.WriteLine("Digita l'elemento numero " + i);
                numeri[i] = int.Parse(Console.ReadLine());
            }
            for (int i = numeri.Length - 1; i >= 0; i--) {
                Console.WriteLine("L'elemento in posizione " + i + " è: " + numeri[i]);
            }
            //VERSIONE BRUTTA MA CHE FA RAGIONAR
            //int[] numeri = new int[5];
            //for (int i = 0; i < numeri.Length; i++) {
            //    Console.WriteLine("Digita l'elemento numero " + i);
            //    numeri[numeri.Length - 1 - i] = int.Parse(Console.ReadLine());
            //}
            //for (int i = 0; i < numeri.Length; i++) {
            //    Console.WriteLine(numeri[i]);
            //}
        }

        static void Esercizio2 () {
            int numeroDiZeri = 0;
            int[] numeri = new int[5];
            for (int i = 0; i < numeri.Length; i++) {
                Console.WriteLine("Inserisci il numero in posizione " + i);
                numeri[i] = int.Parse(Console.ReadLine());
                if (numeri[i] == 0) {
                    numeroDiZeri++;
                }
            }
            for (int i = 0; i < numeri.Length; i++) {
                if (numeri[i] == 0) {
                    Console.WriteLine(numeroDiZeri);
                } else {
                    Console.WriteLine(numeri[i]);
                }
            }
        }


        static void Esercizio3 () {
            int[] arrayGiàInizializzato = new int[] { 1,3,4,4,3,1};
            int primeNumberFound = 0;
            int numberToCheck = 1;
            int[] firstTwentyPrimeNumber = new int[20];
            while (primeNumberFound < 20) {
                if (Primo(numberToCheck)) {
                    firstTwentyPrimeNumber[primeNumberFound] = numberToCheck;
                    primeNumberFound++;
                }
                numberToCheck++;
            }
            for (int i = 0; i < firstTwentyPrimeNumber.Length; i++) {
                Console.WriteLine(firstTwentyPrimeNumber[i]);
            }
        }

        static bool Primo (int number) {
            if (number % 2 == 0 && number != 2) return false;
            int lastNumberToCheck = (int)Math.Sqrt(number);
            for (int i = 2; i <= lastNumberToCheck ; i++) {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
