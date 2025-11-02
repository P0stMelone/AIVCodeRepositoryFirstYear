using OpenTK;

namespace Aiv.Fast2D.Component {

    public enum CollisionType { None, RectsIntersection, CircleIntersection }

    public struct Collision {

        public CollisionType Type;
        public Vector2 Delta;
        public Collider Collider;
        public float DeltaCircle;
        public Vector2 ContactPoint; //da implementare

    }
}
