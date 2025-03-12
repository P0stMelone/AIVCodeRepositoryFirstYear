using Lezione_20250310.NewNameSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lezione_20250310 {
    class Program {
        static void Main(string[] args) {
            //TestGenericExample();
            //TestMyArray();
            //TestCircularArray();
            //TestMyList();
            Exercise3();
            //List<int> intList = new List<int> { 1, 3, 5, 6, 7, 8, 9 };
            //Console.WriteLine(Exercise4<int>(intList, 6));
            //List<string> stringList = new List<string> { "ciao", "donne", "è", "arrivato", "l", "arrotino" };
            ////il compare to tra 2 stringhe verifica cosa viene prima in ordine alfabetico
            ////se this è < di other restituisce un val negativo, se è = restituisce 0, se viene dopo un val positivo
            //Console.WriteLine(Exercise4<string>(stringList, "cane"));Console.ReadLine();
        }


        static void TestGenericExample () {
            TestGeneric<int> intTestGeneric = new TestGeneric<int>();
            intTestGeneric.SetValue(23);
            Console.WriteLine("Il valore di intTestGeneric è: " + intTestGeneric.GetValue());

            TestGeneric<string> stringTestGeneric = new TestGeneric<string>();
            stringTestGeneric.SetValue("Giacarlino il più caryno");
            Console.WriteLine("Il valore di stringTestGeneric è: " + stringTestGeneric.GetValue());
        }

        static void TestMyArray () {
            MyArray<int> intArray = new MyArray<int>(5);

            intArray[0] = 32;
            Console.WriteLine(intArray[0]);
        }

        static void TestCircularArray () {
            CircularArray<int> pippo = new CircularArray<int>(5);
            for (int i = 0; i < pippo.Length; i++) {
                pippo[i] = i;
            }
            for (int i = 0; i < 100; i++) {
                Console.WriteLine(pippo[i]);
            }
        }

        static void TestMyList() {
            MyList<int> intList = new MyList<int>();
            for (int i = 0; i < 50; i++) {
                intList.Add(i);
            }
            intList.Reverse(15, 15);
            for (int i = 0; i < intList.Count;i++) {
                Console.WriteLine(intList[i]);
            }
        }

        static void Exercise3() {
            Box<StructDalNomeLungo> intBox = new Box<StructDalNomeLungo>();
            Console.WriteLine(intBox.ToString());
        }

        static int Exercise4<T>(List<T> collection, T value) where T: IComparable<T> {
            int count = 0;
            foreach(T item in collection) {
                if (item.CompareTo(value) > 0) {
                    count++;
                }
            }
            return count;

        }
    }
}
