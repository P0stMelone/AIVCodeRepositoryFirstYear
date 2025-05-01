using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public static class ColliderFactory {

        public static Collider CreateCircleFor (GameObject obj) {
            SpriteRenderer sr = obj.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            float halfDiagonal = (float)(Math.Sqrt(sr.Width * sr.Width + sr.Height * sr.Height)) * 0.5f;
            return new CircleCollider(obj, sr.CenterOffset, halfDiagonal);
        }

        public static Collider CreateBoxFor (GameObject obj) {
            SpriteRenderer sr = obj.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            return new BoxCollider(obj, sr.CenterOffset, sr.Width, sr.Height);
        }

    }
}
