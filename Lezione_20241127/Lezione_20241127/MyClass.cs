using System;

namespace Lezione_20241127 {
    class MyClass {

        int numero;

        public MyClass(int numero) {
            this.numero = numero;
            Console.WriteLine("Oggetto di tipo MyClass creato con parametro numero passato di valore: " 
                + this.numero);
        }

        public void IncrementNumero () {
            numero++;
        }

        ~MyClass() {
            Console.WriteLine("Qualcuno ha distrutto un oggetto di tipo myclass che aveva com valore dell'attributo" +
                "numero, il valore: " + numero);
        }

    }
}
