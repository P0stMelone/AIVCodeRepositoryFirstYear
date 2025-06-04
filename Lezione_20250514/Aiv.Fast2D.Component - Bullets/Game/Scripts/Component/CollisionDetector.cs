using OpenTK;
using System;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public class CollisionDetector : UserComponent {

        public System.Action groundTouched;

        private Collider collider;
        private SpriteRenderer sr;
        private Rigidbody rb;

        private string[] groundsTag;

        private bool isGround;
        public bool IsGround {
            get { return isGround; }
        }

        public CollisionDetector (GameObject owner, string[] groundsTag) : base (owner) {
            this.groundsTag = groundsTag;
        }


        public override void Awake() {
            rb = GetComponent<Rigidbody>();
            sr = GetComponent<SpriteRenderer>();
            collider = GetComponent<Collider>();
        }

        private void HandleGroundCollision(Collision collisionInfo) {
            SpriteRenderer otherSR = collisionInfo.Collider.GetComponent<SpriteRenderer>();
            if (collisionInfo.Delta.X < collisionInfo.Delta.Y) {
                if (MathHelper.ApproximatelyEquivalent(sr.BottomRight.X - collisionInfo.Delta.X,
                    otherSR.TopLeft.X, 0.0001d)) {
                    transform.Position = new Vector2(transform.Position.X - collisionInfo.Delta.X,
                        transform.Position.Y);
                    rb.Velocity = new Vector2(0, rb.Velocity.Y);
                } else {
                    transform.Position = new Vector2(transform.Position.X + collisionInfo.Delta.X,
                        transform.Position.Y);
                    rb.Velocity = new Vector2(0, rb.Velocity.Y);
                }
            } else {
                if (MathHelper.ApproximatelyEquivalent(sr.TopLeft.Y + collisionInfo.Delta.Y,
                    otherSR.BottomRight.Y, 0.0001d)) {
                    transform.Position = new Vector2(transform.Position.X,
                        sr.TopLeft.Y + collisionInfo.Delta.Y + sr.Height * transform.Scale.Y);
                    rb.Velocity = new Vector2(rb.Velocity.X, 0);
                } else {
                    TouchedGround(transform.Position.Y - collisionInfo.Delta.Y);
                }
            }
        }

        private void TouchedGround(float groundYPosition) {
            transform.Position = new Vector2(transform.Position.X, groundYPosition);
            rb.Velocity = new Vector2(rb.Velocity.X, 0);
            isGround = true;
            groundTouched?.Invoke();
        }

        private bool IsGroundTag (string tag) {
            foreach (string ground in groundsTag) {
                if (ground == tag) return true;
            }
            return false;
        }

        private void CheckIsGrounded() {
            if (!isGround) return;
            List<Collider> contacts = PhysicsMgr.GetColliderContacts(collider);
            if (contacts == null) {
                isGround = false;
                return;
            }
            if (contacts.Count <= 0) {
                isGround = false;
                return;
            }
            foreach (Collider c in contacts) {
                if (IsGroundTag(c.gameObject.Tag)) return;
            }
            isGround = false;
        }

        public override void OnCollideEnter(Collision collisionInfo) {
            Console.WriteLine("OnCollideEnter su: " + gameObject.Name + " con: " + collisionInfo.Collider.gameObject.Name);
            OnCollide(collisionInfo);
        }

        public override void OnCollide(Collision collisionInfo) {
            Console.WriteLine("OnCollide su: " + gameObject.Name + " con: " + collisionInfo.Collider.gameObject.Name);
            if (IsGroundTag(collisionInfo.Collider.gameObject.Tag)) {
                HandleGroundCollision(collisionInfo);
            }
        }

        public override void OnCollideExit(Collision collisionInfo) { 
            Console.WriteLine("OnCollideExit su: " + gameObject.Name + " con: " + collisionInfo.Collider.gameObject.Name);
        }

    }
}
