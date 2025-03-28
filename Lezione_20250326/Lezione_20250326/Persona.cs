using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20250326 {
    public class Persona {

        private string nome;
        public string Nome {
            get { return nome; }
            set {
                if (string.IsNullOrEmpty(value)) {
                    nome = "Nessun nome";
                    return;
                }
                nome = value;

            }
        }
        private string cognome;
        public string Cognome {
            get { return cognome; }
            set {
                if (string.IsNullOrEmpty(value)) {
                    cognome = "Nessun cognome";
                    return;
                }
                cognome = value;

            }
        }
        private int eta;
        public int Eta {
            get { return eta; }
            set {
                if (value < 0) {
                    eta = 0;
                    return;
                }
                eta = value;
            }
        }

        private Persona (string nome, string cognome, int eta) {
            Nome = nome;
            Cognome = cognome;
            Eta = eta;
        }


        public override string ToString() {
            return $"{nome},{cognome},{eta}";
        }

        public static Persona PersonaFactory (string riga) {
            string[] rigaSplittata = riga.Split(',');
            if (rigaSplittata.Length < 3 || rigaSplittata.Length > 3) {
                throw new Exception("Riga inutilizzabile");
            }
            if (!int.TryParse(rigaSplittata[2], out int eta)) {
                eta = 0;
            }
            return new Persona(rigaSplittata[0], rigaSplittata[1], eta);
        }
    }
}
