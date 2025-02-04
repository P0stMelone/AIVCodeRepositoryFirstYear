using OpenTK;

namespace SpaceShooter_NoComponent {
    public class Rigidbody {

        private GameObject gameObject;

        public Vector2 Velocity;
        public bool IsGravityAffected;

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

        public virtual void FixedUpdate () {
            if (IsGravityAffected) {
                Velocity += Game.gravity * Game.Win.DeltaTime;
            }
            Position += Velocity * Game.Win.DeltaTime;
        }

    }
}
