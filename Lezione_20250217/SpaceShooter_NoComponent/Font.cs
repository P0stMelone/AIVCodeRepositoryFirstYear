using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter_NoComponent {
    public class Font {

        protected int numCol;
        protected int firstVal; //valore ascii del primo carattere (quello in alto a sinistra)

        public int CharacterWidth {
            get;
            protected set;
        }
        public int CharacterHeight {
            get;
            protected set;
        }

        public string TextureName { get; protected set; }
        public Texture Texture { get; protected set; }

        public Font (string textureName, string texturePath, int numColumns, 
            int firstCharacterASCIIValue, int charWidth, int charHeight) {
            TextureName = textureName;
            Texture = GfxMgr.AddTexture(textureName, texturePath);
            firstVal = firstCharacterASCIIValue;
            CharacterWidth = charWidth;
            CharacterHeight = charHeight;
            numCol = numColumns;
        }

        public virtual Vector2 GetOffset (char c) {
            int cVal = c;
            int delta = cVal - firstVal;
            int x = delta % numCol;
            int y = delta / numCol;
            return new Vector2(x * CharacterWidth, y * CharacterHeight);
        }

    }
}
