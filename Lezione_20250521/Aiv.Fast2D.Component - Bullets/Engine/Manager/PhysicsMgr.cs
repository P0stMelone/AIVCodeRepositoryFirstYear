using OpenTK;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public static class PhysicsMgr {

        private static List<Rigidbody> rbs;
        private static List<Collider> colliders;
        private static Dictionary<Collider, List<Collider>> contacts;
        private static Dictionary<Collider, List<Collider>> previousFrameContacts;

        private static Collision collisionInfo;

        static PhysicsMgr () {
            rbs = new List<Rigidbody>();
            colliders = new List<Collider>();
            contacts = new Dictionary<Collider, List<Collider>>();
            previousFrameContacts = new Dictionary<Collider, List<Collider>>();
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
            contacts.Add(c, new List<Collider>());
            previousFrameContacts.Add(c, new List<Collider>());
        }

        public static void RemoveCollider (Collider c) {
            if (!colliders.Contains(c)) return;
            colliders.Remove(c);
            contacts.Remove(c);
            previousFrameContacts.Remove(c);
        }

        public static void FixedUpdate () {
            foreach(Rigidbody rb in rbs) {
                if (!rb.Enabled) continue;
                rb.FixedUpdate();
            }
        }

        public static List<Collider> GetColliderContacts (Collider c) {
            if (!contacts.ContainsKey(c)) return null;
            return contacts[c];
        }

        public static void CheckCollisions() {
            foreach (Collider c in colliders) {
                previousFrameContacts[c].Clear();
                previousFrameContacts[c].InsertRange(0, contacts[c]);
                contacts[c].Clear();
            }
            for (int i = 0; i < colliders.Count - 1; i++) {
                if (!colliders[i].Enabled) continue;
                for (int j = i +1; j < colliders.Count; j++) {
                    if (!colliders[j].Enabled) continue;
                    bool firstCheck = colliders[i].CanCollide(colliders[j].Layer);
                    bool secondCheck = colliders[j].CanCollide(colliders[i].Layer);
                    if (firstCheck || secondCheck) {
                        bool collision = colliders[i].Collides(colliders[j], ref collisionInfo);
                        if (!collision) continue;
                        if (firstCheck) {
                            collisionInfo.Collider = colliders[j];
                            if (previousFrameContacts[colliders[i]].Contains(colliders[j])) {
                                colliders[i].gameObject.OnCollide(collisionInfo);
                            } else {
                                colliders[i].gameObject.OnCollideEnter(collisionInfo);
                            }
                            contacts[colliders[i]].Add(colliders[j]);
                            previousFrameContacts[colliders[i]].Remove(colliders[j]);
                        }
                        if (secondCheck) {
                            collisionInfo.Collider = colliders[i];
                            if (previousFrameContacts[colliders[j]].Contains(colliders[i])) {
                                colliders[j].gameObject.OnCollide(collisionInfo);
                            } else {
                                colliders[j].gameObject.OnCollideEnter(collisionInfo);
                            }
                            previousFrameContacts[colliders[j]].Remove(colliders[i]);
                            contacts[colliders[j]].Add(colliders[i]);
                        }
                    }
                }
            }
            foreach(var cList in previousFrameContacts) {
                foreach(var c in cList.Value) {
                    cList.Key.gameObject.OnCollideExit(new Collision { Collider = c });
                }
            }
        } 


        public static void ClearAll () {
            rbs.Clear();
            colliders.Clear();
            contacts.Clear();
        }

    }
}
