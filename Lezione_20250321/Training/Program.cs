using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Aiv;

namespace Training
{
    internal class Program
    {
        public struct Position2D : IEquatable<Position2D>
        {
            public int X;
            public int Y;

            public override bool Equals(object obj)
            {
                return obj is Position2D d &&
                       Equals(d);
            }

            public bool Equals(Position2D d)
            {
                return X == d.X &&
                       Y == d.Y;
            }

            public static int HashCombine(int hashA, int hashB)
            {
                return (int)(hashA ^ (hashB + 0x9e3779b9 + (hashA << 6) + (hashA >> 2)));
            }

            public override int GetHashCode()
            {
                return HashCombine(X, Y);
            }
        }

        class Grid : Aiv.ISearchGraph<Position2D>
        {
            public enum ETile
            {
                Wall = 0,
                Floor = 1,
                Swamp = 5
            }

            public int Width { get { return grid.GetLength(0); } }
            public int Height { get { return grid.GetLength(1); } }

            public Grid(int width, int height)
            {
                grid = new ETile[width, height];
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        grid[x, y] = ETile.Floor;
                    }
                }
            }

            public ETile this[int x, int y]
            {
                get { return grid[x, y]; }
            }

            public void Place(int x0, int y0, int x1, int y1, ETile tileType)
            {
                for (int x = x0; x < x1; ++x)
                {
                    for (int y = y0; y < y1; ++y)
                    {
                        grid[x, y] = tileType;
                    }
                }
            }

