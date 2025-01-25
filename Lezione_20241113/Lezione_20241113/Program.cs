using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20241113 {
    class Program {
        static void Main(string[] args) {

            //Esercizio4();
            //Esercizio7();
            TestBinarySearch();

            Console.ReadLine();

        }

        static void Esercizio4 () {
            int[] numbers = new int[] { 1, 2, 3, 4, 3, 2, 1 };
            if (Palindromo(numbers)) {
                Console.WriteLine("è palindromo");
            } else {
                Console.WriteLine("Non è palindromo");
            }
        }

        static bool Palindromo (int[] array) {
            for (int i = 0; i < array.Length /2; i++) {
                if (array[i] != array[array.Length - 1 - i]) return false;
            }
            return true;
        }

        static void Esercizio5() {
            int[] array = new int[] { 3, 4, 5, 89, -12, 450 };
            int numberToCompare = 6;
            int countMin = 0;
            int countMax = 0;
            for (int i = 0; i < array.Length; i++) {
                if (array[i] >= numberToCompare) {
                    countMax++;
                } else {
                    countMin++;
                }
            }

        }

        static void Esercizio6() {
            int[] numbers = new int[] { 2, 3, 5, 7, -1, 20 };
            if (OrdineCrescente(numbers)) {
                Console.WriteLine("La sequenza è in ordine crescente");
            } else {
                Console.WriteLine("La sequenza non è in ordine crescente");
            }
        }

        static bool OrdineCrescente (int[] array) {
            for (int i = 1; i < array.Length; i++) {
                if (array[i] < array[i - 1]) return false;
            }
            return true;
        }

        static void Esercizio7 () {
            int[] numbers = new int[] { 7, 4, 5, 4, 4, 7, 8, 9, 12, 12, 12, 12, 4 };
            int currentMax = 0;
            int currentMaxTimes = 0;
            int counter = 0;
            for (int i = 0; i < numbers.Length; i++) {
                for (int j = 0; j < numbers.Length; j++) {
                    if (numbers[j] == numbers[i]) counter++;
                }
                if (counter > currentMaxTimes) {
                    currentMaxTimes = counter;
                    currentMax = numbers[i];
                }
                counter = 0;
            }
            Console.WriteLine("Il numero che compare più volte è il: " + currentMax + 
                " e compare: " + currentMaxTimes + " volte");
        }


        static void TestBinarySearch () {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            if (BinarySearch(numbers, 9)) {
                Console.WriteLine("Trovato");
            } else {
                Console.WriteLine("Non trovato");
            }
        }

        static bool Search (int[] array, int numberToSearch) {
            for (int i = 0; i < array.Length; i++) {
                if (numberToSearch == array[i]) return true;
            }
            return false;
        }

        static bool BinarySearch (int[] sortedArray, int numberToSearch) {
            int min = 0;
            int max = sortedArray.Length - 1;
            int mid;
            while (min <= max) {
                mid = (max + min) / 2;
                if (sortedArray[mid] == numberToSearch) return true;
                if (numberToSearch > sortedArray[mid]) {
                    min = mid + 1;
                } else {
                    max = mid - 1;
                }
            }
            return false;
        }

        static void Sort (int[] array) {
            for (int i = 0; i < array.Length -1; i++) {
                for (int j = i+1; j < array.Length; j++) {
                    if (array[j] < array[i]) {
                        int n = array[j];
                        array[j] = array[i];
                        array[i] = n;
                    }
                }
            }
        }
    }
}
