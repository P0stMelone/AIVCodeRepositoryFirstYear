using Aiv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{
    class SimpleMap
    {
        public enum MapTiles
        {
            Floor = 1,
            Grass = 5,
            Mud = 10,
            Wall = -1
        }

        public class MapState : IEquatable<MapState>
        {
            public int X;
            public int Y;

            public MapTiles[,] map;

            public float ComputeHeuristic(MapState target)
            {
                return Aiv.Algo.ManhattanDistance(X, Y, target.X, target.Y);
            }

            public bool Equals(MapState state)
            {
                return X == state.X && Y == state.Y;
            }

            public List<Aiv.SearchTreeAction<MapState>> GetActions()
            {
                List<Aiv.SearchTreeAction<MapState>> actions = new List<Aiv.SearchTreeAction<MapState>>();

                if (X > 0 && map[X - 1, Y] != MapTiles.Wall)                    actions.Add(new Aiv.SearchTreeAction<MapState> { Cost = (float)map[X - 1, Y], NewState = new MapState { X = X - 1, Y = Y, map = map } });
                if (X < map.GetLength(0) - 1 && map[X + 1, Y] != MapTiles.Wall) actions.Add(new Aiv.SearchTreeAction<MapState> { Cost = (float)map[X + 1, Y], NewState = new MapState { X = X + 1, Y = Y, map = map } });
                if (Y > 0 && map[X, Y - 1] != MapTiles.Wall)                    actions.Add(new Aiv.SearchTreeAction<MapState> { Cost = (float)map[X, Y - 1], NewState = new MapState { X = X, Y = Y - 1, map = map } });
                if (Y < map.GetLength(1) - 1 && map[X, Y + 1] != MapTiles.Wall) actions.Add(new Aiv.SearchTreeAction<MapState> { Cost = (float)map[X, Y + 1], NewState = new MapState { X = X, Y = Y + 1, map = map } });

                return actions;
            }

            public List<Aiv.SearchTreeAction<MapState>> GetGreedyActions()
            {
                List<Aiv.SearchTreeAction<MapState>> actions = new List<Aiv.SearchTreeAction<MapState>>();

                if (X > 0 && map[X - 1, Y] != MapTiles.Wall)                    actions.Add(new Aiv.SearchTreeAction<MapState> { Cost = 0, NewState = new MapState { X = X - 1, Y = Y, map = map } });
                if (X < map.GetLength(0) - 1 && map[X + 1, Y] != MapTiles.Wall) actions.Add(new Aiv.SearchTreeAction<MapState> { Cost = 0, NewState = new MapState { X = X + 1, Y = Y, map = map } });
                if (Y > 0 && map[X, Y - 1] != MapTiles.Wall)                    actions.Add(new Aiv.SearchTreeAction<MapState> { Cost = 0, NewState = new MapState { X = X, Y = Y - 1, map = map } });
                if (Y < map.GetLength(1) - 1 && map[X, Y + 1] != MapTiles.Wall) actions.Add(new Aiv.SearchTreeAction<MapState> { Cost = 0, NewState = new MapState { X = X, Y = Y + 1, map = map } });

                return actions;
            }

            public override bool Equals(object obj)
            {
                return obj is MapState state && Equals(state);
            }

            public override int GetHashCode()
            {
                int hashCode = 1861411795;
                hashCode = hashCode * -1521134295 + X.GetHashCode();
                hashCode = hashCode * -1521134295 + Y.GetHashCode();
                return hashCode;
            }
        }

        public struct MapGraphAStar : ISearchGraph<MapState>
        {
            public List<SearchTreeAction<MapState>> GetActions(MapState State)
            {
                return State.GetActions();
            }

            public float Heuristic(MapState From, MapState To)
            {
                return From.ComputeHeuristic(To);
            }
        }

        public struct MapGraphDjikstra : ISearchGraph<MapState>
        {
            public List<SearchTreeAction<MapState>> GetActions(MapState State)
            {
                return State.GetActions();
            }

            public float Heuristic(MapState From, MapState To)
            {
                return 0;
            }
        }

        public struct MapGraphGreedy: ISearchGraph<MapState>
        {
            public List<SearchTreeAction<MapState>> GetActions(MapState State)
            {
                return State.GetGreedyActions();
            }

            public float Heuristic(MapState From, MapState To)
            {
                return From.ComputeHeuristic(To);
            }
        }

        public static void Run()
        {
            MapTiles[,] map = new MapTiles[10, 10];

            /* Map
             * 10,0       10,10
             * ------------
             * |     T  MM|
             * |        MM|
             * |        MM|
             * |        MM|
             * |  GGGGGGMM|
             * |        MM|
             * |        MM|
             * |  WWWWWWMM|
             * |        MM|
             * |     F  MM|
             * ------------
             * 0,0        0,10
             * F = from
             * T = to
             * M = mud
             * W = wall
             * G = grass
             */

            /*inizializza la mappa mettendo il pavimento*/
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = MapTiles.Floor;
                }
            }

            /*aggiungi un muro*/
            for (int i = 2; i < map.GetLength(0) - 2; i++)
            {
                map[i, 3] = MapTiles.Wall;
            }

            /*metti dell'erba*/
            for (int i = 2; i < map.GetLength(0) - 2; i++)
            {
                map[i, 5] = MapTiles.Grass;
            }


            /*e del fango*/
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[8, j] = MapTiles.Mud;
                map[9, j] = MapTiles.Mud;
            }

            Action<Aiv.SearchTree.Result<MapState>> printResult = (result) =>
            {
                Console.WriteLine($"Cost:{result.Solution.Cost}; Iterations:{result.Iterations}");
            };

            MapGraphAStar mapGraphAStar;
            MapGraphDjikstra mapGraphDjikstra;
            MapGraphGreedy mapGraphGreedy;

            printResult(Aiv.SearchTree.BreadthFirstSearch(mapGraphAStar, new MapState { X = 5, Y = 0, map = map }, new MapState { X = 5, Y = 9, map = map }));
            printResult(Aiv.SearchTree.AStarSearch(mapGraphGreedy, new MapState { X = 5, Y = 0, map = map }, new MapState { X = 5, Y = 9, map = map }));
            printResult(Aiv.SearchTree.AStarSearch(mapGraphDjikstra, new MapState { X = 5, Y = 0, map = map }, new MapState { X = 5, Y = 9, map = map }));
            printResult(Aiv.SearchTree.AStarSearch(mapGraphAStar, new MapState { X = 5, Y = 0, map = map }, new MapState { X = 5, Y = 9, map = map }));
        }
    }
}
