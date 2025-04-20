using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20250414 {

    public delegate float OperationDelegate(float f1, float f2);

    public class OperationClass {

        private float a;
        private float b;

        public OperationClass(float a, float b) {
            this.a = a;
            this.b = b;
        }

        public void DoOperation (OperationDelegate operation) {
            Console.WriteLine("Faccio l'operazione e il risultato è: " +  operation.Invoke(a, b));
        }

        public void DoPrintString (PrintStringDelegate stringDelegate) {
            stringDelegate.Invoke("Ciaone barbagiannone");
        }

        public void DoOperationAnon (Func<float, float, float> operation) {
            Console.WriteLine("Faccio l'operazione e il risultato è: " + operation.Invoke(a, b));
        }

        public void DoPrintStringAnon (Action<string> stringAction) {
            stringAction.Invoke("Anon barbagianni");
        }

    }
}
