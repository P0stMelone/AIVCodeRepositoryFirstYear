using OpenTK;

namespace Aiv.Fast2D.Component {
    public class EnemyMgr : UserComponent {

        private Enemy[,] enemiesPool;

        private float timer;

        public EnemyMgr(GameObject owner, int poolSize) : base(owner) {
            enemiesPool = new Enemy[(int)EnemyList.last, poolSize];
            for (int i = 0; i < enemiesPool.GetLength(0); i++) {
                for (int j = 0; j < enemiesPool.GetLength(1); j++) {
                    switch((EnemyList) i) {
                        case EnemyList.white:
                            enemiesPool[i, j] = CreateWhiteEnemy(j);
                            break;
                        case EnemyList.red:
                            enemiesPool[i, j] = CreateRedEnemy(j);
                            break;
                    }
                }
            }
        }

        public override void Update() {
            timer += Game.Win.DeltaTime;
            if (timer > 3) {
                timer = 0;
                Vector2 position = new Vector2(Game.Win.OrthoWidth, 0);
                Enemy freeEnemy = GetEnemy(EnemyList.red);
                freeEnemy.transform.Position = position;
                freeEnemy.gameObject.IsActive = true;
            }
        }

        private Enemy GetEnemy (EnemyList enemy) {
            for (int i = 0; i < enemiesPool.GetLength(1); i++) {
                if (enemiesPool[(int)enemy, i].gameObject.IsActive) continue;
                return enemiesPool[(int)enemy, i];
            }
            return null;
        }

        private Enemy CreateWhiteEnemy(int index) {
            GameObject player = GameObject.Find("Player");
            GameObject enemy = GameObject.CreateGameObject("WhiteEnemy_" + index,
                new Vector2(Game.Win.OrthoWidth * 0.75f, Game.Win.OrthoHeight * 0f));
            enemy.Tag = "Enemy";
            enemy.Layer = (uint)Layer.Enemy;
            enemy.AddCollisionLayer((uint)Layer.Tile);
            enemy.AddCollisionLayer((uint)Layer.Enemy);
            enemy.AddComponent(new SpriteRenderer(enemy, "Player", new Vector2(0.5f, 1f), DrawLayer.Playground,
                58, 58, Vector2.Zero));
            enemy.AddComponent(ColliderFactory.CreateBoxFor(enemy));
            enemy.AddComponent<Rigidbody>().IsGravityAffected = true;
            enemy.AddComponent(new BlockDetector(enemy, new string[] { "Tile" }, new string[] { "Enemy" }));
            FSMComponent fsmComponent = enemy.AddComponent<FSMComponent>();
            fsmComponent.Init(FSMTemplates.CreateWhiteEnemyStateMachine(enemy, player));
            return enemy.AddComponent<Enemy>();
        }

        private Enemy CreateRedEnemy(int index) {
            GameObject player = GameObject.Find("Player");
            GameObject enemy = GameObject.CreateGameObject("RedEnemy_" + index,
                new Vector2(Game.Win.OrthoWidth * 0.75f, Game.Win.OrthoHeight * 0f));
            enemy.Tag = "Enemy";
            enemy.Layer = (uint)Layer.Enemy;
            enemy.AddCollisionLayer((uint)Layer.Tile);
            enemy.AddCollisionLayer((uint)Layer.Enemy);
            enemy.AddComponent(new SpriteRenderer(enemy, "Player", new Vector2(0.5f, 1f), DrawLayer.Playground,
                58, 58, Vector2.Zero));
            enemy.GetComponent<SpriteRenderer>().Sprite.SetMultiplyTint(1f, 0, 0, 1);
            enemy.AddComponent(ColliderFactory.CreateBoxFor(enemy));
            enemy.AddComponent<Rigidbody>().IsGravityAffected = true;
            enemy.AddComponent(new BlockDetector(enemy, new string[] { "Tile" }, new string[] { "Enemy" }));
            enemy.AddComponent(new ShootModule(enemy, BulletType.RedGlobe));
            FSMComponent fsmComponent = enemy.AddComponent<FSMComponent>();
            fsmComponent.Init(FSMTemplates.CreateRedEnemyStateMachine(enemy, player, 3, 4, 3, 6));
            return enemy.AddComponent<Enemy>();
        }

    }
}
