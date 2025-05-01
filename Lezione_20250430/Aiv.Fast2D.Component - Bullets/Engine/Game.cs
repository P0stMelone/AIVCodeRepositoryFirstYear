namespace Aiv.Fast2D.Component {
    public static class Game {

        public static float Gravity {
            get;
            private set;
        }


        public static float WorkingHeight { get; private set; }
        public static float UnitSize { get; private set; }

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

        public static void Init (int windowWith, int windowHeight, string windowName, Scene startScene,
            float workingHeight, float ortographicSize) {
            Gravity = 9.8f;
            Win = new Window(windowWith, windowHeight, windowName);
            Win.SetVSync(false);
            Win.SetDefaultViewportOrthographicSize(ortographicSize);
            WorkingHeight = workingHeight;
            UnitSize = WorkingHeight /ortographicSize;
            ChangeScene(startScene);
        }

        public static float PixelsToUnit (float pixelSize) {
            return pixelSize / UnitSize;
        }

        public static float UnitToPixels(float unitSize) {
            return unitSize * UnitSize;
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
