using System;
using System.Collections.Generic;
using Aiv.Draw;

namespace Lezione_20241122 {
    class Program {
        static void Main(string[] args) {
            //Ex1();
            //Ex2();
            //Ex2Bis();
            //Ex3();
            //Ex3Bis();
            //CodeFrameRateDipendent();
            Ex4();
        }

        static void CodeFrameRateDipendent () {
            Window win = new Window(800, 600, "FrameRateDipendent", PixelFormat.RGB);
            int rectSize = 100;
            int speedPerSecond = 50;
            int x = win.Width / 2- rectSize / 2;
            int y = win.Height / 2- rectSize / 2;

            while (win.IsOpened) {
                if (win.GetKey(KeyCode.W)) {
                    y -= speedPerSecond;
                } else if (win.GetKey(KeyCode.S)) {
                    y += speedPerSecond;
                }

                DrawRect(win, x, y, rectSize, rectSize, Color.Red());
                win.Blit();
                ClearScreen(win);
            }
        }

        static void Ex4() {
            Window win = new Window(800, 600, "FrameRateDipendent", PixelFormat.RGB);
            int rectSize = 100;
            int speedPerSecond = 500;
            int x = win.Width / 2 - rectSize / 2;
            int y = win.Height / 2 - rectSize / 2;

            while (win.IsOpened) {
                if (win.GetKey(KeyCode.W)) {
                    y -= (int)(speedPerSecond * win.DeltaTime);
                } else if (win.GetKey(KeyCode.S)) {
                    y += (int)(speedPerSecond * win.DeltaTime);
                }
                if (win.GetKey(KeyCode.A)) {
                    x -= (int)(speedPerSecond * win.DeltaTime);
                } else if (win.GetKey(KeyCode.D)) {
                    x += (int)(speedPerSecond * win.DeltaTime);
                }
                if (x < 0) x = 0;
                if (x > win.Width - rectSize) x = win.Width - rectSize;
                if (y < 0) y = 0;
                if (y > win.Height - rectSize) y = win.Height - rectSize;
                DrawRect(win, x, y, rectSize, rectSize, Color.Red());
                win.Blit();
                ClearScreen(win);
            }
        }


        static void Ex1() {
            int winWidth = AskInteger("Inserisci la larghezza");
            int winHeight = AskInteger("Inserisci l'altezza");
            Window win = new Window(winWidth, winHeight, "Ex1", PixelFormat.RGB);
            int rectWidth = win.Width / 4;
            int rectHeight = win.Height / 4;
            bool isRed;
            while (win.IsOpened) {
                for (int i = 0; i < 4; i++) {
                    isRed = i % 2 == 0;
                    for (int j = 0; j < 4; j++) {
                        DrawRect(win, j * rectWidth, i * rectHeight, rectWidth, rectHeight,
                            isRed ? Color.Red() : Color.Green());
                        isRed = !isRed;
                    }
                }
                win.Blit();
                ClearScreen(win);
            }
        }

        static void Ex2() {
            int winWidth = AskInteger("Inserisci la larghezza");
            int winHeight = AskInteger("Inserisci l'altezza");
            Window win = new Window(winWidth, winHeight, "Ex1", PixelFormat.RGB);
            int rectWidth = win.Width / 4;
            int rectHeight = win.Height / 4;
            bool isRed;
            while (win.IsOpened) {
                for (int i = 0; i < 4; i++) {
                    isRed = i % 2 == 0;
                    if (win.GetKey(KeyCode.C)) {
                        isRed = !isRed;
                    }
                    for (int j = 0; j < 4; j++) {
                        DrawRect(win, j * rectWidth, i * rectHeight, rectWidth, rectHeight,
                            isRed ? Color.Red() : Color.Green());
                        isRed = !isRed;
                    }
                }
                win.Blit();
                ClearScreen(win);
            }
        }

