using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvaders {
    public class Rect {

        private Vector2 position;
        private int width;
        private int height;
        private Color color;

        public int GetWidth () {
            return width;
        }

        public int GetHeight () {
            return height;
        }

        public Rect (float xPos, float yPos, int w, int h, Color col) {
            position.X = xPos;
            position.Y = yPos;
            color = col;
            width = w;
            height = h;
        }

        public void Translate (float x, float y) {
            position.X += x;
            position.Y += y;
        }

        public void SetPosition (Vector2 position) {
            this.position = position;
        }

        public void Draw () {
            GfxTools.DrawRect((int)position.X, (int)position.Y, width, height,
                color.R, color.G, color.B);
        }
    }
}
