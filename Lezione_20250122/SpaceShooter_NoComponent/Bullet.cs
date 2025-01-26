using OpenTK;
using Aiv.Fast2D;


namespace SpaceShooter_NoComponent {

    public enum BulletType {
        BlueLaser,
        FireGlobe,
        IceGlobe,
        GreenGlobe,
        Last
    }

    public abstract class Bullet {

        public BulletType Type { get; protected set; }

        protected Sprite sprite;
        private Texture texture;

        protected int damage;
        private float speed;
        protected Vector2 velocity;


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

        public int Width {
            get { return (int)sprite.Width; }
        }

        public Bullet (Texture texture, int damage, float speed, BulletType type) {
            Type = type;
            this.texture = texture;
            sprite = new Sprite(texture.Width, texture.Height);
            this.damage = damage;
            this.speed = speed;
            sprite.pivot = new Vector2(0, sprite.Height / 2);
        }

        public virtual void Shoot (Vector2 startPosition, Vector2 direction) {
            velocity = direction.Normalized() * speed;
            Position = startPosition;
            IsActive = true;
        }

        public void Update () {
            Move();
            if (!InsideBorder()) {
                DespawnMe();
            }
        }

        protected virtual void Move () {
            Position += velocity * Game.Win.DeltaTime;
        }

        protected abstract bool InsideBorder();

        public void Draw () {
            sprite.DrawTexture(texture);
        }

        public void DespawnMe () {
            IsActive = false;
            BulletMgr.RestoreBullet(this, Type);
        }
    }
}
