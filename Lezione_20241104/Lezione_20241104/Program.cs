using System;

namespace Lezione_20241104 {
    class Program {
        static void Main(string[] args) {
            //FactorialUsage();
            //QueuedFactorialRecursiveUsage();
            //FibonacciUsage();
            //QueuedFibonacciRecursiveUsage();
            //fStatementExample();
            MaxTra2o4();
            Console.ReadLine();
        }

        #region Ricorsione&RicorsioneInCoda

        static void FactorialUsage () {
            Console.WriteLine("Inserisci il numero di cui vuoi sapere il fattoriale");
            long number = long.Parse(Console.ReadLine());
            long factorial = FactorialRecursive(number);
            Console.WriteLine("Il fattoriale di " + number + " è: " + factorial);
        }

        static long FactorialRecursive (long n) {
            if (n == 0) return 1;
            return n * (FactorialRecursive(n - 1));
        }

        static void QueuedFactorialRecursiveUsage () {
            Console.WriteLine("Inserisci il numero di cui vuoi sapere il fattoriale (in coda)");
            long number = long.Parse(Console.ReadLine());
            long factorial = QueuedFactorialRecursive(number, 1);
            Console.WriteLine("Il fattoriale di " + number + " è: " + factorial);
        }

        static long QueuedFactorialRecursive (long n, long result) {
            if (n == 0) return result;
            result = n * result;
            return QueuedFactorialRecursive(n - 1, result);
        }

        static void FibonacciUsage () {
            Console.WriteLine("Inserisci il numero di cui vuoi sapere il fibonacci");
            int number = int.Parse(Console.ReadLine());
            int fibonacci = FibonacciRecursive(number);
            Console.WriteLine("Il numero " + number + " della serie di fibonacci è: " + fibonacci);
        }

        static int FibonacciRecursive (int n) {
            if (n == 1 || n == 2) return 1;
            return FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2);
        }

        static void QueuedFibonacciRecursiveUsage () {
            Console.WriteLine("Inserisci il numero di cui vuoi sapere il fibonacci (in coda)");
            int number = int.Parse(Console.ReadLine());
            int fibonacci = QueuedFibonacciRecursive(number, 1,0,0 );
            Console.WriteLine("Il numero " + number + " della serie di fibonacci è: " + fibonacci);

        }

        static int QueuedFibonacciRecursive (int startingN, int currentFibonacciNumber, 
            int fib_1, int fib_2) {
            if (startingN <= 2) {
                return 1;
            }
            if (currentFibonacciNumber == startingN) {
                return fib_1 + fib_2;
            }
            if (currentFibonacciNumber == 2) {
                return (QueuedFibonacciRecursive(startingN, currentFibonacciNumber +1, 1,1));
            }
            int temp = fib_1;
            fib_1 = fib_1 + fib_2;
            fib_2 = temp;
            return QueuedFibonacciRecursive(startingN, currentFibonacciNumber + 1, fib_1, fib_2);
        }

        #endregion

        #region SwitchCaseIntroduzione
        static void IfStatementExample () {
            int userInput;
            do {
                Console.WriteLine("Cosa vuoi utilizzare? (1. Fattoriale, 2. Fattoriale in coda, 3. Fibonacci, " +
                    "4. Fibonacci in coda, -1 per uscire)");
                userInput = int.Parse(Console.ReadLine());
                if (userInput == 1) {
                    FactorialUsage();
                } else  if (userInput == 2) {
                    QueuedFactorialRecursiveUsage();
                } else if (userInput == 3) {
                    FibonacciUsage();
                } else if (userInput == 4) {
                    QueuedFibonacciRecursiveUsage();
                } else {
                    userInput = -1;
                }
            } while (userInput != -1);

        }

        static void SwitchCaseStatementExample() {
            int userInput;
            do {
                Console.WriteLine("Cosa vuoi utilizzare? (1. Fattoriale, 2. Fattoriale in coda, 3. Fibonacci, " +
                    "4. Fibonacci in coda, -1 per uscire)");
                userInput = int.Parse(Console.ReadLine());
                switch (userInput) {
                    case 1:
                        FactorialUsage();
                        break;
                    case 2:
                        QueuedFactorialRecursiveUsage();
                        break;
                    case 3:
                        FibonacciUsage();
                        break;
                    case 4:
                        QueuedFibonacciRecursiveUsage();
                        break;
                    default:
                        userInput = -1;
                        break;
                }
            } while (userInput != -1);

        }

        static void WhyBreakExample () {
            int randomNumber =5; //ma potrebbe essere un valore qualsiasi tra 1 e 5
            switch (randomNumber) {
                case 1:
                    //fai qualcosa;
                    break;
                case 2:
                    //fai qualcosa;
                    break;
                case 3:
                case 4:
                case 6:
                    //fai questo;
                    break;
                case 5:
                    //fai quest altro
                    break;
                default:
                    //lancia un errore;
                    break;
            }
         }

        #endregion

        #region Calcolatrice con Switch Case

