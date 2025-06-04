
using Aiv.Fast2D.Component;

namespace Aiv.Tiled {
    public class LayerOld {

        public TileOld[,] Tiles;

        public int OffsetX;
        public int OffsetY;

        public LayerOld(TileOld[,] tiles, int offsetX, int offsetY) {
            int width = tiles.GetLength(0);
            int height = tiles.GetLength(1);
            Tiles = tiles;

            OffsetX = offsetX;
            OffsetY = offsetY;
        }

    }

}
