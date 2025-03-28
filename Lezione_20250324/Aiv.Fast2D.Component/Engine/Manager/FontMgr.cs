using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public static class FontMgr {

        private static Dictionary<string, Font> fonts;
        private static Font defaultFont;

        public static Font DefaultFont {
            get { return defaultFont; }
        }

        static FontMgr() {
            fonts = new Dictionary<string, Font>();
        }

        public static Font AddFont(string fontName, string texturePath, 
            int numColumns, int firstCharacterASCIIValue, int charWidth, int charHeight) {
            Font font = new Font(fontName, texturePath, numColumns, firstCharacterASCIIValue, charWidth, charHeight);
            if (defaultFont == null) defaultFont = font;
            fonts.Add(fontName, font);
            return font;
        }

        public static Font GetFont(string fontName) {
            if (!fonts.ContainsKey(fontName)) return defaultFont;
            return fonts[fontName];
        }

        public static void ClearAll() {
            fonts.Clear();
            defaultFont = null;
        }

    }
}
