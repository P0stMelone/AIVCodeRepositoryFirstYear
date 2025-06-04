using System.Collections.Generic;
using System;

namespace Aiv.Fast2D.Component {
    public abstract class Scene {

        protected List<GameObject> sceneObjets;
        protected bool isInitialized;
        public bool IsInitialized {
            get {
                return isInitialized;
            }
            set {
                isInitialized = value;
            }
        }

        private string assetsPath;

        #region Initialization
        public Scene (string assetsPath) {
            this.assetsPath = assetsPath;
        }

        public virtual void InitializeScene () {
            sceneObjets = new List<GameObject>();
            LoadAssets();
        }
        protected virtual void LoadAssets () {

        }
        protected void AddTexture (string textureName, string fileName) {
            GfxMgr.AddTexture(textureName, assetsPath + fileName);
        }
        #endregion

        #region GameLoop
        public void Awake () {
            foreach(GameObject obj in sceneObjets) {
                obj.Awake();
                if (obj.IsActive) {
                    obj.OnEnable();
                }
            }
        }

        public void Start () {
            foreach(GameObject obj in sceneObjets) {
                if (!obj.IsActive) continue;
                obj.Start();
            }
        }

        public void Update () {
            foreach(GameObject obj in sceneObjets) {
                if (!obj.IsActive) continue;
                obj.Update();
            }
        }

        public void LateUpdate() {
            foreach (GameObject obj in sceneObjets) {
                if (!obj.IsActive) continue;
                obj.LateUpdate();
            }
        }

        public void OnDestroy () {
            foreach(GameObject obj in sceneObjets) {
                obj.OnDestroy();
            }
        }
        #endregion

        #region Utility
        public void AddGameObject (GameObject gameObject) {
            if (sceneObjets.Contains(gameObject)) return;
            sceneObjets.Add(gameObject);
        }

        public GameObject FindGameObject (string name) {
            foreach(GameObject obj in sceneObjets) {
                if (obj.Name == name) return obj;
            }
            return null;
        }

        public GameObject[] FindGameObjects (string name) {
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (GameObject obj in sceneObjets) {
                if (obj.Name == name) gameObjects.Add(obj);
            }
            return gameObjects.ToArray();
        }
        #endregion

        #region Destruction
        public virtual void DestroyScene () {
            OnDestroy();
            sceneObjets.Clear();
            PhysicsMgr.ClearAll();
            DrawMgr.ClearAll();
            GfxMgr.ClearAll();
            FontMgr.ClearAll();
            AudioMgr.ClearAll();
            CameraMgr.ClearAll();
            GC.Collect();
        }
        #endregion

    }
}
