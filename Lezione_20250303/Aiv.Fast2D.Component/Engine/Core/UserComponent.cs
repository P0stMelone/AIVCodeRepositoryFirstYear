using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component {
    public class UserComponent : Component, IStartable, IUpdatable, ICollidable {

        public UserComponent (GameObject gameObject) : base (gameObject) {

        }

        public virtual void Start() {

        }

        public virtual void Update () {

        }

        public virtual void LateUpdate () {

        }

        public virtual void OnCollide (GameObject other) {

        }

    }
}