        static void Ex2Bis() {
            int winWidth = AskInteger("Inserisci la larghezza");
            int winHeight = AskInteger("Inserisci l'altezza");
            Window win = new Window(winWidth, winHeight, "Ex1", PixelFormat.RGB);
            int rectWidth = win.Width / 4;
            int rectHeight = win.Height / 4;
            bool isRed;
            bool previousFrameWasPressed = false;
            bool startRed = true;
            while (win.IsOpened) {
                if (previousFrameWasPressed && !win.GetKey(KeyCode.C)) {
                    startRed = !startRed;
                }
                for (int i = 0; i < 4; i++) {
                    isRed = i % 2 == 0;
                    if (!startRed) {
                        isRed = !isRed;
                    }
                    for (int j = 0; j < 4; j++) {
                        DrawRect(win, j * rectWidth, i * rectHeight, rectWidth, rectHeight,
                            isRed ? Color.Red() : Color.Green());
                        isRed = !isRed;
                    }
                }
                previousFrameWasPressed = win.GetKey(KeyCode.C);
                win.Blit();
                ClearScreen(win);
            }
        }

        static void Ex3() {
            int winWidth = AskInteger("Inserisci la larghezza");
            int winHeight = AskInteger("Inserisci l'altezza");
            Window win = new Window(winWidth, winHeight, "Ex1", PixelFormat.RGB);
            int rectSize = 100;
            while (win.IsOpened) {
                DrawRect(win, win.MouseX, win.MouseY, rectSize, rectSize, Color.Red());
                win.Blit();
                ClearScreen(win);
            }
        }

        static void Ex3Bis() {
            int winWidth = AskInteger("Inserisci la larghezza");
            int winHeight = AskInteger("Inserisci l'altezza");
            Window win = new Window(winWidth, winHeight, "Ex1", PixelFormat.RGB);
            int rectSize = 100;
            while (win.IsOpened) {
                DrawRect(win, win.MouseX - rectSize / 2, win.MouseY - rectSize / 2, rectSize, rectSize, 
                    win.MouseLeft ? Color.Green() : Color.Red());
                win.Blit();
                ClearScreen(win);
            }
        }

        static void PutPixel(Window win, int x, int y, byte r, byte g, byte b) {
            //if pixel is outside the screen don't draw it
            if (x < 0 || y < 0 || x >= win.Width || y >= win.Height) {
                return;
            }
            //compute pixel index (Red)
            int index = 3 * (y * win.Width + x);
            win.Bitmap[index] = r;
            win.Bitmap[index + 1] = g;
            win.Bitmap[index + 2] = b;
        }

        static void DrawHorizontalLine(Window win, int x, int y, int width,
            byte r, byte g, byte b) {
            for (int i = 0; i < width; i++) {
                PutPixel(win, x + i, y, r, g, b);
            }
        }

        static void DrawVerticalLine(Window win, int x, int y, int height,
            byte r, byte g, byte b) {
            for (int i = 0; i < height; i++) {
                PutPixel(win, x, y + i, r, g, b);
            }
        }

        static void DrawRect(Window win, int x, int y, int width, int height,
            Color color) {
            for (int i = 0; i < height; i++) {
                DrawHorizontalLine(win, x, y + i, width, color.R, color.G, color.B);
            }
        }

        static void DrawRect2(Window win, int x, int y, int width, int height,
            byte r, byte g, byte b) {
            for (int i = 0; i < width; i++) {
                DrawVerticalLine(win, x + i, y, height, r, g, b);
            }
        }

        static void ClearScreen(Window win) {
            for (int i = 0; i < win.Bitmap.Length; i++) {
                win.Bitmap[i] = 0;
            }
        }

        static int AskInteger(string message) {
            int counter = 0;
            int userInput;
            bool result;
            do {
                if (counter != 0) {
                    Console.WriteLine("Non hai inserito un numero intero. Riprova");
                } else {
                    Console.WriteLine(message);
                }
                result = int.TryParse(Console.ReadLine(), out userInput);
            } while (!result);
            return userInput;
        }

        struct Color {

            public byte R;
            public byte G;
            public byte B;

            public Color(byte r, byte g, byte b) {
                R = r;
                G = g;
                B = b;
            }

            public static Color Red() {
                return new Color(255, 0, 0);
            }

            public static Color Green() {
                return new Color(0, 255, 0);
            }
        }
    }
}
