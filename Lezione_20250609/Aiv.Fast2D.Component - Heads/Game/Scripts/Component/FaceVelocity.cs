using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component {
    public class FaceVelocity : UserComponent {

        private float rotationSpeed;

        private Rigidbody rb;

        public FaceVelocity (GameObject owner, float rotationSpeed) : base (owner) {
            this.rotationSpeed = rotationSpeed;
        }

        public override void Awake() {
            rb = GetComponent<Rigidbody>();
        }

        public override void LateUpdate() {
            if (rb.Velocity.LengthSquared <= 0) return;
            Vector2 velocityNormalized = rb.Velocity.Normalized();
            float angle = Transform.RadiantsToDegrees
                ((float)Math.Acos(Vector2.Dot(transform.Forward, velocityNormalized)));
            if (angle == 180) {
                transform.Rotation += 10;
            } else {
                Vector2 newForward = Vector2.Lerp(transform.Forward, velocityNormalized, Game.DeltaTime * rotationSpeed);
                transform.Forward = newForward.Normalized();
            }
        }

    }
}
