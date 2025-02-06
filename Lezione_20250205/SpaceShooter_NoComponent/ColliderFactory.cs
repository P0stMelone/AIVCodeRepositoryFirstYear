﻿using System;

namespace SpaceShooter_NoComponent {
    public static class ColliderFactory {

        public static Collider CreateCircleFor (GameObject obj, bool innerCircle = true) {
            float radius;
            if (innerCircle) {
                radius = obj.Width * 0.5f;
            } else {
                radius = (float)(Math.Sqrt(obj.Width * obj.Width + obj.Height * obj.Height)) * 0.5f;
            }
            return new CircleCollider(obj.Rigidbody, radius);
        }

        public static Collider CreateBoxFor (GameObject obj) {
            return new BoxCollider(obj.Rigidbody, obj.Width, obj.Height);
        }

    }
}
