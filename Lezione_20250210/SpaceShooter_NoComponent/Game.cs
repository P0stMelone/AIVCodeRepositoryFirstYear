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

        private static Player player;
        private static Background background;
        private static EnemyMgr enemyMgr;

        public static EnemyMgr EnemyMgr {
            get { return enemyMgr; }
        }


        public static void Init () {
            win = new Window(1280, 720, "Space Shooter");
            win.SetVSync(false);
            GfxMgr.AddTexture("player", "Assets/player_ship.png");
            GfxMgr.AddTexture("enemy_0", "Assets/enemy_ship.png");
            GfxMgr.AddTexture("enemy_1", "Assets/redEnemy_ship.png");
            GfxMgr.AddTexture("fireglobe", "Assets/fireGlobe.png");
            GfxMgr.AddTexture("greenglobe", "Assets/greenGlobe.png");
            GfxMgr.AddTexture("bluelaser", "Assets/blueLaser.png");
            GfxMgr.AddTexture("healthBar", "Assets/loadingBar_bar.png");
            GfxMgr.AddTexture("healthBackground", "Assets/loadingBar_frame.png");
            background = new Background("Assets/spaceBg.png");
            player = new Player(new Vector2(win.Width / 2, win.Height / 2), "player", 10);
            BulletMgr.Init();
            enemyMgr = new EnemyMgr();
        }

        public static void Play () {

            while (win.IsOpened && player.IsActive) {


                if (win.GetKey(KeyCode.Esc)) {
                    break;
                }

                //fintissimo motore fisico con il suo loop
                PhysicsMgr.FixedUpdate();
                PhysicsMgr.CheckCollisions();

                UpdateMgr.Update();
                UpdateMgr.LateUpdate();

                DrawMgr.Draw();

                Win.Update(); //swappa il back buffer con il front buffer
            }

        }

    }

}
