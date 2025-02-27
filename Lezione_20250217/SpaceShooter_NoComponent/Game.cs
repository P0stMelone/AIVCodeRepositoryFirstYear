using Aiv.Fast2D;
using OpenTK;

namespace SpaceShooter_NoComponent {
    public static class Game {


        public static Vector2 gravity = new Vector2(0, 400f);

        private static Window win;
        public static Window Win {
            get {
                return win;
            }
        }

        private static Scene currentScene;
        public static Scene CurrentScene {
            get { return currentScene; }
        }

        public static void Init () {
            win = new Window(1280, 720, "Space Shooter");
            win.SetVSync(false);
            currentScene = new TitleScene();

            currentScene.Start();
        }

        public static void Play () {

            while (win.IsOpened) {


                if (win.GetKey(KeyCode.Esc)) {
                    break;
                }

                currentScene.GameLoop();

                Win.Update(); //swappa il back buffer con il front buffer

                if (!currentScene.IsPlaying) {
                    if (!ChangeScene()) {
                        break;
                    }
                }
            }

        }

        private static bool ChangeScene () {
            Scene nextScene = currentScene.OnExit();
            if (nextScene == null) return false;
            currentScene = nextScene;
            nextScene.Start();
            return true;
        }

    }

}
