using Aiv.Draw;
using System;

namespace SquareInvaders {
    public static class Game {

        public const int gravity = 300;

        static Player player;
        static float totalTime;
        static EnemyMgr enemyMgr;
        static BulletPooler pooler;

        public const int distToSide = 10;


        static Game () {
            Window window = new Window(800, 600, "SquareInvaders", PixelFormat.RGB);
            GfxTools.Init(window);

            pooler = new BulletPooler(30, 30);

            Vector2 playerPosition = new Vector2(window.Width / 2, window.Height - 20);
            player = new Player(playerPosition, 52, 32, new Color(22, 22, 200), pooler,3);
            enemyMgr = new EnemyMgr(24, 3, pooler);
        }


        public static Player GetPlayer () {
            return player;
        }

        public static void Play () {
            while (GfxTools.Win.IsOpened && player.IsAlive() && !enemyMgr.AllDead()) {
                totalTime += GfxTools.Win.DeltaTime;
                GfxTools.Clear();

                //Input
                if (GfxTools.Win.GetKey(KeyCode.Esc)) {
                    break;
                }
                player.Input();

                //Update
                player.Update();
                enemyMgr.Update();
                pooler.Update();

                //Physics
                pooler.DetectCollision(player, enemyMgr.Aliens);

                //Draw
                player.Draw();
                enemyMgr.Draw();
                pooler.Draw();
                
                GfxTools.Win.Blit();
            }
        }
    }
}
