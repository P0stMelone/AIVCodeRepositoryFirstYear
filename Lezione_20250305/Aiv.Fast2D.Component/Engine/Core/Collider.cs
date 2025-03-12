using OpenTK;

namespace Aiv.Fast2D.Component {
    public abstract class  Collider : Component{

        public Vector2 Offset;
        public Vector2 Position { get { return transform.Position + Offset; } }
        public uint Layer {
            get { return gameObject.Layer; }
        }

        public Collider (GameObject owner, Vector2 offset) : base (owner) {
            Offset = offset;
            PhysicsMgr.AddCollider(this);
        }

        public abstract bool Collides(Collider collider);
        public abstract bool Contains(Vector2 point);
        public abstract bool Collides(CircleCollider circle);
        public abstract bool Collides(BoxCollider rect);

        public bool CanCollide (uint layer) {
            return gameObject.CanCollide(layer);
        }

    }
}
