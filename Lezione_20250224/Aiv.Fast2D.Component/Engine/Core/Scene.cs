using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public abstract class Scene {

        protected List<GameObject> sceneObjets;
        protected bool isInitialized;
        public bool IsInitialized {
            get {
                return isInitialized;
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

    }
}
