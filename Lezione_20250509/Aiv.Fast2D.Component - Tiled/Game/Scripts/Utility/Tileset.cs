using Aiv.Fast2D;
using Aiv.Fast2D.Component;
using static System.Net.Mime.MediaTypeNames;

namespace Aiv.Tiled {
    public class Tileset {

        public Texture Source {
            get; set;
        }

        public int TileWidth {
            get; set;
        }

        public int TileHeight {
            get; set;
        }

        private int firstgid;
        private int columns;

        public Tileset(Texture tex, int tileWidth, int tileHeight, int firstgid, int columns) {
            Source = tex;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            this.firstgid = firstgid;
            this.columns = columns;
        }

        public Tileset(string textureName, int tileWidth, int tileHeight, int firstgid, int columns) {
            Source = GfxMgr.GetTexture(textureName);
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            this.firstgid = firstgid;
            this.columns = columns;
        }

        public int HorizontalOffset(int tileID) {
            tileID -= firstgid;
            return (tileID % columns) * TileWidth;
        }

        public int VerticalOffset(int tileID) {
            tileID -= firstgid;
            return (tileID / columns) * TileHeight;
        }

    }
}
