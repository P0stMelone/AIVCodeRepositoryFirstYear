using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Aiv.Fast2D.Component {
    public class CreateEnemyFactory : IGameObjectFatory {
        public GameObject CreateGameObject(Tiled.Object obj) {
            GameObject enemy = GameObject.CreateGameObject(obj.Name, new Vector2((float)obj.X, (float)obj.Y));
            
            return null;
        }
    }
}
