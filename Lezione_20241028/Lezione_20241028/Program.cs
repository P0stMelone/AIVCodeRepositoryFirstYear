using System;

namespace Lezione_20241028 {
    class Program {
        static void Main(string[] args) {

            //LessonIntroduction();
            //OutExample();
            //Esercizio3();
            //Calculator();
            //BimBumBam();
            BimBumBamLoop();
            Console.ReadLine();

        }

        #region Introduction
        static void LessonIntroduction () {
            int pippo = 2;
            Console.WriteLine("Il valore della variabile pippo è: " + pippo);
            SumTwo(pippo);
            Console.WriteLine("Dopo la chiamata al metodo SumTwo, pippo è: " + pippo);
            int paperino = pippo;
            paperino += 2;
            Console.WriteLine("Dopo aver aumentato di due paperino pippo vale: " + pippo);

        }
        static void SumTwo (int param1) {
            param1 += 2; //param1 = param1 + 2;
        }
        #endregion


        #region Example
        static void OutExample () {
            int numberOfTime;
            int parsedNumber;
            //parsedNumber = AskInt(out numberOfTime);
            AskIntPro(out parsedNumber, out numberOfTime);
            IncrementBy2(ref numberOfTime); //baro
            Console.WriteLine("Il numero inserito dall'utente è: " + parsedNumber +
                " e ci ha messo: " + numberOfTime + " volte prima di inserirlo correttamente");
        }

        static int AskInt (out int numberOfTime) {
            numberOfTime = 0;
            int number;
            string userInput;
            do {
                numberOfTime++;
                Console.WriteLine("Inserisci un numero intero");
                userInput = Console.ReadLine();
            } while (!int.TryParse(userInput, out number));
            return number;
        }

        static void AskIntPro (out int parsedNumber, out int numberOfTime) {
            numberOfTime = 0;
            parsedNumber = 0;
            string userInput;
            do {
                numberOfTime++;
                Console.WriteLine("Inserisci un numero intero");
                userInput = Console.ReadLine();
            } while (!int.TryParse(userInput, out parsedNumber));
        }

        static void IncrementBy2 (ref int variable) {
            variable += 2;
        }
        #endregion

        static void Esercizio2 (ref int variable) {
            variable++;
        }

        #region Esercizio3
        static void Esercizio3 () {
            float a = AskFloat("Inserisci a");
            float b = AskFloat("Inserisci b");
            int resultType = EqResult(a, b, out float result);
            if (resultType == 0) {
                Console.WriteLine("L'equazione non ha soluzione");
            } else if (resultType == 1) {
                Console.WriteLine("L'equazione è indeterminata");
            } else {
                Console.WriteLine("La soluzione è: " + result);
            }
        }

        static int EqResult (float a, float b, out float result) {
            if (a == 0) {
                result = 0;
                if (b == 0) {
                    return 1; //indeterminata
                } else {
                    return 0; //impossibile
                }
            }
            result = -b / (float)a;
            return 2; //ha una soluzione
        }

        static float AskFloat (string message) {
            bool firstTime = true;
            string userInput;
            float number;
            do {
                if (firstTime) {
                    Console.WriteLine(message);
                } else {
                    Console.WriteLine("L'input che hai inserito non è un numero reale");
                }
                userInput = Console.ReadLine();
                firstTime = false;
            } while (!float.TryParse(userInput, out number));
            return number;
        }
        #endregion


        #region Esercizio per casa 1

        static void Calculator () {
            char op;
            do {
                Console.Write("Inserisci l'operatore (q per uscire)");
                op = Console.ReadKey().KeyChar;
                if (op == 'q' || op == 'Q') break;
                Console.Write(Environment.NewLine);
                if (op == '+') {
                    PerformSum();
                } else if (op == '-') {
                    PerformSub();
                } else if (op == '*') {
                    PerformMul();
                } else if (op == '/') {
                    PerformDiv();
                }
            } while (true);
        }

        static void PerformSum () {
            int a = AskInteger("Inserisci il primo addendo");
            int b = AskInteger("Inserisci il secondo addendo");
            Console.WriteLine("Il risultato della somma è: " + Sum(a, b));
        }

        static void PerformSub () {
            int a = AskInteger("Inserisci il minuendo");
            int b = AskInteger("Inserisci il sottraendo");
            Console.WriteLine("Il risultato della differenza è: " + Sub(a, b));
        }

        static void PerformMul () {
            int a = AskInteger("Inserisci il primo fattore");
            int b = AskInteger("Inserisci il secondo fattore");
            Console.WriteLine("Il risultato della moltiplicazione è: " + Mul(a, b));
        }

        static void PerformDiv () {
            int a = AskInteger("Inserisci il dividendo");
            int b = AskInteger("Inserisci il divisore");
            while (b == 0) {
                b = AskInteger("Mi spiace ma non si può dividere per 0. Inserisci un numero diverso da 0");
            }
            Console.WriteLine("Il risultato della divisione è: " + Division(a, b));
        }

        static int Sum (int a, int b) {
            return a + b;
        }

        static int Sub (int a, int b) {
            return a - b;
        }

        static int Mul (int a, int b) {
            return a * b;
        }

        static int Division (int a, int b) {
            return a / b;
        }


        static int AskInteger (string message) {
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


        #region Esercizio per casa 2

        static void BimBumBamLoop () {
            bool firstTime = true;
            do {
                if (firstTime) {
                    Console.WriteLine("Benvenuto nel bimbumbam più bello che ci sia");
                } else {
                    Console.WriteLine();
                    Console.WriteLine("Giocatori preparatevi, la nuova partita sta per cominciare");
                }
                firstTime = false;
                BimBumBam();
                Console.WriteLine();
                Console.WriteLine("Volete fare un'altra partita? (y/n)");
            } while (Console.ReadKey().KeyChar != 'n');
        }

        static void BimBumBam () {
            int numberOfRound = AskInteger("Inserisci il numero di round");
            int p1RoundWon = 0;
            int p2RoundWon = 0;
            for (int i = 0; i < numberOfRound; i++) {
                bool p1Right = AskRightOrOdd();
                Console.Write(Environment.NewLine);
                int p1N = AskInteger("Giocatore 1 inserisci il tuo numero");
                Console.Clear();
                int p2N = AskInteger("Giocatore 2 inserisci il tuo numero");
                bool isRight = (p1N + p2N) % 2 == 0;
                if (p1Right && isRight) {
                    p1RoundWon++;
                } else if (!p1Right && !isRight) {
                    p1RoundWon++;
                } else {
                    p2RoundWon++;
                }
                if (p1RoundWon > numberOfRound / 2f || 
                    p2RoundWon > numberOfRound / 2f) {
                    break;
                }
                Console.WriteLine("Risultati parziali:");
                Console.WriteLine("Giocatore 1: " + p1RoundWon);
                Console.WriteLine("Giocatore 2: " + p2RoundWon);
            }
            Console.WriteLine();
            Console.WriteLine("PARTITA FINITA");
            if (p1RoundWon > p2RoundWon) {
                Console.WriteLine("Giocatore 1 vince " + p1RoundWon + " a " + p2RoundWon);
            } else {
                Console.WriteLine("Giocatore 2 vince " + p2RoundWon + " a " + p1RoundWon);
            }
        }

        static bool AskRightOrOdd () {
            Console.WriteLine("Giocatore 1, scegli pari (y/n)");
            return Console.ReadKey().KeyChar == 'y';
        }

        #endregion

    }
}
