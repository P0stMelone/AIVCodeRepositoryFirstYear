using System;

namespace SpaceShooter_NoComponent {
    public abstract class Scene {

        public bool IsPlaying { get; protected set; }
        public Scene nextScene;

        public Scene () {

        }

        public virtual void Start () {
            IsPlaying = true;
            LoadAssets();
        }

        protected virtual void LoadAssets () {

        }

        public virtual Scene OnExit () {
            IsPlaying = false;
            UpdateMgr.ClearAll();
            DrawMgr.ClearAll();
            PhysicsMgr.ClearAll();
            GfxMgr.ClearAll();
            GC.Collect(); //forziamo il garbage collector
            return nextScene;
        }


        public virtual void GameLoop () {
            PhysicsMgr.FixedUpdate();
            PhysicsMgr.CheckCollisions();
            UpdateMgr.Update();
            UpdateMgr.LateUpdate();
            DrawMgr.Draw();
        }

    }
}
