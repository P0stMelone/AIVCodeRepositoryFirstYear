using OpenTK;

namespace Aiv.Fast2D.Component {
    public abstract class PowerUp : UserComponent {

        protected GameObject attachedPlayer;

        protected SpriteRenderer sr;
        protected Rigidbody rb;

        public PowerUp (GameObject owner) : base (owner) {

        }

        public override void Start() {
            gameObject.IsActive = false;
            rb = GetComponent(typeof(Rigidbody)) as Rigidbody;
            sr = GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        }

        public override void Update () {
            if (attachedPlayer != null) return;
            if (sr.BottomRight.X < 0) {
                DestroyPowerUp();
            }
        }

        public virtual void OnAttach (GameObject p) {
            attachedPlayer = p;
            sr.Enabled = false;
        }

        public virtual void OnDetach () {
            attachedPlayer = null;
            DestroyPowerUp();
        }

        protected void DestroyPowerUp() {
            gameObject.IsActive = false;
        }

        public virtual void Spawn () {
            Vector2 pos = new Vector2();
            pos.X = Game.Win.Width + sr.Width / 2;
            pos.Y = RandomGenerator.GetRandomInt((int)sr.Height, (int)(Game.Win.Height - sr.Height));
            transform.Position = pos;
            rb.Velocity = new Vector2(-300, 0);
            gameObject.IsActive = true;
            sr.Enabled = true;
        }

        public override void OnCollide(GameObject other) {
            OnAttach(other);
        }
    }
}
