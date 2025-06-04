using Aiv.Tiled;
using System;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public class TileSprite : Component, IDrawable {

        private Tiled.TileSprite tileSprite;

        public TileSprite(GameObject gameObject, List<Tileset> tilesets, Tile tile) : base(gameObject) {
            tileSprite = new Tiled.TileSprite(tilesets, tile);
        }

        public DrawLayer Layer { 
            get { return DrawLayer.Foreground; }
        }


        public void Draw() {
            tileSprite.Draw();
        }
    }
}
