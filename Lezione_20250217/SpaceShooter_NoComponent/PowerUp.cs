using OpenTK;

namespace SpaceShooter_NoComponent {
    public abstract class PowerUp : GameObject {

        protected Player attachedPlayer;

        private bool _interalIsActive;

        public override bool IsActive {
            get {
                return _interalIsActive;
            }
            set {
                _interalIsActive = value;
            }
        }

        public PowerUp(string textureName) : base(textureName) {

            rigidbody.Type = RigidbodyType.PowerUp;
            rigidbody.AddCollisionType(RigidbodyType.Player);


            rigidbody.Collider = ColliderFactory.CreateCircleFor(this);
            rigidbody.Velocity = new Vector2(-200, 0);
            IsActive = false;
        }

        public virtual void OnAttach(Player p) {
            attachedPlayer = p;
        }

        public virtual void OnDeatch () {
            attachedPlayer = null;
            Restore();
        }

        public override void Update() {
            if (attachedPlayer != null) return;
            if (Position.X + Width *0.5f < 0) {
                Restore();
            }
        }

        public override void Draw() {
            if (attachedPlayer != null) return;
            base.Draw();
        }


        public override void OnCollide(GameObject other) {
            if (other is Player) {
                OnAttach((Player)other);
            }
        }

        public virtual void Spawn () {
            Vector2 pos = new Vector2();
            pos.X = Game.Win.Width + Width * 0.5f;
            pos.Y = RandomGenerator.GetRandomInt(Height, Game.Win.Height - Height);
            Position = pos;
            IsActive = true;
        }

        protected virtual void Restore () {
            IsActive = false;
            ((PlayScene)(Game.CurrentScene)).PowerUpMgr.Restore(this);
        }
    }
}
