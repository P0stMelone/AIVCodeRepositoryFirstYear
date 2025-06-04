using Aiv.Tiled;
using OpenTK;

namespace Aiv.Fast2D.Component {
    public class DrawMap : Component, IDrawable {

        private MapMesh mapMesh;

        public DrawMap(GameObject gameObject, Map map, Vector2 scale) : base(gameObject) {

            mapMesh = new MapMesh(map);
            mapMesh.Scale = new Vector2(Game.PixelsToUnit(scale.X), Game.PixelsToUnit(scale.Y));

        }

        public DrawLayer Layer { 
            get { return DrawLayer.Background; }
        }

        public void Draw() {
            mapMesh.Draw();
        }

    }
}
