using OpenTK;

namespace Aiv.Fast2D.Component {
    public class Rigidbody : Component {


        private Vector2 velocity;
        public Vector2 Velocity {
            get { return velocity; }
            set { velocity = value; }
        }

        public bool IsGravityAffected;

        public Rigidbody(GameObject owner) : base (owner) {
            PhysicsMgr.AddRB(this);
        }

        public virtual void FixedUpdate () {
            if (IsGravityAffected) {
                velocity.Y += Game.Gravity * Game.DeltaTime;
            }
            transform.Position += Velocity * Game.DeltaTime;
        }

    }
}
