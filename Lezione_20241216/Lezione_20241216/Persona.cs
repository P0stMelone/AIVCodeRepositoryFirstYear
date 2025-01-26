using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20241216 {
    class Persona {

        private float age;
        private string name;
        private string surname;

        public float Age {
            get { return age; }
            set {
                if (value < 0) return;
                age = value;
            }
        }

        public float ImplicitProperty {
            get;
            private set;
        }

        private float notImplicit;

        public float DaveImplicitProperty {
            get { return notImplicit; }
        }

    }
}
