using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvaders {
    public struct Vector2 {

        #region Attributes
        public float X;
        public float Y;
        #endregion

        #region InstanceMethods
        public Vector2 (float x, float y) {
            X = x;
            Y = y;
        }

        public Vector2 Sub (Vector2 vec) {
            return new Vector2(X - vec.X, Y - vec.Y);
        }

        public Vector2 Add (Vector2 vec) {
            return new Vector2(X + vec.X, Y + vec.Y);
        }

        public float GetLenght () {
            return (float)Math.Sqrt(GetLenghtSquared());
        }

        public float GetLenghtSquared () {
            return X * X + Y * Y;
        }
        #endregion

        #region StaticMethods
        public static Vector2 operator + (Vector2 a, Vector2 b) {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator - (Vector2 a, Vector2 b) {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator * (Vector2 a, float scalar) {
            return new Vector2(a.X * scalar, a.Y * scalar);
        }

        public static Vector2 Zero () {
            return new Vector2(0, 0);
        }

        public static Vector2 Right () {
            return new Vector2(1, 0);
        }

        public static Vector2 Down () {
            return new Vector2(0, 1);
        }
        #endregion

    }
}
