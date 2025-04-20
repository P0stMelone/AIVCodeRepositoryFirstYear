using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20250414 {

    public delegate void PrintStringDelegate(string s); //DICHIARAZIONE
    public delegate void TestClassDelegate();

    class Program {

        public static float Sum (float f1, float f2) {
            return f1 + f2;
        }

        public static float Product (float f1, float f2) {
            return f1 * f2;
        }

        public static float Sub(float f1, float f2) {
            return f1 - f2;
        }
        public static float Div(float f1, float f2) {
            return f1 / f2;
        }

        public static void PrintFullString (string s) {
            Console.WriteLine(s);
        }

        public static void PrintTiHoFregatoObserver (string s) {
            //throw new Exception("Eh eh eh");
        }

        public static void PrintRightCharacters (string s) {
            for (int i = 0; i < s.Length; i++) {
                if (i % 2 != 0) continue;
                Console.Write(s[i]);
            }
            Console.Write(Environment.NewLine);
        }

        public static void PrintOddCharacters (string s) {
            for (int i = 0; i < s.Length; i++) {
                if (i % 2 == 0) continue;
                Console.Write(s[i]);
            }
            Console.Write(Environment.NewLine);
        }

        static void Main(string[] args) {
            //DelegateExample();
            //DelegateOnClassMethodsExample();
            OperationClassUsage();
            Console.ReadLine();
        }


        static void DelegateExample () {
            PrintStringDelegate delegateObject = new PrintStringDelegate(PrintRightCharacters);
            delegateObject += PrintFullString;
            delegateObject += PrintOddCharacters;
            delegateObject.Invoke("Barbagianni se son bello");
            delegateObject -= PrintRightCharacters;
            delegateObject -= PrintOddCharacters;
            delegateObject.Invoke("Barbagianni se son brutto");
        }

        static void DelegateOnClassMethodsExample () {
            DummyClass o1 = new DummyClass("Ciaone è una stringa non so se pari o dispari ma la " +
                "prossima ha tutti i caratteri tranne questo!");
            DummyClass o2 = new DummyClass("Ciaone è una stringa non so se pari o dispari ma la " +
                "prossima ha tutti i caratteri tranne questo");
            TestClassDelegate oDelegate = new TestClassDelegate(o1.DoSomethingWithTheString);
            oDelegate += o2.DoSomethingWithTheString;
            oDelegate.Invoke();
        }

        static void OperationClassUsage () {
            OperationClass a = new OperationClass(3, 5);
            OperationClass b = new OperationClass(5, 5);
            OperationDelegate sum = new OperationDelegate(Sum);
            OperationDelegate prod = new OperationDelegate(Product);
            OperationDelegate sub = new OperationDelegate(Sub);
            OperationDelegate div = new OperationDelegate(Div);
            a.DoOperation(sum);
            //b.DoOperation(prod);
            a.DoOperation(prod);
            a.DoOperation(sub);
            a.DoOperation(div);

            OperationDelegate _100Perc_DelMioCervello = new OperationDelegate(Sum);
            _100Perc_DelMioCervello += Product;
            _100Perc_DelMioCervello += Sub;
            _100Perc_DelMioCervello += Div;

            a.DoOperation(_100Perc_DelMioCervello);

            PrintStringDelegate stringDelegate = new PrintStringDelegate(PrintTiHoFregatoObserver);

            stringDelegate += PrintOddCharacters;
            stringDelegate += PrintRightCharacters;
            stringDelegate += PrintFullString;
            a.DoPrintString(stringDelegate);

            OperationDelegate lambdaExampleDelegate = new OperationDelegate(
                (float x, float y) => { return (float)Math.Pow(x, y);  }
            );
            a.DoOperation(lambdaExampleDelegate);

            lambdaExampleDelegate = new OperationDelegate(
                (float x, float y) => { return (float)Math.Max(x, y); }
            );

            a.DoOperation(lambdaExampleDelegate);

            a.DoOperation((float x, float y) => { return (float)Math.Min(x, y); });

            Action<string> stringOperation = new Action<string>(
                (string s) => { Console.WriteLine("Ciaone"); }
            );
            stringOperation += ((string s) => { Console.WriteLine(s); });
            a.DoPrintStringAnon(stringOperation);

        }
    }
}
