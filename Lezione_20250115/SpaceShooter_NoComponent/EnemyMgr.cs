using System.Collections.Generic;
using OpenTK;
using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    public static class EnemyMgr {

        const int poolSize = 20;

        static Queue<Enemy> enemies;
        static Enemy[] enemiesArray;
        static RandomTimer spawnTimer;


        public static void Init() {
            enemies = new Queue<Enemy>(poolSize);
            enemiesArray = new Enemy[poolSize];
            for (int i = 0; i < poolSize; i++) {
                Enemy b = new Enemy(Vector2.Zero,"Assets/Enemy_Ship.png", 10);
                enemies.Enqueue(b);
                enemiesArray[i] = b;
            }
            spawnTimer = new RandomTimer(2, 5);
        }

        private static Enemy GetEnemy() {
            if (enemies.Count == 0) return null;
            Enemy b = enemies.Dequeue();
            return b;
        }

        public static void RestoreEnemy(Enemy b) {
            enemies.Enqueue(b);
        }

        public static void Update() {
            ManageEnemySpawn();
            ManageEnemyUpdate();

        }

        private static void ManageEnemySpawn () {
            spawnTimer.Tick();
            if (!spawnTimer.IsOver()) return;
            Enemy e = GetEnemy();
            if (e == null) return; //non deve succedere se è un elemento di gameplay
            e.SpawnMe(new Vector2(Game.Win.Width + e.Width / 2,
                RandomGenerator.GetRandomInt(e.Height, Game.Win.Height - e.Height)));
            spawnTimer.Reset();
        }


        private static void ManageEnemyUpdate () {
            foreach (Enemy b in enemiesArray) {
                if (!b.IsAlive) continue;
                b.Update();
            }
        }

        public static void Draw() {
            foreach (Enemy b in enemiesArray) {
                if (!b.IsAlive) continue;
                b.Draw();
            }
        }


    }
}
