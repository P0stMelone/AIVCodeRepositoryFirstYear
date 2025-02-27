using Aiv.Fast2D;
using OpenTK;
using System;


namespace SpaceShooter_NoComponent {
    public abstract class Actor : GameObject {

        private int maxHp;
        protected int hp;

        protected ProgressBar healthBar;


        protected virtual  int Energy {
            get { return hp; }
            set {
                hp = Math.Min(value, maxHp);
                healthBar.Scale(hp / (float)maxHp);
            }
        }

        public override bool IsActive {
            get { return hp > 0; }
        }

        protected float reloadTime = 0.33f;
        protected float currentReloadTime = 0;


        public Actor (Vector2 position, string texturePath, int maxHp) : base (texturePath) {
            speed = 250;
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            Position = position;
            this.maxHp = maxHp;
            rigidbody.Collider = ColliderFactory.CreateBoxFor(this);
            healthBar = new ProgressBar("healthBackground", "healthBar", new Vector2(4, 4));
        }

        public virtual void TakeDamge(int damage) {
            Energy -= damage;
        }

        public virtual void Reset () {
            Energy = maxHp;
        }

        public override void LateUpdate() {
            healthBar.Position = Position + new Vector2(-Width / 2, -Height - healthBar.Height);
        }

    }
}
