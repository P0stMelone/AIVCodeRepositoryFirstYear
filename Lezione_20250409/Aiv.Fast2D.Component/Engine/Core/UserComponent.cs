﻿
namespace Aiv.Fast2D.Component {
    public class UserComponent : Component, IStartable, IUpdatable, ICollidable {

        public override bool Enabled {
            get => base.Enabled;
            set {
                if (gameObject.IsActive) {
                    if (Enabled && !value) {
                        OnDisable();
                    } else if (!Enabled && value) {
                        OnEnable();
                    }
                }
                base.Enabled = value;
            }
        }
        public bool EnabledSelf {
            get { return enabled; }
        }

        public UserComponent (GameObject gameObject) : base (gameObject) {

        }

        public virtual void Awake() {

        }

        public virtual void Start() {

        }

        public virtual void OnEnable () {

        }

        public virtual void OnDisable() {

        }

        public virtual void Update () {

        }

        public virtual void LateUpdate () {

        }

        public virtual void OnCollide (GameObject other) {

        }

        public virtual void OnDestroy () {

        }

    }
}
