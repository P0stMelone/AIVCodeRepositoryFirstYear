
using Aiv.Fast2D.Component;

namespace Aiv.Tiled {
    public class Layer {

        public TileID[,] Tiles;

        public int OffsetX;
        public int OffsetY;

        public Layer(int[,] tiles, int offsetX, int offsetY) {
            int width = tiles.GetLength(0);
            int height = tiles.GetLength(1);
            Tiles = new TileID[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Tiles[x, y].Gid = tiles[x, y];
                }
            }

            OffsetX = offsetX;
            OffsetY = offsetY;
        }

    }

    public struct TileID {

        public int Gid;

    }
}
