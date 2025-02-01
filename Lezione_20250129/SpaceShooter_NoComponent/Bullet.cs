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

    public abstract class Bullet : GameObject {

        public BulletType Type { get; protected set; }

        private int damage;

        private bool _isActive;
        public override bool IsActive {
            get {
                return _isActive;
            }
            protected set {
                _isActive = value;
            }
        }

        public Bullet (string texturePath, int damage, float speed, BulletType type) : base (texturePath) {
            Type = type;
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

        public override void Update () {
            base.Update();
            if (!InsideBorder()) {
                DespawnMe();
            }
        }

        protected abstract bool InsideBorder();
        

        public void DespawnMe () {
            IsActive = false;
            BulletMgr.RestoreBullet(this, Type);
        }
    }
}
