using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component {
    internal class TiledMapPathfindingGraph : ISearchGraph<Vector2> {

        Tiled.Layer grid;

        public TiledMapPathfindingGraph(Tiled.Layer wallLayer) {
            grid = wallLayer;
        }

        public List<SearchTreeAction<Vector2>> GetActions(Vector2 State) {
            List<SearchTreeAction<Vector2>> result = new List<SearchTreeAction<Vector2>>();
            if (grid.Tiles[(int)State.X+1, (int)State.Y].Gid == 0) {
                result.Add(new SearchTreeAction<Vector2> { Cost = 1, NewState = new Vector2(State.X + 1, State.Y) });
            }
            if (grid.Tiles[(int)State.X - 1, (int)State.Y].Gid == 0) {
                result.Add(new SearchTreeAction<Vector2> { Cost = 1, NewState = new Vector2(State.X  -1, State.Y) });
            }
            if (grid.Tiles[(int)State.X , (int)State.Y + 1].Gid  == 0) {
                result.Add(new SearchTreeAction<Vector2> { Cost = 1, NewState = new Vector2(State.X , State.Y + 1) });
            }
            if (grid.Tiles[(int)State.X, (int)State.Y - 1].Gid == 0) {
                result.Add(new SearchTreeAction<Vector2> { Cost = 1, NewState = new Vector2(State.X, State.Y - 1) });
            }
            return result;
        }

        public float Heuristic(Vector2 From, Vector2 To) {
            return (From - To).Length;
        }
    }
}
