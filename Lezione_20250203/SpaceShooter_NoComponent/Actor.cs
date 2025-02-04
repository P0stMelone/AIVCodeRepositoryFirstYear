using Aiv.Fast2D;
using OpenTK;


namespace SpaceShooter_NoComponent {
    public abstract class Actor : GameObject {

        private int maxHp;
        protected int hp;

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
        }

        public virtual void TakeDamge(int damage) {
            hp -= damage;
        }

        public virtual void Reset () {
            hp = maxHp;
        }

    }
}
