using Aiv.Fast2D;
using OpenTK;

namespace Lezione_20241228 {
    class Program {
        static void Main(string[] args) {
            //Esercizio1();
            //Esercizio1Bis();
            //Esercizio2();
            //Esercizio2Bis();
            //Esercizio3();
            Esercizio4();
        }

        static void Esercizio1 () {
            Window window = new Window(1024, 576, "Esercizio1");
            window.SetVSync(true);
            //window.SetFullScreen(true);

            Mesh square = new Mesh();
            square.v = new float[] {
                0,0,
                0,100,
                100,0,
                100,0,
                0,100,
                100,100
            };
            square.Update();//aggiorna anche nella scheda video la struttura della mesh, che prima di questa riga
            //era aggiornata solo nella RAM

            square.position = new Vector2(window.Width / 2, window.Height / 2);

            while (window.IsOpened) {
                square.DrawColor(255, 0, 0, 255);

                window.Update(); //swappa il back buffer con il front buffer
            }


        }

        static void Esercizio1Bis() {
            Window window = new Window(1024, 576, "Esercizio1");
            window.SetVSync(true);
            //window.SetFullScreen(true);

            Mesh square = new Mesh();
            square.v = new float[] {
                -50,-50,
                -50,50,
                50,-50,
                50,-50,
                -50,50,
                50,50
            };
            square.Update();//aggiorna anche nella scheda video la struttura della mesh, che prima di questa riga
            //era aggiornata solo nella RAM

            square.position = new Vector2(window.Width / 2, window.Height / 2);

            while (window.IsOpened) {
                square.DrawColor(255, 0, 0, 255);

                window.Update(); //swappa il back buffer con il front buffer
            }


        }

        static void Esercizio2() {
            Window window = new Window(1024, 576, "Esercizio1");
            window.SetVSync(true);
            //window.SetFullScreen(true);

            Texture texture = new Texture("Assets/LogoAIV.png");
            Mesh square = new Mesh();
            square.v = new float[] {
                0,0,
                0,100,
                100,0,
                100,0,
                0,100,
                100,100
            };
            square.uv = new float[] {
                0,0,
                0,1,
                1,0,
                1,0,
                0,1,
                1,1
            };
            square.Update();//aggiorna anche nella scheda video la struttura della mesh, che prima di questa riga
            //era aggiornata solo nella RAM
            Mesh notStrechedMesh = new Mesh();
            notStrechedMesh.v = new float[] {
                0,0,
                0,texture.Height,
                texture.Width,0,
                texture.Width,0,
                0,texture.Height,
                texture.Width,texture.Height
            };
            notStrechedMesh.uv = new float[] {
                0,0,
                0,1,
                1,0,
                1,0,
                0,1,
                1,1
            };
            notStrechedMesh.Update();
            square.position = new Vector2(window.Width / 2, window.Height / 2);
            notStrechedMesh.position = new Vector2(50, 50);

            while (window.IsOpened) {
                square.DrawTexture(texture);
                notStrechedMesh.DrawTexture(texture);
                window.Update(); //swappa il back buffer con il front buffer
            }
        }

        static void Esercizio2Bis() {
            Window window = new Window(1024, 576, "Esercizio1");
            window.SetVSync(true);
            //window.SetFullScreen(true);

            Texture texture = new Texture("Assets/LogoAIV.png");
            Mesh square = new Mesh();
            square.v = new float[] {
                0,0,
                0,100,
                100,0,
                100,0,
                0,100,
                100,100
            };
            square.uv = new float[] {
                0,0,
                0,100f/texture.Height,
                100f/texture.Width,0,
                100f/texture.Width,0,
                0,100f/texture.Height,
                100f/texture.Width,100f/texture.Height
            };
            square.Update();//aggiorna anche nella scheda video la struttura della mesh, che prima di questa riga
            //era aggiornata solo nella RAM
            Mesh notStrechedMesh = new Mesh();
            notStrechedMesh.v = new float[] {
                0,0,
                0,texture.Height,
                texture.Width,0,
                texture.Width,0,
                0,texture.Height,
                texture.Width,texture.Height
            };
            notStrechedMesh.uv = new float[] {
                0,0,
                0,1,
                1,0,
                1,0,
                0,1,
                1,1
            };
            notStrechedMesh.Update();
            square.position = new Vector2(window.Width / 2, window.Height / 2);
            notStrechedMesh.position = new Vector2(50, 50);

            while (window.IsOpened) {
                square.DrawTexture(texture);
                notStrechedMesh.DrawTexture(texture);
                window.Update(); //swappa il back buffer con il front buffer
            }
        }

        static void Esercizio3 () {
            Window window = new Window(1024, 576, "Esercizio1");
            window.SetVSync(true);
            //window.SetFullScreen(true);

            Texture texture = new Texture("Assets/LogoAIV.png");

            //Sprite mySprite = new Sprite(100, 100);
            Sprite mySprite = new Sprite(texture.Width, texture.Height);
            mySprite.position = new Vector2(window.Width/2, window.Height/2);
            mySprite.pivot = new Vector2(mySprite.Width / 2, mySprite.Height / 2);
            //mySprite.EulerRotation = 90;


            while (window.IsOpened) {
                mySprite.position = window.MousePosition;
                mySprite.EulerRotation += 90 * window.DeltaTime;

                mySprite.DrawTexture(texture);

                window.Update();
            }
        }

        static void Esercizio4() {
            Window window = new Window(1024, 576, "Esercizio1");
            window.SetVSync(true);
            window.SetClearColor(0, 0, 255);
            //window.SetFullScreen(true);

            Texture texture = new Texture("Assets/LogoAIV.png");

            //Sprite mySprite = new Sprite(100, 100);
            Sprite mySpriteFirstScreen = new Sprite(texture.Width, texture.Height);
            mySpriteFirstScreen.position = new Vector2(window.Width / 2, window.Height / 2);
            mySpriteFirstScreen.pivot = new Vector2(mySpriteFirstScreen.Width / 2, mySpriteFirstScreen.Height / 2);
            //mySprite.EulerRotation = 90;
            Sprite mySpriteSecondScreen = new Sprite(texture.Width, texture.Height);
            mySpriteSecondScreen.position = new Vector2(window.Width / 2, window.Height / 2);
            mySpriteSecondScreen.pivot = new Vector2(mySpriteFirstScreen.Width / 2, mySpriteFirstScreen.Height / 2);

            while (window.IsOpened) {
                window.SetViewport(0, 0, window.Width, window.Height /2);
                mySpriteFirstScreen.position = window.MousePosition;
                mySpriteFirstScreen.position.Y /= 2;
                mySpriteFirstScreen.EulerRotation += 90 * window.DeltaTime;

                mySpriteFirstScreen.DrawTexture(texture);
                window.SetViewport(0, window.Height /2, window.Width, window.Height / 2);
                mySpriteSecondScreen.position = window.MousePosition;
                mySpriteSecondScreen.position.Y /= 2;
                mySpriteSecondScreen.position.Y += window.Height / 2;
                mySpriteSecondScreen.EulerRotation -= 90 * window.DeltaTime;
                mySpriteSecondScreen.DrawTexture(texture);
                window.Update();
            }
        }
    }
}
