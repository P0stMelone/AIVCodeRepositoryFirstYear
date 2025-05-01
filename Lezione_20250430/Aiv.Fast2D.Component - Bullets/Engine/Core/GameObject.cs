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

        public string Tag { get; set; }

        private bool isActive;
        public bool IsActive {
            get {
                return isActive;
            }
            set {
                if (!isActive && value) {
                    isActive = value; //qui perché i componenti devono essere attivi per chiamare la onenable
                    OnEnable();
                } else if (isActive && !value) {
                    OnDisable();
                    isActive = value; //qui perché i componenti devono essere attivi per chiamare la ondisable
                }
                if (!isStarted && value && Game.CurrenScene.IsInitialized) { //caso in cui il gameobject viene creato e disattivato prima dell'inizio della scena
                    Start();
                }
            }
        }

        private bool isStarted;
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
        public static GameObject CreateGameObject (string name, Vector2 position, bool active) {
            GameObject gameObject = new GameObject(name, position, Vector2.Zero);
            gameObject.isActive = false;
            return gameObject;
        }
        public static GameObject CreateGameObject (string name, Vector2 position) {
            return new GameObject(name, position, Vector2.One);
        }
        public static GameObject CreateGameObject(string name, Vector2 position, Vector2 scale, float rotation = 0) {
            return new GameObject(name, position, scale, rotation);
        }
        private GameObject (string name, Vector2 position, Vector2 scale, float rotation = 0) {
            Name = name;
            isActive = true; 
            transform = new Transform(this, position, scale, rotation);
            components = new List<Component>();
            components.Add(transform);
            Game.CurrenScene.AddGameObject(this);
        }
        #endregion

        #region GameLoopWrappers
        public void Awake () {
            foreach(Component component in components) {
                IStartable startable = component as IStartable;
                if (startable == null) continue;
                startable.Awake();
            }
        }
        public void Start () {
            IStartable temp;
            foreach(Component component in components) {
                if (!component.Enabled) continue;
                temp = component as IStartable;
                if (temp == null) continue;
                temp.Start();
            }
            isStarted = true;
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

        public void OnEnable () {
            foreach(Component component in components) {
                if (!component.Enabled) continue;
                IStartable startable = component as IStartable;
                if (startable == null) continue;
                startable.OnEnable();
            }
        }

        public void OnDisable() {
            foreach (Component component in components) {
                if (!component.Enabled) continue;
                IStartable startable = component as IStartable;
                if (startable == null) continue;
                startable.OnDisable();
            }
        }

        public void OnDestroy () {
            foreach(Component component in components) {
                IStartable startable = component as IStartable;
                if (startable == null) continue;
                if (startable.Enabled) OnDisable(); //se è attivo, prima di distruggerlo lo disattiviamo
                startable.OnDestroy();
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

        public T AddComponent<T> (params object[] constructorParameters) where T:Component {
            object[] parameters = new object[constructorParameters.Length + 1];
            parameters[0] = this;
            for (int i = 1; i < parameters.Length; i++) {
                parameters[i] = constructorParameters[i - 1];
            }
            T component = Activator.CreateInstance(typeof(T), parameters) as T;
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

        public T GetComponent<T> () where T: Component {
            foreach(var component in components) {
                if (component is T) {
                    return component as T;
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

        public static GameObject Clone (GameObject gameObjectToClone) {
            GameObject clonedGameObject = new GameObject(gameObjectToClone.name,
                gameObjectToClone.transform.Position, gameObjectToClone.transform.Scale,
                gameObjectToClone.transform.Rotation);
            clonedGameObject.Tag = gameObjectToClone.Tag;
            clonedGameObject.isActive = gameObjectToClone.isActive;
            List<Component> clonedComponents = new List<Component>();
            foreach(Component c in gameObjectToClone.components) {
                clonedComponents.Add(c.Clone(clonedGameObject));
            }
            clonedGameObject.components = clonedComponents;
            return clonedGameObject;
        }

    }
}
