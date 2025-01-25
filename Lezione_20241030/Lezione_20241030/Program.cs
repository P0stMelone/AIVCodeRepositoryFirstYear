
using System;

namespace Lezione_20241030 {
    class Program {
        static void Main(string[] args) {

            //EsercizioPerCasa();
            //ArgomentiDiDefaultExample();
            Esercizio5();
            Console.ReadLine();
        }


        #region EsercizioPerCasa

        static void EsercizioPerCasa () {
            //guardate la registrazione
        }

        #endregion


        #region ArgomentiDiDefaultExample 

        static void ArgomentiDiDefaultExample() {
            ArgomentiDiDefault();
            ArgomentiDiDefault(25);
        }
        static void ArgomentiDiDefault(int argomentoDiDefault = 5) {
            Console.WriteLine(argomentoDiDefault);
        }

        static void ArgomentiDiDefaultOk(int i1, int i2, int i3, string geppeto,
            float default1 = 23, float def2 = 33) {

        }

        //static void ArgomentiDiDefaultNonOk(int i1 = 1, int i2) {

        //}

        #endregion

        #region Esercizio1

        static bool IsRight (int number) {
            return number % 2 == 0;
        }

        #endregion

        #region Esercizio2

        static bool IsOdd_1(int number) {
            return number % 2 == 1;
        }

        static bool IsOdd_2(int number) {
            return number % 2 != 0;
        }

        static bool IsOdd_3(int number) {
            return !(number % 2 == 0);
        }

        static bool IsOdd_4(int number) {
            return !IsRight(number);
        }

        #endregion

        #region Esercizio3

        static bool IsPrime (int number) {
            if (number == 0) return false;
            if (number == 1) return true;
            for (int i = 2; i < number; i++) {
                if (number % i == 0) {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Esercizio4

        static bool FirstAmongThem (int n1, int n2) {
            if (n1 == 0 || n2 == 0) return false;
            if (n1 == n2) return true;
            if (n1 == 1 || n2 == 1) return true;
            int min = n1 < n2 ? n1 : n2; //unico operatore ternario di c#,
            //ed è esattamente uguale all'if che c'è scritto sotto.
            //if (n1 < n2) {
            //    min = n1;
            //} else {
            //    min = n2;
            //}
            for (int i = 2; i <= min; i++) {
                //if (min%i == 0) {
                //    if (max%i == 0) {
                //        return false;
                //    }
                //}
                if (n1 % i != 0) continue;
                if (n2 % i == 0) return false;
            }
            return true;
        }

        #endregion

        #region Esercizio5

        static void Esercizio5() {
            int n = int.Parse(Console.ReadLine());
            int primeNumberFound = 0;
            int numberToCheck = 1;
            while (primeNumberFound < n) {
                if (IsPrime(numberToCheck)) {
                    Console.WriteLine(numberToCheck);
                    primeNumberFound++;
                }
                numberToCheck++;
            }
        }
        static int Factorial (int n) {
            if (n == 0) return 1;
            return n * Factorial(n - 1);
        }

        #endregion


    }
}
