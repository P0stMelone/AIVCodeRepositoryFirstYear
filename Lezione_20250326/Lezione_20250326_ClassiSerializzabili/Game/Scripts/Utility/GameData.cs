using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component {
    [Serializable]
    public class GameData {

        //PlayerData playerData;

        private int numberOfShoot;
        public int NumberOfShoot {
            get { return numberOfShoot; }
        }
        private float timePlayed;
        public float TimePlayed {
            get { return timePlayed; }
        }

        public void IncreaseNumberOfShoot () {
            numberOfShoot++;
        }

        public void IncreaseTimePlayed (float dt) {
            timePlayed += dt;
        }

    }

}
