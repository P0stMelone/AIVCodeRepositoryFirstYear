using Aiv.Fast2D;
using OpenTK;

namespace SpaceShooter_NoComponent {
    public static class Game {


        private static Window win;
        public static Window Win {
            get {
                return win;
            }
        }

        private static Player player;
        private static Background background;


        public static void Init () {
            win = new Window(1280, 720, "Space Shooter");
            win.SetVSync(false);
            player = new Player(new Vector2(win.Width / 2, win.Height / 2), "Assets/player_ship.png");
            background = new Background("Assets/spaceBg.png");
        }

        public static void Play () {

            while (win.IsOpened) {


                if (win.GetKey(KeyCode.Esc)) {
                    break;
                }



                //Update
                player.Update();

                //Rendering
                background.Draw();
                player.Draw();

                Win.Update(); //swappa il back buffer con il front buffer
            }

        }

    }

}
