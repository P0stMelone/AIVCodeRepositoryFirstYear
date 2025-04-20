using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20250414 {
    public class DummyClass {

        private string encapsulatedString;

        public DummyClass(string s) {
            encapsulatedString = s;
        }

        public void DoSomethingWithTheString () {
            if (encapsulatedString.Length % 2 == 0) {
                Program.PrintRightCharacters(encapsulatedString);
            } else {
                Program.PrintOddCharacters(encapsulatedString);
            }
        }


    }
}
