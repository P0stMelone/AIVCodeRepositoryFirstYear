using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component {
    public abstract class Component {


        public GameObject gameObject {
            get;
            private set;
        }
        public Transform transform {
            get { return gameObject.transform; }
        }

        protected bool enabled;
        public virtual bool Enabled {
            get {
                return enabled && gameObject.IsActive;
            }
            set {
                enabled = value;
            }
        }

        public Component (GameObject gameObject) {
            this.gameObject = gameObject;
            enabled = true;
        }

        public Component GetComponent (Type type) {
            return gameObject.GetComponent(type);
        }

        public T GetComponent<T>() where T : Component {
            return gameObject.GetComponent<T>();
        }

    }
}
