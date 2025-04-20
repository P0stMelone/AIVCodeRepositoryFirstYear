namespace Aiv.Fast2D.Component {
    public static class Game {

        public const float Gravity = 400f;

        public static Window Win;
        public static bool IsRunning;
        public static float DeltaTime {
            get {
                if (firstFrame) {
                    return 0;
                }
                return Win.DeltaTime;
            }
        }

        private static bool firstFrame;
        private static bool triggerChangeScene;
        private static Scene newScene;

        private static Scene currentScene;
        public static Scene CurrenScene {
            get { return currentScene; }
        }

        public static void Init (int windowWith, int windowHeight, string windowName, Scene startScene) {
            Win = new Window(windowWith, windowHeight, windowName);
            Win.SetVSync(false);
            ChangeScene(startScene);
        }

        public static void Play () {
            IsRunning = true;
            while (Win.IsOpened && IsRunning) {

                //inizia il mio gameloop
                PhysicsMgr.FixedUpdate();
                PhysicsMgr.CheckCollisions();

                currentScene.Update();
                currentScene.LateUpdate();
                DrawMgr.Draw();
                //finisce il mio game loop
                firstFrame = false;
                //essendo finito il mio gameloop e posso salvare il valore attuale dei 
                //key perché nel prossimo frame sarà il valore del frame precedente
                Input.PerformLastKey();
                Win.Update();
                if (triggerChangeScene) {
                    triggerChangeScene = false;
                    ChangeScene(newScene);
                }
            }
        }

        private static void ChangeScene (Scene newScene) {
            if (currentScene != null) {
                currentScene.DestroyScene();
            }
            if (newScene == null) {
                IsRunning = false;
                return;
            }
            currentScene = newScene;
            firstFrame = true;
            currentScene.InitializeScene();
            currentScene.Awake();
            currentScene.Start();
            currentScene.IsInitialized = true;
        }

        public static void TriggerChangeScene (Scene newScene) {
            triggerChangeScene = true;
            Game.newScene = newScene;
        }
    }
}
