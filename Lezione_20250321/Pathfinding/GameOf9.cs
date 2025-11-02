using Aiv;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Pathfinding.GameOf9;

namespace Pathfinding
{
    class GameOf9
    {
        // int[,] State = new int[3,3];
        public struct State : IEquatable<State>
        {
            // int[] PackedCode = new int[8];
            public struct PackedCode
            {
                public int Code;

                public int this[int i]
                {
                    get { return  1 + ((Code & (7 << i * 3)) >> (i * 3)); }
                    set { Code = (Code & ~(7 << i * 3)) | ((value - 1) << i * 3); }
                }
            }

            public PackedCode PkdCode;

            public int Code { get { return PkdCode.Code; } set { PkdCode.Code = value; } }
            public int Hole;

            public State(int[] vals)
            {
                PkdCode.Code = 0;
                Hole = 0;

                int i = 0;
                foreach(var val in vals)
                {
                    if (val == 0) Hole = i;
                    else PkdCode[i++] = val;
                }
            }

            public int this[int i]
            {
                get { return i == Hole ? 0 : PkdCode[i > Hole ? i - 1 : i]; }
            }

            public int this[int x, int y]
            {
                get { return this[x + y * 3]; }
            }

            public bool Up()
            {
                if (Hole < 3) return false;

                var tmp = PkdCode[Hole - 3];
                PkdCode[Hole - 3] = PkdCode[Hole - 2];
                PkdCode[Hole - 2] = PkdCode[Hole - 1];
                PkdCode[Hole - 1] = tmp;
                Hole -= 3;
                return true;
            }

            public bool Down()
            {
                if (Hole >= 6) return false;

                var tmp = PkdCode[Hole + 2];
                PkdCode[Hole + 2] = PkdCode[Hole + 1];
                PkdCode[Hole + 1] = PkdCode[Hole];
                PkdCode[Hole] = tmp;
                Hole += 3;
                return true;
            }

            public bool Left()
            {
                if ((Hole % 3) == 0) return false;
                --Hole;
                return true;
            }

            public bool Right()
            {
                if ((Hole % 3) == 2) return false;
                ++Hole;
                return true;
            }

            public bool Equals(State other)
            {
                return Code == other.Code && Hole == other.Hole;
            }

            public override bool Equals(object obj)
            {
                return obj is State state && Equals(state);
            }

            public override int GetHashCode()
            {
                int hashCode = Code;
                hashCode += Hole << 27;
                return hashCode.GetHashCode();
            }

            public bool IsSolvable()
            {
                int invCount = 0;
                for (int i = 0; i < 9 - 1; i++)
                    for (int j = i + 1; j < 9; j++)
                        if (this[j] != 0 && this[i] != 0 && this[i] > this[j])
                            invCount++;
                return invCount % 2 == 0;
            }

            public override string ToString()
            {
                string result = "";
                for (int i = 0; i < 9; ++i)
                {
                    result += (this[i] + ", ");
                }
                return result;
            }

            public void Print()
            {
                for (int i = 0; i < 3; ++i)
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        Console.Write(this[j, i] + ", ");
                    }
                    Console.WriteLine();
                }
            }

            bool Find(int v, out int x, out int y)
            {
                y = 0;
                for (x = 0; x < 3; x++)
                {
                    for (y = 0; y < 3; y++)
                    {
                        if (this[x, y] == v) return true;
                    }
                }
                return false;
            }

            public int DistanceSimple(State other)
            {
                int dist = 0;
                for (int i = 0; i < 9; ++i)
                {
                    if (this[i] != other[i]) dist++;
                }
                return dist;
            }

            public int DistanceManhattan(State other)
            {
                int dist = 0;
                for (int i = 0; i < 9; ++i)
                {
                    int x;
                    int y;
                    if (Find(other[i], out x, out y) && other[i] != 0)
                    {
                        dist += Algo.ManhattanDistance(x, y, i % 3, i / 3);
                    }
                }
                return dist;
            }

