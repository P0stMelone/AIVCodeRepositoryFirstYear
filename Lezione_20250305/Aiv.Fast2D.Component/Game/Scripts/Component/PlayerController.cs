using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayerController : UserComponent {


        private BulletType bulletType;
        public BulletType BulletType {
            get { return bulletType; }
            set { bulletType = value; }
        }
        private float reloadTime;
        private float currentReloadTime;
        private KeyCode shootKey;


        private KeyCode upKey, downKey, leftKey, rightKey;
        private float speed;

        private Rigidbody rb;
        private ShootModule sm;
        private SpriteRenderer sr;
        private Collider collider;

        public PlayerController(GameObject gameObject, float speed, KeyCode upKey, KeyCode downKey, 
            KeyCode leftKey, KeyCode rightKey, float reloadTime, KeyCode shootKey) : base(gameObject) {
            this.speed = speed;
            this.upKey = upKey;
            this.downKey = downKey;
            this.leftKey = leftKey;
            this.rightKey = rightKey;
            this.shootKey = shootKey;
            this.reloadTime = reloadTime;
            currentReloadTime = reloadTime;
            bulletType = BulletType.BlueLaser;
        }

        public override void Start() {
            rb = GetComponent(typeof(Rigidbody)) as Rigidbody;
            sm = GetComponent(typeof(ShootModule)) as ShootModule;
            sr = GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            collider = GetComponent(typeof(Collider)) as Collider;
        }


        public override void Update () {
            HandleMovement();
            HandleShoot();
        }

        private void HandleMovement () {
            Vector2 input = Vector2.Zero;
            if (Game.Win.GetKey(upKey)) {
                input.Y -= 1;
            } else if (Game.Win.GetKey(downKey)) {
                input.Y += 1;
            }
            if (Game.Win.GetKey(leftKey)) {
                input.X -= 1;
            } else if (Game.Win.GetKey(rightKey)) {
                input.X += 1;
            }
            if (input != Vector2.Zero) input.Normalize();
            rb.Velocity = input * speed;
        }

        private void HandleShoot () {
            currentReloadTime -= Game.DeltaTime;
            if (currentReloadTime > 0) return;
            if (!Game.Win.GetKey(shootKey)) return;
            if (!sm.Shoot(bulletType, transform.Position + Vector2.UnitX * sr.Width / 2, gameObject)) return;
            currentReloadTime = reloadTime;
        }

    }
}
