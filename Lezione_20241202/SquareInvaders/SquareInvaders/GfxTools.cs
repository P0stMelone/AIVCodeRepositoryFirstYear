using System;
using Aiv.Draw;

namespace SquareInvaders {
    public static class GfxTools {

        public static Window Win;

        public static void Init (Window win) {
            Win = win;
        }

        public static void Clear () {
            for (int i = 0; i < Win.Bitmap.Length; i++) {
                Win.Bitmap[i] = 0;
            }
        }

        public static void PutPixel (int x, int y, byte r, byte g, byte b) {
            if (x < 0 || x > Win.Width || y < 0 || y > Win.Height) return;
            int index = (y * Win.Width + x) * 3;
            Win.Bitmap[index] = r;
            Win.Bitmap[index + 1] = g;
            Win.Bitmap[index + 2] = b;
        }

        public static void DrawHorizontalLine (int x, int y, int width, byte r, byte g, byte b) {
            for (int i = 0; i < width; i++) {
                PutPixel(x + i, y, r, g, b);
            }
        }

        public static void DrawVerticalLine (int x, int y, int height, byte r, byte g, byte b) {
            for (int i = 0; i < height; i++) {
                PutPixel(x, y + i, r, g, b);
            }
        }

        public static void DrawRect (int x, int y, int width, int height, byte r, byte g, byte b) {
            for (int i = 0; i < width; i++) {
                DrawVerticalLine(x + i, y, height, r, g, b);
            }
        }

    }
}
