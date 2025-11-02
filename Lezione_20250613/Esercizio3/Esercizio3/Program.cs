using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio3 {

    class Calculator {
        static List<string> inf = new List<string>();
        static Stack<string> op = new Stack<string>();

        static Dictionary<string, int> priority = new Dictionary<string, int>{
        { "+", 1 },
        { "-", 1 },
        { "*", 2 },
        { "/", 2 }
        };


        public class Program {
            static void Main(string[] args) {
                Esercizio3();
            }


            static void Esercizio3() {
                Console.WriteLine("Inserisci caratteri uno alla volta (i numeri per intero). Premi = poi invio per calcolare.");

                while (true) {
                    string input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input))
                        continue;

                    if (IsOperator(input)) {
                        while (op.Count > 0 && priority[input] <= priority[op.Peek()]) {
                            inf.Add(op.Pop());
                        }
                        op.Push(input);
                    }
                    else if (IsNumber(input)) {
                        inf.Add(input);
                    }

                    if (input == "=") {
                        while (op.Count > 0) {
                            inf.Add(op.Pop());
                        }

                        int result = Result(inf);
                        Console.WriteLine($"Risultato: {result}");
                        break;
                    }


                }
            }
            static bool IsOperator(string token) {
                return priority.ContainsKey(token);
            }
            
            static bool IsNumber(string number) {
                return int.TryParse(number, out int outNumber);
            }

            static int Result(List<string> allChar) {
                Stack<int> stack = new Stack<int>();

                foreach (var token in allChar) {
                    if (IsNumber(token)) {
                        stack.Push(int.Parse(token));
                    }
                    else if (IsOperator(token)) {
                        int b = stack.Pop();
                        int a = stack.Pop();
                        switch (token) {
                            case "+": stack.Push(a + b); break;
                            case "-": stack.Push(a - b); break;
                            case "*": stack.Push(a * b); break;
                            case "/": stack.Push(a / b); break;
                        }
                    }
                }
                return stack.Pop();
            }

        }
    }
}
