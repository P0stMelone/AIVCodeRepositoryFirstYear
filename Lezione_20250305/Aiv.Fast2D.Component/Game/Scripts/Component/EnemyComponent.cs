using OpenTK;

namespace Aiv.Fast2D.Component {

    public enum EnemyType { enemy_0, enemy_1, last}

    public class EnemyComponent : UserComponent {

        private EnemyType enemyType;
        private RandomTimer randomTimer;
        private BulletType bulletType;

        private Rigidbody rb;
        private SpriteRenderer sr;
        private ShootModule sm;

        private int score;
        public int Score {
            get { return score; }
        }
        public float Width {
            get { return sr.Width; }
        }
        public float Height {
            get { return sr.Height; }
        }


        public EnemyComponent (GameObject owner, EnemyType type, int score, BulletType bulletType) : base (owner) {
            enemyType = type;
            this.score = score;
            randomTimer = new RandomTimer(1, 3);
            this.bulletType = bulletType;
        }

        public override void Start() {
            rb = GetComponent(typeof(Rigidbody)) as Rigidbody;
            sr = GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            sr.Sprite.FlipX = true;
            sm = GetComponent(typeof(ShootModule)) as ShootModule;
            gameObject.IsActive = false;
        }

        public void SpawnMe (Vector2 startPosition, Vector2 velocity) {
            randomTimer.Reset();
            transform.Position = startPosition;
            rb.Velocity = velocity;
            gameObject.IsActive = true;
        }

        public override void Update() {
            if (sr.BottomRight.X < 0) DestroyMe();
            randomTimer.Tick();
            if (!randomTimer.IsOver()) return;
            if (!sm.Shoot(bulletType, transform.Position - Vector2.UnitX * sr.Width / 2, gameObject)) return;
            randomTimer.Reset();
        }


        private void DestroyMe () {
            gameObject.IsActive = false;
        }


    }
}
