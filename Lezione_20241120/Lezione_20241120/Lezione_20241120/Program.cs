using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Lezione_20241120 {
    class Program {
        static void Main(string[] args) {
            //PutPixelExample();
            //DrawHorizontalLineExample();
            //Ex3Noob();
            //Ex3LessNoob();
            Ex3();
        }

        static void PutPixelExample() {
            Window myWindow = new Window(800, 600, "Aiv.Draw Introduction", PixelFormat.RGB);

            int W = myWindow.Width;
            int H = myWindow.Height;

            while (myWindow.IsOpened) {

                for (int x = 4; x < 12; x++) {
                    for (int y = 0; y < 8; y++) {
                        PutPixel(myWindow, x, y, 0, 255, 0);
                    }
                }

                myWindow.Blit();

            }
        }

        static void DrawHorizontalLineExample() {
            Window myWindow = new Window(800, 600, "Aiv.Draw Introduction", PixelFormat.RGB);

            int W = myWindow.Width;
            int H = myWindow.Height;

            while (myWindow.IsOpened) {
                DrawHorizontalLine(myWindow, 10, 10, 300, 0, 255, 0);
                myWindow.Blit();
            }


        }

        static void Ex3Noob () {
            const int linesHeight = 8;

            Window myWindow = new Window(800, 600, "Aiv.Draw Introduction", PixelFormat.RGB);

            int W = myWindow.Width;
            int H = myWindow.Height;

            while (myWindow.IsOpened) {
                for (int i = 0; i < linesHeight; i++) {
                    DrawHorizontalLine(myWindow, 0, i, myWindow.Width, 255, 0, 0);
                }
                for (int i = myWindow.Height/2- linesHeight/2; i < myWindow.Height/2+linesHeight/2; i++) {
                    DrawHorizontalLine(myWindow, 0, i, myWindow.Width, 255, 0, 0);
                }
                for (int i = myWindow.Height - linesHeight; i < myWindow.Height; i++) {
                    DrawHorizontalLine(myWindow, 0, i, myWindow.Width, 255, 0, 0);
                }
                myWindow.Blit();
            }
        }

        static void Ex3LessNoob () {
            const int linesHeight = 8;

            Window myWindow = new Window(800, 600, "Aiv.Draw Introduction", PixelFormat.RGB);

            while (myWindow.IsOpened) {
                DrawThickHorizontalLine(myWindow, 0, linesHeight, 255, 0, 0);
                DrawThickHorizontalLine(myWindow, myWindow.Height / 2 - linesHeight / 2, linesHeight, 255, 0, 0);
                DrawThickHorizontalLine(myWindow, myWindow.Height - linesHeight, linesHeight, 255, 0, 0);
                myWindow.Blit();
            }
        }

        static void Ex3 () {
            const int linesHeight = 8;
            const int linesNumber = 4;

            Window myWindow = new Window(800, 600, "Aiv.Draw Introduction", PixelFormat.RGB);
            int spaceBetween = myWindow.Height / (linesNumber - 1) - (linesHeight / (linesNumber - 1));
            for (int i = 0; i < linesNumber; i++) {
                DrawThickHorizontalLine(myWindow, spaceBetween * i, linesHeight, 255, 0, 0);
            }
            while (myWindow.IsOpened) {

                myWindow.Blit();
                //Clear(myWindow);
            }
        }

        static void Clear (Window win) {
            for (int i = 0; i < win.Bitmap.Length; i++) {
                win.Bitmap[i] = 0;
            }
        }

        static void DrawThickHorizontalLine(Window win, int y, int thickness, byte r, byte g, byte b) {
            for (int i = 0; i < thickness; i++) {
                DrawHorizontalLine(win, 0, y + i, win.Width, r, g, b);
            }
        }

        static void DrawHorizontalLine(Window win, int x, int y, int width, byte r, byte g, byte b) {
            for (int i = 0; i < width; i++) {
                PutPixel(win, x + i, y, r, g, b);
            }
        }

        static void DrawVertiacalLine(Window win, int x, int y, int height, byte r, byte g, byte b) {
            for (int i = 0; i < height; i++) {
                PutPixel(win, x, y + i, r, g, b);
            }
        }

        static void PutPixel(Window win, int x, int y, byte r, byte g, byte b) {
            if (x < 0 || x >= win.Width || y < 0
                    || y >= win.Height) return; //il pixel non sta nella finestra
            int indexR = (y * win.Width + x) * 3;
            win.Bitmap[indexR] = r;
            win.Bitmap[indexR + 1] = g;
            win.Bitmap[indexR + 2] = b;
        }
    }
}
