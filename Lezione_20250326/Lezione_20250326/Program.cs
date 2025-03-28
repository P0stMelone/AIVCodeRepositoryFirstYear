using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lezione_20250326 {
    class Program {

        const string esercizio1Path = @"Assets\Config.txt";
        const string esercizio2Path = @"Assets\Persone.txt";

        static void Main(string[] args) {
            //Esercizio1();
            //Esercizio2();
            //Esercizio3();
            //Esercizio4();
            Esercizio5();
            Console.ReadLine();
        }

        static void Esercizio1 () {
            Console.WriteLine("Inizio il programma");
            List<string> lines = File.ReadAllLines(esercizio1Path).ToList();
            foreach(string line in lines) {
                Console.WriteLine(line);
            }
            lines.Add("Iscrivetevi alla newsletter di Dave per sapere anche " +
                "come si estinguerà l'uomo sapiens sapiens");
            File.WriteAllLines(esercizio1Path, lines.ToArray());
        }

        static void Esercizio2 () {
            List<string> lines = File.ReadAllLines(esercizio2Path).ToList();

            List<Persona> persone = new List<Persona>();
            foreach(string line in lines) {
                persone.Add(Persona.PersonaFactory(line));
            }
            persone.Add(Persona.PersonaFactory("Pippo,Franco,80"));

            lines = new List<string>();
            foreach(Persona persona in persone) {
                lines.Add(persona.ToString());
            }

            File.WriteAllLines(esercizio2Path, lines);
        }

        static void Esercizio3 () {
            List<Persona> persone = new List<Persona>();
            StreamReader sr = File.OpenText(esercizio2Path);
            string s = string.Empty;
            while ((s = sr.ReadLine()) != null) {
                persone.Add(Persona.PersonaFactory(s));
            }
            sr.Close();
            persone.Add(Persona.PersonaFactory("Pippo,Franco,80"));
            StreamWriter sw = File.AppendText(esercizio2Path);
            sw.Write(Environment.NewLine + persone[persone.Count - 1].ToString());
            sw.Close();
        }

        static void Esercizio4 () {
            int intValue = -16;
            Byte[] intBytes = BitConverter.GetBytes(intValue);

            int intValueConverted = BitConverter.ToInt32(intBytes, 0);
            Console.WriteLine($"{intValue},{intValueConverted}");
            uint uintValueConverted = BitConverter.ToUInt32(intBytes, 0);
            Console.WriteLine("Se lo convertiamo come uint esce: " + uintValueConverted);

            bool boolValue = true;
            Byte[] boolBytes = BitConverter.GetBytes(boolValue);
            if (BitConverter.IsLittleEndian) {
                Array.Reverse(boolBytes);
            }
            bool boolValueConvertered = BitConverter.ToBoolean(boolBytes, 0);
            Console.WriteLine(boolValueConvertered);

            Console.WriteLine(BitConverter.ToString(intBytes));
        }

        static void Esercizio5 () {
            List<string> lines = new List<string>();
            List<Persona> persone = new List<Persona>();

            FileStream fs = File.Open(esercizio2Path, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            UTF8Encoding encoder = new UTF8Encoding();
            byte[] b = new byte[fs.Length];
            while((fs.Read(b,0,b.Length) > 0)) {
                string convertedByteReaded = encoder.GetString(b);
                lines = convertedByteReaded.Split('\n').ToList();
            }
            foreach(string line in lines) {
                persone.Add(Persona.PersonaFactory(line));
            }
            persone.Add(Persona.PersonaFactory("Pippo,Franco,80"));
            string pippoFrancoStringa = Environment.NewLine + persone[persone.Count - 1].ToString();
            Byte[] pippoFrancoByte = encoder.GetBytes(pippoFrancoStringa);
            fs.Seek(fs.Length, SeekOrigin.Begin);
            fs.Write(pippoFrancoByte, 0, pippoFrancoByte.Length);
            fs.Close();
        }
    }
}
