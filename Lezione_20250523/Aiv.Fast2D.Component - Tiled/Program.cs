using System;
using System.Xml.Linq;

namespace Aiv.Fast2D.Component {

    public enum Layer {
        Player = 1,
        PlayerBullet = 2,
        Enemy = 4,
        EnemyBullet = 8,
        Tile = 16
    }

    class Program {

        static void Main(string[] args) {

            Game.Init(1280, 720, "SpaceShooter", new PlayScene(), 720, 10);
            Game.Play();
        }

    }

}

#region TiledFirstImport
//public struct TilesetInfo {

//    public int tileWidth; //sono espressi in pixel
//    public int tileHeight; //sono espressi in pixel
//    public int columns;
//    public int tileCount;
//    public string imageSource;
//    public int firstgid;
//    public int layerWidth; //sono espresse in n di tile
//    public int layerHeight; //sono espresse in n di tile
//    public Tiled.TileOld[,] layerTiles;

//    public TilesetInfo(string filePath) {
//        tileWidth = 0;
//        tileHeight = 0;
//        columns = 0;
//        tileCount = 0;
//        imageSource = "";
//        firstgid = 0;
//        layerWidth = 0;
//        layerHeight = 0;
//        layerTiles = new Tiled.TileOld[1, 1];
//        OpenXDocument(filePath);
//    }

//    public void OpenXDocument(string filePath) {
//        XDocument src = XDocument.Load(filePath);
//        XElement map = src.Element("map");
//        XAttribute version = map.Attribute("version");
//        XAttribute width = map.Attribute("width");
//        XAttribute height = map.Attribute("height");
//        Console.WriteLine($"versione: {version.Value} width: {(int)width} height: {(int)height}");
//        XElement tileset = map.Element("tileset");
//        XAttribute tilesetSource = tileset.Attribute("source");
//        firstgid = (int)tileset.Attribute("firstgid");
//        XElement layer = map.Element("layer");
//        layerWidth = (int)layer.Attribute("width");
//        layerHeight = (int)layer.Attribute("height");
//        layerTiles = new Tiled.TileOld[layerWidth, layerHeight];
//        int counter = 0;
//        XElement data = layer.Element("data");
//        foreach(XElement tile in data.Elements("tile")) {
//            layerTiles[counter % layerWidth, counter / layerWidth] = new Tiled.TileOld((uint)tile.Attribute("gid"));
//            counter++;
//        }
//        Console.WriteLine($"firstgid: {(int)firstgid}");
//        if (tilesetSource != null) {
//            XDocument newSource = XDocument.Load($"Game/Assets/{tilesetSource.Value}");
//            tileset = newSource.Element("tileset");
//        }
//        ReadTilesetInfo(tileset);
//    }

//    public void ReadTilesetInfo(XElement tileset) {
//        tileWidth = (int)tileset.Attribute("tilewidth");
//        tileHeight = (int)tileset.Attribute("tileheight");
//        tileCount = (int)tileset.Attribute("tilecount");
//        columns = (int)tileset.Attribute("columns");
//        XElement image = tileset.Element("image");
//        imageSource = image.Attribute("source").Value;

//        Console.WriteLine($"width: {(int)tileWidth}, height: {(int)tileHeight}, tileCount: {(int)tileCount}," +
//            $"columns: {(int)columns}, imageSource: {(string)imageSource}");
//    }

//    }


//}
#endregion
