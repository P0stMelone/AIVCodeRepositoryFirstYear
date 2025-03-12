using OpenTK;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public class PhysicsMgr {

        private static List<Rigidbody> rbs;
        private static List<Collider> colliders;

        static PhysicsMgr () {
            rbs = new List<Rigidbody>();
            colliders = new List<Collider>();
        }

        public static void AddRB (Rigidbody rb) {
            if (rbs.Contains(rb)) return;
            rbs.Add(rb);
        }

        public static void RemoveRB(Rigidbody rb) {
            if (!rbs.Contains(rb)) return;
            rbs.Remove(rb);
        }

        public static void AddCollider (Collider c) {
            if (colliders.Contains(c)) return;
            colliders.Add(c);
        }

        public static void RemoveCollider (Collider c) {
            if (!colliders.Contains(c)) return;
            colliders.Remove(c);
        }

        public static void FixedUpdate () {
            foreach(Rigidbody rb in rbs) {
                if (!rb.Enabled) continue;
                rb.FixedUpdate();
            }
        }

        public static void CheckCollisions() {
            for (int i = 0; i < colliders.Count - 1; i++) {
                if (!colliders[i].Enabled) continue;
                for (int j = i +1; j < colliders.Count; j++) {
                    if (!colliders[j].Enabled) continue;
                    bool firstCheck = colliders[i].CanCollide(colliders[j].Layer);
                    bool secondCheck = colliders[j].CanCollide(colliders[i].Layer);
                    if (firstCheck || secondCheck) {
                        bool collision = colliders[i].Collides(colliders[j]);
                        if (!collision) continue;
                        if (firstCheck) colliders[i].gameObject.OnCollide(colliders[j].gameObject);
                        if (secondCheck) colliders[j].gameObject.OnCollide(colliders[i].gameObject);
                    }
                }
            }
        } 


        public void ClearAll () {
            rbs.Clear();
            colliders.Clear();
        }

    }
}
