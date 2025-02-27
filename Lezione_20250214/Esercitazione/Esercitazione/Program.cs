using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione {
    public class Program {

        public class AIVTest {
            int[] arr;
            public AIVTest() {
                
                Random rng = new Random();
                arr = new int[rng.Next(10, 20)];

                for (int i = 0; i < arr.Length; i++) {
                    arr[i] = rng.Next(0, 2);
                }
            }
            public void PrettyPrint() {
                int length = Max().ToString().Length + 1;
                for (int i = 0; i < arr.Length-1; i++) {
                        int spaces = length - arr[i].ToString().Length;
                        Console.Write(new string(' ', spaces) + arr[i] + ",");
                }
                int lastSpaces = length - arr[arr.Length - 1].ToString().Length;
                Console.Write(new string(' ', lastSpaces) + arr[arr.Length - 1]);
            }
            public void Print() {
                //foreach (int n in arr) {
                //    Console.Write(n + ", ");
                //}

                for (int i = 0; i < arr.Length - 1; i++) {
                    Console.Write(arr[i] + ", ");
                }
                Console.Write(arr[arr.Length - 1]);
            }
            public void Print(int[] array) {
                //foreach (int n in arr) {
                //    Console.Write(n + ", ");
                //}

                for (int i = 0; i < array.Length - 1; i++) {
                    Console.Write(array[i] + ", ");
                }
                Console.Write(array[array.Length - 1]);
            }

            public void Reverse() {
            for (int i = 0; i < arr.Length / 2; i++) {
                    int temp = arr[i];
                    arr[i] = arr[arr.Length - i - 1];
                    arr[arr.Length - i - 1] = temp;
                }
            }

            public int Max() {
                int max =int.MinValue;
                for (int i = 0; i < arr.Length - 1; i++) {
                    if (!(arr[i] > max)) { continue; }
                    max = arr[i];
                }
                return max;
            }
            
            public bool SanValentino(int from, int to) {
                int zero=0;
                int one=0;
                for (int i = from; i < to; i++) {
                    if (arr[i] != 0) { one++; }
                    else { zero++; }
                }
                if (zero == one) { return true; }
                else return false;
            }

            public bool SanValentinoSfaticati(int from, int to) {
                int lastPlate = arr[from];
                int change = 0;
                for(int i = from+1; i < to; i++) {
                    if (arr[i] != lastPlate) { change++; }
                    lastPlate = arr[i];
                }
                return change <= 1;
            }
            public int SanValentinoConteggioSfaticati() {
                int lastPlate = arr[0];
                int change = 0;
                for (int i =  1; i < arr.Length; i++) {
                    if (arr[i] != lastPlate) { change++; }
                    lastPlate = arr[i];
                }
                return change+1;
            }

            public int[] Affamati() {
                //un solo ciclo è fattibile
                int[] fame = new int[SanValentinoConteggioSfaticati()];
                int count = 1;
                int pos = 1;
                for (int i = 0; i < fame.Length; i++) {
                    for (int j = pos; j < arr.Length; j++)
                    if (arr[j] == arr[j-1]) { count++; }
                        else {
                            pos = j + 1;
                            break;
                        }
                    fame[i] = count;
                    count = 1;
                }
                return fame;
            }

            public int Ammmore() {
                // 3 4 2 5 1
                int sushi = 0;
                int[] fame = Affamati();
                for (int i = 1; i < fame.Length; i++) {
                    int tempMin = Math.Min(fame[i], fame[i - 1]);
                    if (sushi < tempMin) {
                        sushi = tempMin; 
                    }
                }
                return sushi;
            }

            public int Ammore() {
                int lastPlate = arr[0];
                int count = 1;
                int maxCount = int.MinValue;
                int lastCount = -1;
                for (int i = 1; i < arr.Length; i++) {
                    if (arr[i] != lastPlate) {
                        if (lastCount != -1) {
                            maxCount = Math.Max(maxCount, Math.Min(count, lastCount));
                        }
                        else {
                            lastCount = count;
                        }
                        lastCount = count;
                        count = 1;
                    }
                    else { count++; }
                    lastPlate = arr[i];
                }
                return maxCount;
            }
        }

       
        static void Main(string[] args) {

            AIVTest aivTest = new AIVTest();
            aivTest.PrettyPrint();
            Console.Write("\n");
            aivTest.Reverse();
            aivTest.PrettyPrint();
            //int a = aivTest.SanValentinoConteggioSfaticati();
            //Console.WriteLine(a);
            //Console.WriteLine();
            //int m = ;
            //Console.WriteLine(aivTest.Max());
            //aivTest.Print();
            ////Console.WriteLine(aivTest.SanValentino(0,6));
            ////Console.WriteLine(aivTest.SanValentinoSfaticati(0, 3));
            //Console.WriteLine();
            //aivTest.Print(aivTest.Affamati());
            Console.WriteLine();
            Console.WriteLine(aivTest.Ammmore());
            Console.WriteLine(aivTest.Ammore());
            //
        }
    }
}
