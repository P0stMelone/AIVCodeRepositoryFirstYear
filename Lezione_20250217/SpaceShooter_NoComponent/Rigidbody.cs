using OpenTK;

namespace SpaceShooter_NoComponent {
    public class Rigidbody {

        public RigidbodyType Type;
        private uint collisionMask;
        public bool IsCollisionAffected;

        private GameObject gameObject;
        public GameObject GameObject {
            get { return gameObject; }
        }

        public Vector2 Velocity;
        public bool IsGravityAffected;

        public Collider Collider { get; set; }

        public Vector2 Position {
            get { return gameObject.Position; }
            set { gameObject.Position = value; }
        }
        public bool IsActive {
            get { return gameObject.IsActive; }
        }

        public Rigidbody (GameObject owner) {
            gameObject = owner;
            PhysicsMgr.AddItem(this);
        }

        public void AddCollisionType (RigidbodyType type) {
            collisionMask |= (uint)type;
        }

        public void RemoveCollisionType (RigidbodyType type) {
            collisionMask &= ~(uint)type;
        }

        public bool CollisionTypeMatches (RigidbodyType type) {
            return ((uint)type & collisionMask) != 0;
        }

        public virtual void FixedUpdate () {
            if (IsGravityAffected) {
                Velocity += Game.gravity * Game.Win.DeltaTime;
            }
            Position += Velocity * Game.Win.DeltaTime;
        }

        public virtual bool Collides (Rigidbody other) {
            return Collider.Collides(other.Collider);
        }

    }
}
