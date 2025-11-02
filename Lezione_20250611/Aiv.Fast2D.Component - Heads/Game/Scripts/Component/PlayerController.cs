using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayerController : UserComponent, IDamagable {

        private string horizontalAxis;
        private string verticalAxis;
        private string shootButtonName;
        private float speed;

        private Rigidbody rb;
        private ShootModule sm;
        private HealthModule hm;

        public float HealthPercentage {
            get { return hm.CurrentEnergyPercentage; }
        }

        public PlayerController (GameObject owner, string horizontalAxis, 
            string verticalAxis, float speed, string shootButtonName, int hp) : base (owner) {
            this.horizontalAxis = horizontalAxis;
            this.verticalAxis = verticalAxis;
            this.speed = speed;
            this.shootButtonName = shootButtonName;
            hm = new HealthModule(hp);
        }

        public override void Awake() {
            rb = GetComponent<Rigidbody>();
            sm = GetComponent<ShootModule>();
        }

        public DamageFeedback TakeDamage(DamageContainer damage) {
            if(hm.TakeDamage((int)damage.damage)) {
                Die();
            }
            return DamageFeedback.DefaultFeedback;
        }

        private void Die () {
            gameObject.IsActive = false;
        }

        public override void Update() {
            Vector2 input = new Vector2(Input.GetAxis(horizontalAxis), 
                Input.GetAxis(verticalAxis));
            if (input != Vector2.Zero) {
                input.Normalize();
                rb.Velocity = input * speed;
            }
            if (Input.GetInputActionButtonDown(shootButtonName)) {
                sm.Shoot(BulletType.Player);
            }
        }

    }
}