            public void Print()
            {
                for (int x = -2; x < grid.GetLength(0); x++)
                {
                    Console.Write('_');
                }
                Console.WriteLine();
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    Console.Write('|');
                    for (int x = 0; x < grid.GetLength(0); x++)
                    {
                        Console.Write(grid[x, y] == ETile.Wall ? '#' : ( grid[x, y] == ETile.Swamp ? '^' : ' '));
                    }
                    Console.Write('|');
                    Console.WriteLine();
                }
                for (int x = -2; x < grid.GetLength(0); x++)
                {
                    Console.Write('_');
                }
            }

            public List<Aiv.SearchTreeAction<Position2D>> GetActions(Position2D from)
            {
                var result = new List<Aiv.SearchTreeAction<Position2D>>();

                if (from.X > 0 && this[from.X - 1, from.Y] != Grid.ETile.Wall)
                {
                    result.Add(new Aiv.SearchTreeAction<Position2D> { NewState = new Position2D { X = from.X - 1, Y = from.Y }, Cost = (int)this[from.X-1, from.Y] });
                }
                if (from.X + 1 < this.Width && this[from.X + 1, from.Y] != Grid.ETile.Wall)
                {
                    result.Add(new Aiv.SearchTreeAction<Position2D> { NewState = new Position2D { X = from.X + 1, Y = from.Y }, Cost = (int)this[from.X + 1, from.Y] });
                }
                if (from.Y > 0 && this[from.X, from.Y - 1] != Grid.ETile.Wall)
                {
                    result.Add(new Aiv.SearchTreeAction<Position2D> { NewState = new Position2D { X = from.X, Y = from.Y - 1 }, Cost = (int)this[from.X, from.Y - 1] });
                }
                if (from.Y + 1 < this.Height && this[from.X, from.Y + 1] != Grid.ETile.Wall)
                {
                    result.Add(new Aiv.SearchTreeAction<Position2D> { NewState = new Position2D { X = from.X, Y = from.Y + 1 }, Cost = (int)this[from.X, from.Y + 1] });
                }

                return result;
            }

            public float Heuristic(Position2D from, Position2D to)
            {
                return Aiv.Algo.ManhattanDistance(from.X, from.Y, to.X, to.Y);
            }

            ETile[,] grid;
        }


        struct GameOf9State : IEquatable<GameOf9State>
        {
            public int[,] Tiles;

            public GameOf9State(int[,] other)
            {
                Tiles = new int[other.GetLength(0), other.GetLength(1)];
                for (int i = 0; i < other.GetLength(1); ++i)
                {
                    for (int j = 0; j < other.GetLength(0); ++j)
                    {
                        Tiles[i, j] = other[i, j];
                    }
                }
            }

            public static GameOf9State GetDefault()
            {
                GameOf9State result;
                result.Tiles = new int[3, 3];
                result.Tiles[0, 0] = 1;
                result.Tiles[1, 0] = 2;
                result.Tiles[2, 0] = 3;
                result.Tiles[0, 1] = 4;
                result.Tiles[1, 1] = 5;
                result.Tiles[2, 1] = 6;
                result.Tiles[0, 2] = 7;
                result.Tiles[1, 2] = 8;
                result.Tiles[2, 2] = 0;
                return result;
            }

            public bool IsSolvable()
            {
                int invCount = 0;
                for (int i = 0; i < 9 - 1; i++)
                    for (int j = i + 1; j < 9; j++)
                        if (Tiles[j%3, j/3] != 0 && Tiles[i%3, i/3] != 0 && Tiles[i%3,i/3] > Tiles[j%3,j/3])
                            invCount++;
                return invCount % 2 == 0;
            }

            public static GameOf9State GetRandom()
            {
                GameOf9State result;
                result.Tiles = new int[3, 3];
                result.Tiles[0, 0] = 4;
                result.Tiles[1, 0] = 8;
                result.Tiles[2, 0] = 0;
                result.Tiles[0, 1] = 7;
                result.Tiles[1, 1] = 2;
                result.Tiles[2, 1] = 6;
                result.Tiles[0, 2] = 1;
                result.Tiles[1, 2] = 3;
                result.Tiles[2, 2] = 5;
                return result;
            }

            public void Find(int v, out int x, out int y)
            {
                for (int i = 0; i < Tiles.GetLength(1); ++i)
                {
                    for (int j = 0; j < Tiles.GetLength(0); ++j)
                    {
                        if(Tiles[i, j] == v)
                        {
                            x = i;
                            y = j;
                            return;
                        }
                    }
                }
                x = 0;
                y = 0;
            }

            public bool Equals(GameOf9State other)
            {
                for (int i = 0; i < other.Tiles.GetLength(1); ++i)
                {
                    for (int j = 0; j < other.Tiles.GetLength(0); ++j)
                    {
                        if (Tiles[i, j] != other.Tiles[i, j]) return false;
                    }
                }
                return true;
            }

            public override bool Equals(object other)
            {
                if(other is GameOf9State)
                {
                    return ((GameOf9State)other).Equals(this);
                }
                return false;
            }

            public override int GetHashCode()
            {
                return 1095759006 + EqualityComparer<int[,]>.Default.GetHashCode(Tiles);
            }
        }

        class GameOf9Graph : Aiv.ISearchGraph<GameOf9State>
        {
            public List<Aiv.SearchTreeAction<GameOf9State>> GetActions(GameOf9State from)
            {
                var result = new List<Aiv.SearchTreeAction<GameOf9State>>();

                int zx, zy;
                from.Find(0, out zx, out zy);

                if (zx > 0)
                {
                    GameOf9State newState = new GameOf9State(from.Tiles);
                    Aiv.Algo.Swap(ref newState.Tiles[zx, zy], ref newState.Tiles[zx-1, zy]);
                    result.Add(new Aiv.SearchTreeAction<GameOf9State> { NewState = newState, Cost = 1 });
                }
                if (zx < 2)
                {
                    GameOf9State newState = new GameOf9State(from.Tiles);
                    Aiv.Algo.Swap(ref newState.Tiles[zx, zy], ref newState.Tiles[zx + 1, zy]);
                    result.Add(new Aiv.SearchTreeAction<GameOf9State> { NewState = newState, Cost = 1 });
                }
                if (zy > 0)
                {
                    GameOf9State newState = new GameOf9State(from.Tiles);
                    Aiv.Algo.Swap(ref newState.Tiles[zx, zy], ref newState.Tiles[zx, zy - 1]);
                    result.Add(new Aiv.SearchTreeAction<GameOf9State> { NewState = newState, Cost = 1 });
                }
                if (zy < 2)
                {
                    GameOf9State newState = new GameOf9State(from.Tiles);
                    Aiv.Algo.Swap(ref newState.Tiles[zx, zy], ref newState.Tiles[zx, zy + 1]);
                    result.Add(new Aiv.SearchTreeAction<GameOf9State> { NewState = newState, Cost = 1 });
                }

                return result;
            }

            public float SimpleHeuristic(GameOf9State from, GameOf9State to)
            {
                int count = 0;
                int x0, y0, x1, y1;

                for (int i = 1; i < 9; ++i)
                {
                    from.Find(i, out x0, out y0);
                    to.Find(i, out x1, out y1);
                    if (x0 != x1 || y0 != y1) ++count;
                }

                return count;
            }

            public float ManhattanHeuristic(GameOf9State from, GameOf9State to)
            {
                int count = 0;
                int x0, y0, x1, y1;

                for (int i = 1; i < 9; ++i)
                {
                    from.Find(i, out x0, out y0);
                    to.Find(i, out x1, out y1);
                    count += Aiv.Algo.ManhattanDistance(x0, y0, x1, y1);
                }

                return count;
            }

            public float Heuristic(GameOf9State from, GameOf9State to)
            {
                return SimpleHeuristic(from, to);
            }
        }

        static void Main(string[] args)
        {
            GameOf9Graph gameOf9 = new GameOf9Graph();
            var solution = Aiv.SearchTree.AStarSearch(gameOf9, GameOf9State.GetRandom(), GameOf9State.GetDefault());
            Console.WriteLine($"Solved in {solution.Iterations}");

            Grid g = new Grid(10, 10);

            g.Place(1, 0, 10, 5, Grid.ETile.Swamp);
            g.Place(2, 5, 8, 6, Grid.ETile.Wall);
            g.Place(5, 2, 6, 8, Grid.ETile.Wall);
            g.Print();

            Console.WriteLine();
            var destination = Aiv.SearchTree.AStarSearch(g, new Position2D { X = 0, Y = 0 }, new Position2D { X = 7, Y = 7 });
            foreach(var step in destination.Steps)
            {
                Console.WriteLine($"{step.X}; {step.Y}");
            }
            Console.WriteLine("Done");
        }
    }
}
