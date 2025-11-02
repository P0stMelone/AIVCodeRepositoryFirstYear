using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Xml.Linq;

namespace Esercitazione {
    internal class Program {
        static void Main(string[] args) {
            GetInfo(args);
        }
        #region esercizio1
        static void GetInfo(string[] args) {

            if (args.Length == 0 || !int.TryParse(args[0], out int baseSize)) {
                Console.WriteLine("Insert an int number");
                return;
            }

            PrintPyramid(baseSize);
        }

        static void PrintPyramid(int baseSize) {
            int topSize = baseSize % 2 == 0 ? 2 : 1;
            int pyramidHigh = ((baseSize - topSize) / 2) + 1;

            for (int i = 0; i < pyramidHigh; i++) {
                int numberOfStar = topSize + i * 2;
                int spaces = (baseSize - numberOfStar) / 2;

                Console.Write(new string(' ', spaces));
                Console.WriteLine(new string('*', numberOfStar));
            }
        }
        #endregion
        #region Esercizio2
        public void LoadFromXDocument(string filePath) {

            XDocument src = XDocument.Load(filePath);
            XElement world = src.Element("world");
            World newWorld = new World();

            XAttribute level = world.Attribute("level");
            newWorld.Level = level?.Value;

            foreach (XElement area in world.Elements("area")) {
                Area newArea = new Area();
                XAttribute name = area.Attribute("name");
                XAttribute state = area.Attribute("state");
                newArea.Name = name?.Value;
                newArea.State = state?.Value;
                newWorld.Areas.Add(newArea);
            }

            foreach (XElement door in world.Elements("door")) {
                Door newDoor = new Door();
                XAttribute name = door.Attribute("name");
                XAttribute state = door.Attribute("state");
                newDoor.Name = name?.Value;
                newDoor.State = state?.Value;
                newWorld.Doors.Add(newDoor);
            }

            foreach (XElement spawner in world.Elements("spawner")) {
                Spawner newSpawner = new Spawner();
                Spawn newSpawn = new Spawn();

                XAttribute type = spawner.Attribute("type");
                XAttribute name = spawner.Attribute("name");
                XAttribute state = spawner.Attribute("state");
                foreach (XElement spawn in spawner.Elements("spawn")) {

                    XAttribute eClass = spawn.Attribute("class");
                    XAttribute id = spawn.Attribute("id");
                    XAttribute pos = spawn.Attribute("position");

                    newSpawn.Class = eClass?.Value;
                    newSpawn.Id = id?.Value;
                    newSpawn.Position = pos?.Value;
                }

                newSpawner.Type = type?.Value;
                newSpawner.Name = name?.Value;
                newSpawner.State = state?.Value;
                newSpawner.Spawns.Add(newSpawn);

                newWorld.Spawners.Add(newSpawner);
            }
        }
    }

    #region Class

    public class World {
        public string Level;
        public List<Area> Areas = new List<Area>();
        public List<Door> Doors = new List<Door>();
        public List<Spawner> Spawners = new List<Spawner>();
    }
    public class Area {
        public string Name;
        public string State;
    }

    public class Door {
        public string Name;
        public string State;
    }
    public class Spawner {
        public string Type;
        public string Name;
        public string State;
        public List<Spawn> Spawns = new List<Spawn>();
    }

    public class Spawn {
        public string Class;
        public string Id;
        public string Position;
    }

    public class Player {
        public string Id;
        public string Position;
        public HP Hp;
        public Weapon Weapon;
        public Inventory Inventory;
    }

    public class HP {
        public int Current;
        public int Max;
    }

    public class Weapon {
        public string Class;
        public int Ammo;
    }

    public class Inventory {
        public List<InventoryItem> Items = new List<InventoryItem>();
    }
    public class InventoryItem {
        public string Class;
        public int Count;
    }
    public class NPC {
        public string Id;
        public HP Hp;
    }



    #endregion

    #endregion
    #region Esercizio3
    static void Esercizio3() {
            Console.WriteLine("Inserisci caratteri uno alla volta (i numeri per intero). Premi = poi invio per calcolare.");
        }





    class Calculator {
        static List<string> inf = new List<string>();
        static Stack<string> op = new Stack<string>();
        static string currentNumber = "";

        static Dictionary<string, int> priority = new Dictionary<string, int>{
        { "+", 1 },
        { "-", 1 },
        { "*", 2 },
        { "/", 2 }
        };
    }
        #endregion
    }
}
