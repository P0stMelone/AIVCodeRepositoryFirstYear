
namespace Aiv.Fast2D.Component {
    public class LinkController : UserComponent {

        private SheetAnimator animator;

        public LinkController (GameObject owner) : base (owner) {

        }

        public override void Awake() {
            animator = GetComponent<SheetAnimator>();
        }

        public override void Update() {
            if (animator.GetCurrentAnimationName() == "Die") return;
            if (Input.GetKeyDown(KeyCode.A)) {
                animator.ChangeClip("Attack");
            } else if (Input.GetKeyDown(KeyCode.D)) {
                animator.ChangeClip("Die");
            }
        }

    }
}
