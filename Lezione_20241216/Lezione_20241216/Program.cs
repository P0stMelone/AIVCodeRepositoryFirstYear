using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20241216 {
    class Program {
        static void Main(string[] args) {

            Persona pippo = new Persona();

            Persona dave = new Persona();

            pippo.Age = -12;

            dave.Age = pippo.Age;

            float implicitProperty = pippo.ImplicitProperty;

        }
    }
}
