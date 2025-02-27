using OpenTK;

namespace Aiv.Fast2D.Component {
    public class Transform: Component {

        private Vector2 position;
        public Vector2 Position {
            get {
                return position;
            }
            set {
                position = value;
            }
        }
        private float rotation;
        public float Rotation {
            get {
                return rotation;
            }
            set {
                rotation = value;
            }
        }
        private Vector2 scale;
        public Vector2 Scale {
            get {
                return scale;
            }
            set {
                scale = value;
            }
        }

        public Transform(GameObject gameObject, Vector2 position, Vector2 scale, float rotation = 0) : base(gameObject) {
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }

    }
}
