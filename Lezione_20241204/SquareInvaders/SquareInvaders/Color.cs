using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvaders {
    public struct Color {

        public byte R;
        public byte G;
        public byte B;

        public Color (byte r, byte g, byte b) {
            R = r;
            G = g;
            B = b;
        }

        public Color Red () {
            return new Color(255, 0, 0);
        }

        public Color Green () {
            return new Color(0, 255, 0);
        }

        public Color Blue () {
            return new Color(0, 0, 255);
        }

    }
}
