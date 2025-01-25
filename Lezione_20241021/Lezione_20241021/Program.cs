using System;

namespace Lezione_20241021 {
    class Program {

        static void Main(string[] args) {

            //RipassoPrimaLezione();
            //ReadANumber();
            //Div();
            //CleverDiv();
            //ClevestDiv();
            //ReadableClevestDiv();
            //NumberBetween();
            //NumberRightOrGreaterThen100();
            Higher532();
            Console.ReadLine();
        }

        static void RecapFirstLesson () {
            string userInput; //DICHIARAZIONE
            //userInput = string.Empty;
            userInput = Console.ReadLine(); //INIZIALIZZAZIONE <-- ASSEGNAZIONE

            Console.WriteLine("Sono un programma straforte, hai appena digitato: " + userInput);

            int numero1 = 56 * 123; //DICHIARAZIONE + INIZIALIZZAZIONE

            Console.WriteLine("Il valore di numero è: " + numero1);
        }

        static void ReadANumber () {
            Console.WriteLine("Insert one number");
            string userInput = Console.ReadLine();
            int number = int.Parse(userInput);
            int result = number + 2;
            Console.WriteLine("Sono il nuovo chatgpt, il numero che hai inserito, " +
                "sommato al numero 2 è: " + result);
        }

        static void Div() {
            string userInput;

            Console.WriteLine("Inserisci il numeratore");
            userInput = Console.ReadLine();
            int a = int.Parse(userInput);

            Console.WriteLine("Inserisci il denominatore");
            userInput = Console.ReadLine();
            int b = int.Parse(userInput);

            float result = a / b;

            Console.WriteLine("Il risultato della divisione è: " + result);
        }

        static void CleverDiv() {
            string userInput;

            Console.WriteLine("Inserisci il numeratore");
            userInput = Console.ReadLine();
            int a = int.Parse(userInput);

            Console.WriteLine("Inserisci il denominatore");
            userInput = Console.ReadLine();
            int b = int.Parse(userInput);
            //bool bNotZero = b != 0;
            //bool bIsZero = b == 0;

            //if (bNotZero) {
            //    float result = a / b;
            //    Console.WriteLine("Il risultato della divisione è: " + result);
            //} else {
            //    Console.WriteLine("Uè triceratopo. Non si può dividere per 0 :)");
            //    Console.WriteLine("Però non te la prendere. Ho scoperto che hai 3 anni e le " +
            //        "divisioni non le hai fatte");
            //}

            if (b == 0) {
                Console.WriteLine("Uè triceratopo. Non si può dividere per 0 :)");
                Console.WriteLine("Però non te la prendere. Ho scoperto che hai 3 anni e le " +
                    "divisioni non le hai fatte");
            } else {
                float result = a / b;
                Console.WriteLine("Il risultato della divisione è: " + result);
            }

            Console.WriteLine("Il programma è terminato, premi invio per chiudere la finestra");

            //if (bIsZero) {
            //    Console.WriteLine("Uè triceratopo. Non si può dividere per 0 :)");
            //}
        }

        static void ClevestDiv() {
            string userInput;
            Console.WriteLine("Inserisci il numeratore");
            userInput = Console.ReadLine();
            int a;
            if (int.TryParse(userInput, out a)) {
                Console.WriteLine("Inserisci il denominatore");
                userInput = Console.ReadLine();
                int b;
                if (int.TryParse(userInput, out b)) {
                    if (b == 0) {
                        Console.WriteLine("Uè triceratopo. Non si può dividere per 0 :)");
                        Console.WriteLine("Però non te la prendere. Ho scoperto che hai 3 anni e le " +
                            "divisioni non le hai fatte");
                    } else {
                        float result = a / b;
                        Console.WriteLine("Il risultato della divisione è: " + result);
                    }
                } else {
                    Console.WriteLine("Non era un numero bro. Perché mi fai questo :(");
                }
            } else {
                Console.WriteLine("Non era un numero bro. Perché mi fai questo :(");
            }

        }

        static void ReadableClevestDiv () {
            string userInput;
            string notANumberFeedback = "Non è un numero. Il programma terminerà la sua esecuzione";
            Console.WriteLine("Inserisci il numeratore");
            userInput = Console.ReadLine();
            int a;
            if (!int.TryParse(userInput, out a)) {
                Console.WriteLine(notANumberFeedback);
                return;
            }
            Console.WriteLine("Inserisci il denominatore");
            int b;
            if (!int.TryParse(userInput, out b)) {
                Console.WriteLine(notANumberFeedback);
                return;
            }
            if (b == 0) {
                Console.WriteLine("Non si può dividere per zero");
                return;
            }
            int result = a / b;
            Console.WriteLine("Il risultato della divisione è: " + result);
        }

        static void NumberBetween () {
            string userInput;
            Console.WriteLine("Inserisci un numero tra 40 e 60");
            userInput = Console.ReadLine();
            int number;
            if (!int.TryParse(userInput, out number)) {
                Console.WriteLine("Non era un numero");
            }
            //if (number < 40) {
            //    Console.WriteLine("No");
            //    return;
            //}
            //if (number > 60) {
            //    Console.WriteLine("No");
            //    return;
            //}
            //Console.WriteLine("Siiiiiii! :)");            
            if (number >= 40 && number <= 60) {
                Console.WriteLine("Si");
            } else {
                Console.WriteLine("No");
            }
            if (!(!(number >= 40) || !(number <= 60))) {
                Console.WriteLine("Si con DeMorgan");
            } else {
                Console.WriteLine("No con DeMorgan");
            }
        }

        static void NumberRightOrGreaterThen100 () {
            Console.WriteLine("Inserisci un numero pari o maggiore di 100");
            string userInput = Console.ReadLine();
            //non mettiamo il controllo perché siamo pigri
            int number = int.Parse(userInput);
            //int resto = number % 2;
            //Console.WriteLine("Il resto della divisione tra il numero che hai inserito e 2 è: " + resto);
            //if (number > 100 || number%2 == 0) {
            //    Console.WriteLine("Hai inserito il numero corretto");
            //} else {
            //    Console.WriteLine("Ciao tricy");
            //}
            if (number > 100 || IsRight(number)) {
                Console.WriteLine("Hai inserito il numero corretto");
            } else {
                Console.WriteLine("Ciao tricy");
            }
        }

        static void Higher532 () {
            Console.WriteLine("Inserisci un numero");
            string userInput = Console.ReadLine();
            //non mettiamo il controllo perché siamo pigri
            int number = int.Parse(userInput);
            if (number%5 == 0) {
                Console.WriteLine("Divisibile per 5");
            } else if (number%3 == 0) {
                Console.WriteLine("Divisible per 3");
            } else if (number%2 == 0) {
                Console.WriteLine("Divisible per 2");
            } else {
                Console.WriteLine("Non è divisibile ne per 5 ne per 3 ne per 2");
            }
        }

        static bool IsRight (int number) {
            Console.WriteLine("Controllo se è pari");
            return number % 2 == 0;
        }

    }
}
