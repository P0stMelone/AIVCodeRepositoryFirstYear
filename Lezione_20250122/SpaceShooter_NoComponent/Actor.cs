using Aiv.Fast2D;
using OpenTK;


namespace SpaceShooter_NoComponent {
    public abstract class Actor {

        private int maxHp;
        protected int hp;

        public bool IsAlive {
            get { return hp > 0; }
        }

        protected Sprite sprite;
        private Texture texture;

        protected float speed = 250;
        protected Vector2 velocity;

        protected float reloadTime = 0.33f;
        protected float currentReloadTime = 0;

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

        public Actor (Vector2 position, string texturePath, int maxHp) {
            texture = new Texture(texturePath);
            sprite = new Sprite(texture.Width, texture.Height);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            Position = position;
            this.maxHp = maxHp;
        }

        private void Move() {
            Position += velocity * Game.Win.DeltaTime;

        }

        public virtual void Update() {
            Move();
        }

        public void Draw() {
            sprite.DrawTexture(texture);
        }

        public virtual void TakeDamge(int damage) {
            hp -= damage;
        }

        public virtual void Reset () {
            hp = maxHp;
        }

    }
}
