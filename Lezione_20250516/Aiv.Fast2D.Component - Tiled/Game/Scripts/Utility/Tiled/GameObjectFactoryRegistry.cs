using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component {
    public class GameObjectFactoryRegistry {

        Dictionary<string, IGameObjectFatory> registry = new Dictionary<string, IGameObjectFatory>();

        public void Register(string type, IGameObjectFatory factory) {
            registry.Add(type, factory);
        }

        public GameObject CreateGameObject(Tiled.Object obj) {
            IGameObjectFatory factory;
            if (registry.TryGetValue(obj.Type, out factory)) {
                return factory.CreateGameObject(obj);
            }
            return null;
        }
    }
}
