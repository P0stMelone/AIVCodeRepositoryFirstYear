using OpenTK;
using System;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {

    struct CameraLimits {
        public float MaxX;
        public float MinX;
        public float MaxY;
        public float MinY; 


        public CameraLimits(float maxX, float minX, float maxY, float minY) {
            MaxX = maxX;
            MinX = minX;
            MaxY = maxY;
            MinY = minY;
        }

        public float ValidateX (float x) {
            return x > MaxX ? MaxX : x < MinX ? MinX : x;
        }

        public float ValidateY (float y) {
            return y > MaxY ? MaxY : y < MinY ? MinY : y;
        }


    }

    public static class CameraMgr {

        private static float speed;
        public static float Speed {
            get { return speed; }
            set {
                speed = value == 0 ? speed : value;
            }
        }
        public static Transform target;
        public static Camera MainCamera { get; private set; }
        private static CameraLimits limits;

        private static Dictionary<string, Tuple<Camera, float>> cameras;

        static CameraMgr () {
            MainCamera = new Camera();
            speed = 10f;
            cameras = new Dictionary<string, Tuple<Camera, float>>();
        }

        public static bool InsideCameraLimits (Vector2 position) {
            return position.X > limits.MinX - MainCamera.pivot.X &&
                position.X < limits.MaxX + MainCamera.pivot.X &&
                position.Y > limits.MinY - MainCamera.pivot.Y &&
                position.Y < limits.MaxY + MainCamera.pivot.Y;
        }

        public static void SetCameraLimits(float minX, float maxX, float minY, float maxY) {
            limits = new CameraLimits(maxX, minX, maxY, minY);
        }

        public static void Init (Vector2 position, Vector2 pivot) {
            MainCamera.position = position;
            MainCamera.pivot = pivot;
        }

        public static void AddCameras (string name, Camera camera = null, float speed = 0) {
            if (cameras.ContainsKey(name)) return;
            if (camera == null) {
                camera = new Camera(MainCamera.position.X, MainCamera.position.Y);
                camera.pivot = MainCamera.pivot;
            }
            cameras.Add(name, new Tuple<Camera, float>(camera, speed));
        }

        public static Camera GetCamera (string name) {
            if (!cameras.ContainsKey(name)) return null;
            return cameras[name].Item1;
        }

        public static void MoveCameras () {
            if (target == null) return;
            Vector2 oldPosition = MainCamera.position;
            MainCamera.position = Vector2.Lerp(MainCamera.position, target.Position, Game.Win.DeltaTime * speed);
            MainCamera.position = new Vector2(limits.ValidateX(MainCamera.position.X), limits.ValidateY(MainCamera.position.Y));
            Vector2 delta = MainCamera.position - oldPosition;
            foreach(var camera in cameras) {
                camera.Value.Item1.position += delta * camera.Value.Item2;
            }
        }

        public static void ClearAll() {
            cameras.Clear();
        }

    }
}
