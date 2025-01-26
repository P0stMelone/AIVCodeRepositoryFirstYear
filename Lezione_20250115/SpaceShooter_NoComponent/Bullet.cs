using OpenTK;
using Aiv.Fast2D;


namespace SpaceShooter_NoComponent {
    public class Bullet {


        private Sprite sprite;
        private Texture texture;

        private int damage;
        private float speed;
        private Vector2 velocity;


        public Vector2 Position {
            get {
                return sprite.position;
            }
            set {
                sprite.position = value;
            }
        }
        public bool IsActive {
            get;
            private set;
        }

        public Bullet (Texture texture, int damage, float speed) {
            this.texture = texture;
            sprite = new Sprite(texture.Width, texture.Height);
            this.damage = damage;
            this.speed = speed;
            sprite.pivot = new Vector2(0, sprite.Height / 2);
        }

        public void Shoot (Vector2 startPosition, Vector2 direction) {
            velocity = direction.Normalized() * speed;
            Position = startPosition;
            IsActive = true;
        }

        public void Update () {
            Position += velocity * Game.Win.DeltaTime;
            if (Position.X > Game.Win.Width) {
                DespawnMe();
            }
        }

        public void Draw () {
            sprite.DrawTexture(texture);
        }

        public void DespawnMe () {
            IsActive = false;
            BulletMgr.RestoreBullet(this);
        }
    }
}
