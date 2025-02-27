namespace Aiv.Fast2D.Component {
    public static class Game {

        public static Window Win;
        public static bool IsRunning;
        public static float DeltaTime {
            get {
                return Win.DeltaTime;
            }
        }

        private static Scene currentScene;

        public static void Init (int windowWith, int windowHeight, string windowName, Scene startScene) {
            Win = new Window(windowWith, windowHeight, windowName);
            Win.SetVSync(false);

            currentScene = startScene;
            currentScene.InitializeScene();
        }

        public static void Play () {
            IsRunning = true;
            currentScene.Start();
            while (Win.IsOpened && IsRunning) {
                currentScene.Update();
                currentScene.LateUpdate();
                DrawMgr.Draw();

                Win.Update();
            }
        }

    }
}
