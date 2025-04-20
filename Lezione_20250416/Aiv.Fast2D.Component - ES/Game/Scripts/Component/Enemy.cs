

namespace Aiv.Fast2D.Component {
    public class Enemy : UserComponent {

        private Player playerRef;

        public Enemy (GameObject owner) : base (owner) {

        }

        public override void Awake() {
            playerRef = GameObject.Find("Player").GetComponent<Player>();
        }

        public override void Update() {
            if (Input.GetKeyDown(KeyCode.A)) {
                playerRef.TakeDamage(100, gameObject);
            }
        }

    }
}
