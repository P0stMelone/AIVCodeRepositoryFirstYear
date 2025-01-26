using Aiv.Draw;

namespace SquareInvaders {
    public static class Game {
        
        static Player player;
        static float totalTime;

        public const int distToSide = 20;


        static Game () {
            Window window = new Window(800, 600, "SquareInvaders", PixelFormat.RGB);
            GfxTools.Init(window);

            Vector2 playerPosition = new Vector2(window.Width / 2, window.Height - 20);
            player = new Player(playerPosition, 52, 32, new Color(22, 22, 200), distToSide);
        }


        public static Player GetPlayer () {
            return player;
        }

        public static void Play () {
            while (GfxTools.Win.IsOpened) {
                totalTime += GfxTools.Win.DeltaTime;
                GfxTools.Clear();

                //Input
                if (GfxTools.Win.GetKey(KeyCode.Esc)) {
                    break;
                }
                player.Input();

                //Update
                player.Update();

                //Draw
                player.Draw();


                GfxTools.Win.Blit();
            }
        }
    }
}
