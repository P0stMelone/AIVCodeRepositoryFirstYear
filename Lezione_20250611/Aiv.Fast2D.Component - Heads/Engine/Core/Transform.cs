using OpenTK;
using System;

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

        public Vector2 Forward {
            get {
                return new Vector2((float)Math.Cos(DegressToRadiants(rotation)), (float)Math.Sin(DegressToRadiants(rotation)));
            } set {
                rotation = RadiantsToDegrees((float)Math.Atan2(value.Y, value.X));
            }
        }

        public static float RadiantsToDegrees (float radiants) {
            return (180 / (float)Math.PI) * radiants;
        }

        public static float DegressToRadiants (float degress) {
            return degress * (float)Math.PI / 180;
        }

        public Transform(GameObject gameObject, Vector2 position, Vector2 scale, float rotation = 0) : base(gameObject) {
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }

        public override Component Clone(GameObject owner) {
            return new Transform(owner, position, scale, rotation);
        }

    }
}