            public List<SearchTreeAction<State>> GetActions()
            {
                List<SearchTreeAction<State>> actions = new List<SearchTreeAction<State>>();
                SearchTreeAction<State> action = new SearchTreeAction<State>();

                action.Cost = 1;
                action.NewState = this;
                if (action.NewState.Up()) actions.Add(action);
                action.NewState = this;
                if (action.NewState.Down()) actions.Add(action);
                action.NewState = this;
                if (action.NewState.Left()) actions.Add(action);
                action.NewState = this;
                if (action.NewState.Right()) actions.Add(action);

                return actions;
            }
        }

        public struct SearchGraphDjikstra : ISearchGraph<State>
        {
            public List<SearchTreeAction<State>> GetActions(State State)
            {
                return State.GetActions();
            }

            public float Heuristic(State From, State To)
            {
                return 0;
            }
        }

        public struct SearchGraphManhattan : ISearchGraph<State>
        {
            public List<SearchTreeAction<State>> GetActions(State State)
            {
                return State.GetActions();
            }

            public float Heuristic(State From, State To)
            {
                return From.DistanceManhattan(To);
            }
        }

        public struct SearchGraphSimple : ISearchGraph<State>
        {
            public List<SearchTreeAction<State>> GetActions(State State)
            {
                return State.GetActions();
            }

            public float Heuristic(State From, State To)
            {
                return Math.Max(0, From.DistanceSimple(To));
            }
        }

        public class SearchGraphDatabase : ISearchGraph<State>
        {
            public class HeuristicDB
            {
                Dictionary<State, float> Heuristics = new Dictionary<State, float>();
                int Precision = 5;

                public bool TryGetHeuristics(State s, out float heur)
                {
                    for (int i = 0; i < 8; ++i)
                    {
                        if (s.PkdCode[i] < Precision) s.PkdCode[i] = 1;
                    }
                    return Heuristics.TryGetValue(s, out heur);
                }

                public HeuristicDB(FileInfo fileName, int precision = 5)
                {
                    Precision = precision;
                    if (fileName.Exists)
                    {
                        Load(fileName);
                    }
                    else
                    {
                        Compute(fileName);
                    }
                }

                void Load(FileInfo fileName)
                {
                    XDocument doc = XDocument.Load(fileName.FullName);
                    foreach (var element in doc.Root.Elements())
                    {
                        Heuristics[new State { Code = (int)element.Attribute("Code"), Hole = (int)element.Attribute("Hole") }] = (float)element.Attribute("Cost");
                    }
                }

                void Compute(FileInfo fileName)
                {
                    int[] trainValues = { 0, 1, 1, 1, 1, 1, 1, 1, 1 };
                    for (int i = Precision; i < trainValues.Length; ++i)
                    {
                        trainValues[i] = i;
                    }

                    SearchGraphSimple simpleGraph;
                    State trainTo = new State(trainValues);
                    do
                    {
                        State from = new State(trainValues);
                        
                        var cost = SearchTree.AStarSearch(simpleGraph, from, trainTo).Solution.Cost;
                        Heuristics[from] = cost;
                    } while (Algo.NextPermutation(trainValues));

                    XDocument doc = new XDocument(new XElement("Heuristics"));
                    foreach (var heuristic in Heuristics)
                    {
                        doc.Root.Add(new XElement("Heuristic", new XAttribute("Code", heuristic.Key.Code), new XAttribute("Hole", heuristic.Key.Hole), new XAttribute("Cost", heuristic.Value)));
                    }
                    doc.Save(fileName.FullName);
                }
            }


            HeuristicDB database = null;

            public SearchGraphDatabase(FileInfo dbFileName, int precision = 5)
            {
                database = new HeuristicDB(dbFileName, precision);
            }

            public List<SearchTreeAction<State>> GetActions(State State)
            {
                return State.GetActions();
            }

            public float Heuristic(State From, State To)
            {
                float dist = 0;
                if(database.TryGetHeuristics(From, out dist))
                {
                    return dist;
                }
                return From.DistanceSimple(To);
            }
        }

        public static void Run()
        {
            int count = 0;
            
            int[] values = { 1, 2, 3, 4, 5, 6, 7, 8 };
            State to = new State(values);

            values = new int[]{ 4, 8, 0, 7, 2, 6, 1, 3, 5 };

            var databaseGraph = new SearchGraphDatabase(new FileInfo("Heuristics.xml"), 5);
            var manhattanGraph = new SearchGraphManhattan();
            var simpleGraph = new SearchGraphSimple();
            var djikstraGraph = new SearchGraphDjikstra();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                State from = new State(values);
                if (from.IsSolvable())
                {
                    //var solutionM = SearchTree.AStarSearch(manhattanGraph, from, to);
                    //var solutionS = SearchTree.AStarSearch(simpleGraph, from, to);
                    var solutionD = SearchTree.AStarSearch(databaseGraph, from, to);
                    //Console.WriteLine($"{solutionM.Iterations} - {solutionS.Iterations} - {solutionD.Iterations}");
                }

                if (count++ == 500)
                {
                    Console.WriteLine(sw.Elapsed);
                    break;
                }
            } while (Algo.NextPermutation(values));
        }
    }
}
