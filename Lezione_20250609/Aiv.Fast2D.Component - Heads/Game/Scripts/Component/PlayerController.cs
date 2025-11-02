using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayerController : UserComponent, IDamagable {

        private string horizontalAxis;
        private string verticalAxis;
        private string shootButtonName;
        private float speed;

        private Rigidbody rb;
        private ShootModule sm;

        public PlayerController (GameObject owner, string horizontalAxis, 
            string verticalAxis, float speed, string shootButtonName) : base (owner) {
            this.horizontalAxis = horizontalAxis;
            this.verticalAxis = verticalAxis;
            this.speed = speed;
            this.shootButtonName = shootButtonName;
        }

        public override void Awake() {
            rb = GetComponent<Rigidbody>();
            sm = GetComponent<ShootModule>();
        }

        public DamageFeedback TakeDamage(DamageContainer damage) {
            throw new System.NotImplementedException();
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