        static void Calculator() {
            char op;
            bool keepLooping = true;
            do {
                Console.Write("Inserisci l'operatore (q per uscire)");
                op = Console.ReadKey().KeyChar;
                //if (op == 'q' || op == 'Q') break;
                Console.Write(Environment.NewLine);
                //if (op == '+') {
                //    PerformSum();
                //} else if (op == '-') {
                //    PerformSub();
                //} else if (op == '*') {
                //    PerformMul();
                //} else if (op == '/') {
                //    PerformDiv();
                //}
                switch (op) {
                    case '+':
                        PerformSum();
                        break;
                    case '-':
                        PerformSub();
                        break;
                    case '*':
                        PerformMul();
                        break;
                    case '/':
                        PerformDiv();
                        break;
                    default:
                        keepLooping = false;
                        break;
                }
            } while (keepLooping/*true*/);
        }

        static void PerformSum() {
            int a = AskInteger("Inserisci il primo addendo");
            int b = AskInteger("Inserisci il secondo addendo");
            Console.WriteLine("Il risultato della somma è: " + Sum(a, b));
        }

        static void PerformSub() {
            int a = AskInteger("Inserisci il minuendo");
            int b = AskInteger("Inserisci il sottraendo");
            Console.WriteLine("Il risultato della differenza è: " + Sub(a, b));
        }

        static void PerformMul() {
            int a = AskInteger("Inserisci il primo fattore");
            int b = AskInteger("Inserisci il secondo fattore");
            Console.WriteLine("Il risultato della moltiplicazione è: " + Mul(a, b));
        }

        static void PerformDiv() {
            int a = AskInteger("Inserisci il dividendo");
            int b = AskInteger("Inserisci il divisore");
            while (b == 0) {
                b = AskInteger("Mi spiace ma non si può dividere per 0. Inserisci un numero diverso da 0");
            }
            Console.WriteLine("Il risultato della divisione è: " + Division(a, b));
        }

        //static int Sum(int a, int b) {
        //    return a + b;
        //}

        static int Sub(int a, int b) {
            return a - b;
        }

        static int Mul(int a, int b) {
            return a * b;
        }

        static int Division(int a, int b) {
            return a / b;
        }


        static int AskInteger(string message) {
            bool firstTime = true;
            string userInput;
            int number;
            do {
                if (firstTime) {
                    Console.WriteLine(message);
                } else {
                    Console.WriteLine("L'input che hai inserito non è un numero reale");
                }
                userInput = Console.ReadLine();
                firstTime = false;
            } while (!int.TryParse(userInput, out number));
            return number;
        }
        #endregion

        #region OverloadingIntroduzione 

        static int Sum (int a, int b) {
            return a + b;
        }

        static int Sum (int a, int b, int c) {
            return a + b + c;
        }

        static float Sum (float a, float b) {
            return a + b;
        } 

        //static byte Sum (byte a, byte b) {
        //    return a + b;
        //}

        static int SumInt(int a, int b) {
            return a + b;
        }

        static float SumFloat (float a, float b) {
            return a + b;
        }

        static float SumByte (Byte a, Byte b) {
            return a + b;
        }



        #endregion

        #region EsercizioMaxTra2o4

        static void MaxTra2o4 () {
            int n1 = int.MinValue, n2 = int.MinValue,n3 = int.MinValue,n4 = int.MinValue;
            int insertedNumber = 0;
            int number;
            do {
                if (insertedNumber < 2) {
                    number = AskInteger("Inserisci un numero intero");
                } else {
                    Console.WriteLine("Vuoi inserire un altro numero (y/n)");
                    string userInput = Console.ReadLine();
                    if (userInput != "y") {
                        break;
                    }
                    number = AskInteger("Inserisci un numero intero");
                }
                insertedNumber++;
                switch (insertedNumber) {
                    case 1:
                        n1 = number;
                        break;
                    case 2:
                        n2 = number;
                        break;
                    case 3:
                        n3 = number;
                        break;
                    case 4:
                        n4 = number;
                        break;
                }
            } while (insertedNumber < 4);
            int max = 0;
            switch (insertedNumber) {
                case 2:
                    max = FindMax(n1, n2);
                    break;
                case 3:
                    max = FindMax(n1, n2, n3);
                    break;
                case 4:
                    max = FindMax(n1, n2, n3, n4);
                    break;
            }
            Console.WriteLine("Il numero maggiore che hai inserito è: " + max);

        }

        static int FindMax (int n1, int n2) {
            if (n1 >= n2) {
                return n1;
            }
            return n2;
        }

        static int FindMax(int n1,int n2,int n3) {
            int max = FindMax(n2, n3);
            if (n1 >= max) {
                return n1;
            }
            return max;
        }

        static int FindMax(int n1,int n2,int n3, int n4) {
            int max = FindMax(n2, n3, n4);
            if (n1 >= max) {
                return n1;
            }
            return max;
        }

        #endregion
    }
}


//5! = 5*4*3*2*1 -- 6! = 6*5*4*3*2*1 --> 6! = 6*(5*4*3*2*1) --> 6! = 6*(5!)
//4! = 4*3*2*1 --> 5! = 5*(4*3*2*1) --> 5! = 5*(4!) --> 6! = 6*(5*(4!))
//n! = n*(n-1)! --> n*(n-1)*(n-2)!
//Fib(n) = Fib(n-1)+Fib(n-2) --> Fib(1)=Fib(0) = 1