using OpenTK;

namespace Aiv.Fast2D.Component {
    public class Rigidbody : Component {


        private Vector2 velocity;
        public Vector2 Velocity {
            get { return velocity; }
            set { velocity = value; }
        }

        private float friction;
        public float Friction {
            get { return friction; }
            set {
                if (value < 0) return;
                friction = value;
            }
        }

        public bool IsGravityAffected;

        public Rigidbody(GameObject owner) : base (owner) {
            PhysicsMgr.AddRB(this);
        }

        public virtual void FixedUpdate () {
            if (IsGravityAffected) {
                velocity.Y += Game.Gravity * Game.DeltaTime;
            }
            if (velocity.LengthSquared != 0 && Friction > 0) {
                float fAmout = Friction * Game.DeltaTime;
                float newVelocityLength = velocity.Length - fAmout;
                if (newVelocityLength < 0) {
                    velocity = Vector2.Zero;
                } else {
                    velocity = velocity.Normalized() * newVelocityLength;
                }
            }
            transform.Position += Velocity * Game.DeltaTime;
        }

    }
}
