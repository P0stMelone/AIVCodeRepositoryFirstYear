using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace SpaceShooter_NoComponent {
    public class CompoundCollider : Collider {

        protected Collider boundingCollider; //collider più grosso che racchiude sicuramente tutta la geometria

        protected List<Collider> colliders;

        public CompoundCollider(Rigidbody rigidbody, Collider boundingCollider) : base(rigidbody) {
            this.boundingCollider = boundingCollider;
            colliders = new List<Collider>();
        }

        public void AddCollider(Collider collider) {
            if (colliders.Contains(collider)) return;
            colliders.Add(collider);
        }

        public override bool Collides(Collider collider) {
            return collider.Collides(this);
        }

        public override bool Collides(CircleCollider circle) {
            return InternalCollides(circle);
        }

        public override bool Collides(BoxCollider box) {
            return InternalCollides(box);
        }

        protected bool InternalCollides(Collider collider) {
            if (!boundingCollider.Collides(collider)) return false;
            foreach (Collider c in colliders) {
                if (collider.Collides(c)) {
                    return true;
                }
            }
            return false;
        }

        public override bool Collides(CompoundCollider compound) {
            if (!boundingCollider.Collides(compound.boundingCollider)) return false;
            for (int i = 0; i < colliders.Count; i++) {
                for (int j = 0; j < compound.colliders.Count; j++) {
                    if (colliders[i].Collides(compound.colliders[j])) return true;
                }
            }
            return false;
        }

        public override bool Contains(Vector2 point) {
            if (!boundingCollider.Contains(point)) return false;
            foreach(Collider collider in colliders) {
                if (collider.Contains(point)) return true;
            }
            return false;
        }

        public void RemoveCollider(Collider collider) {
            if (!colliders.Contains(collider)) return;
            colliders.Remove(collider);
        }



    }
}
