using System.Collections.Generic;
using OpenTK;

namespace Aiv.Fast2D.Component {


    public class GameObject {

        #region StaticMembers
        private static int unamedGONumber = 0;
        internal static void ResetGONumber () {
            unamedGONumber = 0;
        }
        #endregion

        #region Attributes&Properties
        private string name;
        public string Name {
            get {
                return name;
            }
            set {
                if (!string.IsNullOrEmpty(value)) {
                    name = value;
                    return;
                }
                name = "GameObject_" + unamedGONumber;
                unamedGONumber++;
            }
        }
        public bool IsActive { get; }
        public Transform transform {
            get;
            private set;
        }
        private List<Component> components;
        #endregion

        #region Factories
        public static GameObject CreateGameObject (string name, Vector2 position) {
            return new GameObject(name, position, Vector2.One);
        }
        public static GameObject CreateGameObject(string name, Vector2 position, Vector2 scale, float rotation = 0) {
            return new GameObject(name, position, scale, rotation);
        }
        private GameObject (string name, Vector2 position, Vector2 scale, float rotation = 0) {
            IsActive = true;
            transform = new Transform(this, position, scale, rotation);
            components = new List<Component>();
            components.Add(transform);
        }
        #endregion

        #region GameLoopWrappers
        public void Start () {
            IStartable temp;
            foreach(Component component in components) {
                if (!component.Enabled) continue;
                temp = component as IStartable;
                if (temp == null) continue;
                temp.Start();
            }
        }
        public void Update () {
            IUpdatable temp;
            foreach(Component component in components) {
                if (!component.Enabled) continue;
                temp = component as IUpdatable;
                if (temp == null) continue;
                temp.Update();
            }
        }
        public void LateUpdate () {
            IUpdatable temp;
            foreach (Component component in components) {
                if (!component.Enabled) continue;
                temp = component as IUpdatable;
                if (temp == null) continue;
                temp.LateUpdate();
            }
        }
        public void OnCollide (GameObject other) {
            ICollidable temp;
            foreach (Component component in components) {
                if (!component.Enabled) continue;
                temp = component as ICollidable;
                if (temp == null) continue;
                temp.OnCollide(other);
            }
        }
        #endregion

        public void AddComponent (Component component) {
            components.Add(component);
        }

    }
}
