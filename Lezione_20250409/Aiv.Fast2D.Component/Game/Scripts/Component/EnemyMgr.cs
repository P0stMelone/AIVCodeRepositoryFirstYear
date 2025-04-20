using OpenTK;

namespace Aiv.Fast2D.Component {
    public class EnemyMgr : UserComponent {

        private EnemyComponent[,] enemiesPool;

        private RandomTimer randomTimer;

        public EnemyMgr(GameObject owner, int poolSize, int timeMin, int timeMax): base(owner) {
            randomTimer = new RandomTimer(timeMin, timeMax);
            randomTimer.Reset();
            enemiesPool = new EnemyComponent[(int)EnemyType.last, poolSize];
            CreateEnemies();
        }

        private void CreateEnemies () {
            for (int i = 0; i < enemiesPool.GetLength(0); i++) {
                for (int j = 0; j< enemiesPool.GetLength(1); j++) {
                    switch((EnemyType) i) {
                        case EnemyType.enemy_0:
                            enemiesPool[i, j] = CreateEnemy_0(j);
                            break;
                        case EnemyType.enemy_1:
                            enemiesPool[i, j] = CreateEnemy_1(j);
                            break;
                        case EnemyType.enemy_bottom:
                            enemiesPool[i, j] = CreateEnemy_Bottom(j);
                            break;
                    }
                }
            }
        }

        private EnemyComponent CreateEnemy_0 (int index) {
            GameObject enemy = GameObject.CreateGameObject("Enemy_0_" + index, Vector2.Zero);
            enemy.AddComponent(new SpriteRenderer(enemy, "enemy_0", Vector2.One * 0.5f, DrawLayer.Playground));
            enemy.AddComponent(typeof(Rigidbody));
            enemy.AddComponent(ColliderFactory.CreateBoxFor(enemy));
            enemy.AddComponent(typeof(ShootModule));
            enemy.Layer = (uint)Layer.Enemy;
            enemy.AddCollisionLayer((uint)Layer.Player);
            EnemyComponent enemyComponent = new EnemyComponent(enemy, EnemyType.enemy_0, 20, 
                BulletType.IceGlobe, 50);
            enemy.AddComponent(enemyComponent);
            return enemyComponent;
        }

        private EnemyComponent CreateEnemy_1 (int index) {
            GameObject enemy = GameObject.CreateGameObject("Enemy_1_" + index, Vector2.Zero);
            enemy.Layer = (uint)Layer.Enemy;
            enemy.AddCollisionLayer((uint)Layer.Player);
            enemy.AddComponent(new SpriteRenderer(enemy, "enemy_1", Vector2.One * 0.5f, DrawLayer.Playground));
            enemy.AddComponent(typeof(Rigidbody));
            enemy.AddComponent(ColliderFactory.CreateBoxFor(enemy));
            enemy.AddComponent(typeof(ShootModule));
            EnemyComponent enemyComponent = new EnemyComponent(enemy, EnemyType.enemy_1, 50, 
                BulletType.FireGlobe, 75);
            enemy.AddComponent(enemyComponent);
            return enemyComponent;
        }

        private EnemyComponent CreateEnemy_Bottom(int index) {
            GameObject enemy = GameObject.CreateGameObject("Enemy_2_" + index, Vector2.Zero);
            enemy.Layer = (uint)Layer.Enemy;
            enemy.AddCollisionLayer((uint)Layer.Player);
            enemy.AddComponent(new SpriteRenderer(enemy, "enemy_2", Vector2.One * 0.5f, DrawLayer.Playground));
            enemy.AddComponent(typeof(Rigidbody));
            enemy.AddComponent(ColliderFactory.CreateBoxFor(enemy));
            EnemyComponent enemyComponent = new EnemyComponent(enemy, EnemyType.enemy_bottom, 250,
                BulletType.FireGlobe, 5);
            enemy.AddComponent(enemyComponent);
            return enemyComponent;
        }


        public override void Update() {
            randomTimer.Tick();
            if (!randomTimer.IsOver()) return;
            EnemyType typeToSpawn = (EnemyType)RandomGenerator.GetRandomInt(0, (int)EnemyType.last);
            EnemyComponent enemyToSpawn = GetEnemyFromPool(typeToSpawn);
            if (enemyToSpawn == null) return;
            //Spawna nemico dal basso
            if (typeToSpawn == EnemyType.enemy_bottom) {
                GameObject player = FindPlayer();
                if ( player == null) return;
                enemyToSpawn.SpawnMe(new Vector2(player.transform.Position.X, Game.Win.Height + enemyToSpawn.Height / 2),
                    Vector2.UnitY * -500);
                randomTimer.Reset();
                return;
            }
            enemyToSpawn.SpawnMe(new Vector2(Game.Win.Width + enemyToSpawn.Width / 2,
                RandomGenerator.GetRandomFloat(enemyToSpawn.Height / 2, Game.Win.Height - enemyToSpawn.Height / 2)), 
                Vector2.UnitX * -200);            
            randomTimer.Reset();
        }

        private GameObject FindPlayer() {
            GameObject player;
            if (!GameData.IsMultiplayer) {
            player = GameObject.Find("Player1");
            }
            else {
                player = GameObject.Find("Player"+RandomGenerator.GetRandomInt(1, 2));
            }
            return player;
        }

        private EnemyComponent GetEnemyFromPool (EnemyType type) {
            for (int i = 0; i < enemiesPool.GetLength(1); i++) {
                if (enemiesPool[(int)type, i].gameObject.IsActive) continue;
                return enemiesPool[(int)type, i];
            }
            return null;
        }
    }
}
