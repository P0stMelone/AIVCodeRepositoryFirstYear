using Aiv.Fast2D;
using OpenTK;

namespace SpaceShooter_NoComponent {
    public class Enemy {

        private int maxHp;
        private int hp;

        public bool IsAlive {
            get { return hp > 0; }
        }

        private Sprite sprite;
        private Texture texture;

        private float speed = 250;
        private Vector2 velocity;

        private float reloadTime = 0.33f;
        private float currentReloadTime = 0;

        public Vector2 Position {
            get { return sprite.position; }
            set { sprite.position = value; }
        }
        public int Width {
            get { return (int)sprite.Width; }
        }
        public int Height {
            get { return (int)sprite.Height; }
        }

        public Enemy(Vector2 position, string texturePath, int maxHp) {
            texture = new Texture(texturePath);
            sprite = new Sprite(texture.Width, texture.Height);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            sprite.FlipX = true;
            velocity = -Vector2.UnitX * speed;
            Position = position;
            this.maxHp = maxHp;
            hp = 0;
        }

        public void SpawnMe (Vector2 startPosition) {
            this.Position = startPosition;
            hp = maxHp;
        }

        public void Update() {
            Move();
            CheckBorder();
        }

        private void Move() {
            Position += velocity * Game.Win.DeltaTime;
        }

        private void CheckBorder () {
            if (Position.X < -Width / 2) {
                Die();
            }
        }

        public void Draw() {
            sprite.DrawTexture(texture);
        }

        public void TakeDamge(int damage) {
            hp -= damage;
        }

        private void Die () {
            EnemyMgr.RestoreEnemy(this);
        }

    }
}
