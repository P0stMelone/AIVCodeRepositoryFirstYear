using System;
using System.Collections.Generic;

namespace Lezione_20250113 {
    class Program {
        static void Main(string[] args) {

            //ListExample();
            //DictionaryExample();
            //QueueExample();
            StackExample();

            Console.ReadLine();

        }
        
        static void ListExample () {

            List<string> dinosaurs = new List<string>(200); //--> inventory.Count NON è 200. Ma è 0

            dinosaurs.Add("Triceratopo"); // --> in questo momento inventory.Count è 1
            dinosaurs.Add("T-Rex"); // count è 2
            dinosaurs.Add("Brontosauro"); //count è 3

            dinosaurs[2] = "Brontosaura";

            dinosaurs.Add("Velociraptor");

            dinosaurs.Insert(2, "Pterodattilo"); // la nostra amica brontosaura  è diventata l'elemento in indice 3

            Console.WriteLine("La nostra collezione di dinosauri è: ");
            for (int j = 0; j < dinosaurs.Count; j++) {
                Console.WriteLine(j + ": " + dinosaurs[j]);
            }

            //La stessa cosa con il foreach è così
            int i = 0;
            foreach(string dinosaur in dinosaurs) {
                Console.WriteLine(i + ": " + dinosaur);
                i++;
            }


        }

        static void DictionaryExample () {

            Dictionary<string, int> playerStats = new Dictionary<string, int>();

            playerStats.Add("hp", 10);
            playerStats.Add("mp", 1);
            playerStats.Add("strength", 300);
            playerStats.Add("magic power", 1);

            if (playerStats.ContainsKey("defense")) {
                playerStats.Remove("defense");
            }
            if (playerStats.ContainsKey("hp")) {
                playerStats["hp"] = 9999;
            } else {
                playerStats.Add("hp", 9999);
            }

            playerStats.Remove("strength");
            playerStats.Add("Geppetto", 23);

            foreach (var item in playerStats) {
                Console.WriteLine(item.Key + ": " + item.Value);
            }
        }

        static void QueueExample () {

            Queue<string> enemyAction = new Queue<string>();

            enemyAction.Enqueue("Prendi in giro il giocatore");
            enemyAction.Enqueue("Fai una giravolta");
            enemyAction.Enqueue("Falla un'altra volta");
            enemyAction.Enqueue("Guarda in su");
            enemyAction.Enqueue("Guarda in giù");
            enemyAction.Enqueue("Uno schiaffone pesante prendi tu");

            while (enemyAction.Count > 0) {
                Console.WriteLine("Il nemico esegue: " + enemyAction.Dequeue());
            }

        }

        static void StackExample () {

            Stack<string> actionStack = new Stack<string>();

            actionStack.Push("Menu di pausa");
            actionStack.Push("Menu opzioni");
            actionStack.Push("Opzioni grafiche");

            while (actionStack.Count > 0) {
                Console.Write("Chiudo " + actionStack.Pop());
                if (actionStack.Count > 0) {
                    Console.Write(" e apro " + actionStack.Peek());
                }
                Console.Write(Environment.NewLine);
            }
        }

    }
}
