using System.Collections.Generic;
using OpenTK;
using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    public class EnemyMgr : IUpdatable {

        const int poolSize = 20;

        Queue<Enemy> enemies;
        //static Enemy[] enemiesArray;
        RandomTimer spawnTimer;

        public bool IsActive {
            get { return true; }
        }

        public EnemyMgr() {
            enemies = new Queue<Enemy>(poolSize);
            //enemiesArray = new Enemy[poolSize];
            for (int i = 0; i < poolSize; i++) {
                Enemy b;
                if (RandomGenerator.GetRandomInt(0,1) == 0) {
                    b = new Enemy_0();
                } else {
                    b = new Enemy_1();
                }
                enemies.Enqueue(b);
                //enemiesArray[i] = b;
            }
            spawnTimer = new RandomTimer(2, 5);
            UpdateMgr.AddItem(this);
        }

        private Enemy GetEnemy() {
            if (enemies.Count == 0) return null;
            Enemy b = enemies.Dequeue();
            return b;
        }

        public void RestoreEnemy(Enemy b) {
            enemies.Enqueue(b);
        }

        public void Update() {
            ManageEnemySpawn();
            //ManageEnemyUpdate();

        }

        private void ManageEnemySpawn () {
            spawnTimer.Tick();
            if (!spawnTimer.IsOver()) return;
            Enemy e = GetEnemy();
            if (e == null) return; //non deve succedere se è un elemento di gameplay
            e.SpawnMe(new Vector2(Game.Win.Width + e.Width / 2,
                RandomGenerator.GetRandomInt(e.Height, Game.Win.Height - e.Height)));
            spawnTimer.Reset();
        }

        public void LateUpdate() {
        }


        //private static void ManageEnemyUpdate () {
        //    foreach (Enemy b in enemiesArray) {
        //        if (!b.IsActive) continue;
        //        b.Update();
        //    }
        //}

        //public static void Draw() {
        //    foreach (Enemy b in enemiesArray) {
        //        if (!b.IsActive) continue;
        //        b.Draw();
        //    }
        //}


    }
}
