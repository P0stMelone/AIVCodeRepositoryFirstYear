namespace Aiv.Fast2D.Component {
    public class BlockDetector : CollisionDetector {

        private string[] movingElementsTag;
        private Transform playerPos;

        public BlockDetector(GameObject owner, string[] groundsTag, string[] movingElements) : base(owner, groundsTag) {
            this.movingElementsTag = movingElements;
        }

        public override void Awake() {
            base.Awake();
            playerPos = GameObject.Find("Player").transform;
        }

        public override void OnCollide(Collision collisionInfo) {
            base.OnCollide(collisionInfo);
            if (!IsMovingElementsTag(collisionInfo.Collider.gameObject.Tag)) { return; }
            if (IsHorizontalCollision(collisionInfo)) {
                HandleHorizontalCollision(collisionInfo);
            } else {
                HandleVerticalCollision(collisionInfo);
            }
        }

        private void HandleHorizontalCollision (Collision collisionInfo) {
            if (playerPos.Position.X < gameObject.transform.Position.X
                && gameObject.transform.Position.X > collisionInfo.Collider.Position.X) {
                HandleGroundCollision(collisionInfo);
            }
            if (playerPos.Position.X > gameObject.transform.Position.X
                && gameObject.transform.Position.X < collisionInfo.Collider.Position.X) {
                HandleGroundCollision(collisionInfo);
            }
        }

        private void HandleVerticalCollision (Collision collisionInfo) {
            if (transform.Position.Y < collisionInfo.Collider.transform.Position.Y) {
                HandleGroundCollision(collisionInfo);
            }
        }


        private bool IsMovingElementsTag(string tag) {
            foreach (string elem in movingElementsTag) {
                if (elem == tag) return true;
            }
            return false;
        }

    }
}
