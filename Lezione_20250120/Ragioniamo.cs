using System;


namespace Ragioniamo {
    class Class_A {
        public virtual void Stampa () {
            Console.WriteLine("Stampa Class_A");
        }
        public void Gianfilippo () {
            Console.WriteLine("Gianfilippo Class_A");
        }

        public void MetodoACaso(string aCaso, int superCaso) {
            Console.WriteLine($"aCaso: {aCaso} superCaso: {superCaso}");
        }
    }

    class Class_B : Class_A {
        public override void Stampa () {
            Console.WriteLine("Stampa Class_B");
        }

        public void OnlyB () {

        }
    }

    class Class_C : Class_B {
        public override void Stampa () {
            Console.WriteLine("Stampa Class_C");
        }
    }

    class Class_D : Class_B {
        public void MetodoACaso(string aCaso, int superCaso, bool gianCaso) {
            if (gianCaso) {
                MetodoACaso(aCaso, superCaso);
            } else {
                base.Stampa();
            }
        }
    }


    class Class_E : Class_D {
        public new void Gianfilippo () {
            Console.WriteLine("Gianfilippo Class_E");
        }
    }


    public static class Usage {
        public static void Main() {
            Class_A a = new Class_A();
            Class_B b = new Class_C();
            Class_B c = new Class_E();
            Class_D d = new Class_D();

            a.Stampa(); //Stampa Class_A  OK
            ((Class_B)a).OnlyB(); //darà errore di compilazione
            b.OnlyB(); //lo posso fare perché Class_C eredita da Class_B, quindi eredita tutto quello che c'è in Class_A ma anche in Class_B tra cui OnlyB
            b.Stampa(); //Stampa Class_B nope -- Stampa Class_C
            c.Stampa(); //Stampa Class_B OK
            d.Stampa(); //Stampa Class_B OK
            d.Gianfilippo(); //Gianfilippo Class_A  OK
            c.Gianfilippo(); //Gianfilippo Class_E  nope -- Gianfilippo Class_A
            ((Class_D)c).MetodoACaso("ciaone", 34, false); //Stampa Class_B  OK
        }
    }
}
