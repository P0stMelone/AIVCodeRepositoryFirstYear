using System.Collections.Generic;
using OpenTK;
using System;

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
        public bool IsActive { get; set; }
        public Transform transform {
            get;
            private set;
        }
        private List<Component> components;
        private uint collisionMask;
        private uint layer;
        public uint Layer {
            get { return layer; }
            set {
                layer = value;
            }
        }
        #endregion

        #region Factories
        public static GameObject CreateGameObject (string name, Vector2 position) {
            return new GameObject(name, position, Vector2.One);
        }
        public static GameObject CreateGameObject(string name, Vector2 position, Vector2 scale, float rotation = 0) {
            return new GameObject(name, position, scale, rotation);
        }
        private GameObject (string name, Vector2 position, Vector2 scale, float rotation = 0) {
            Name = name;
            IsActive = true;
            transform = new Transform(this, position, scale, rotation);
            components = new List<Component>();
            components.Add(transform);
            Game.CurrenScene.AddGameObject(this);
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

        #region Component
        public void AddComponent (Component component) {
            components.Add(component);
        }

        public Component AddComponent (Type type, params object[] constructorParameters) {
            object[] parameters = new object[constructorParameters.Length + 1];
            parameters[0] = this;
            for (int i = 1; i < parameters.Length; i++) {
                parameters[i] = constructorParameters[i - 1];
            }
            Component component = Activator.CreateInstance(type, parameters) as Component;
            AddComponent(component);
            return component;
        }

        public Component GetComponent (Type type) {
            foreach(Component component in components) {
                if (component.GetType() == type) return component;
            }
            Type currentType;
            foreach(Component component in components) {
                currentType = component.GetType().BaseType;
                while (currentType != typeof(object)) {
                    if (currentType == type) {
                        return component;
                    }
                    currentType = currentType.BaseType;
                }
            }
            return null;
        }
        #endregion

        #region Utility
        public static GameObject Find(string name) {
            return Game.CurrenScene.FindGameObject(name);
        }

        public static GameObject[] FindAll (string name) {
            return Game.CurrenScene.FindGameObjects(name);
        }
        #endregion

        #region CollisionMask
        public bool CanCollide (uint layer) {
            return (collisionMask & layer) != 0;
        }

        public void AddCollisionLayer (uint layer) {
            collisionMask |= layer;
        }
        #endregion

    }
}
