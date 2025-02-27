using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter_NoComponent {
    public class PowerUpMgr : IUpdatable {

        protected List<PowerUp> powerUps;

        RandomTimer timer;

        public bool IsActive {
            get { return true; }
        }

        public PowerUpMgr() {
            powerUps = new List<PowerUp>(2);
            powerUps.Add(new EnergyPowerUp());
            powerUps.Add(new TriplePowerUp());

            timer = new RandomTimer(4, 8);
            UpdateMgr.AddItem(this);
        }

        public void Update () {
            timer.Tick();
            if (!timer.IsOver()) return;
            if (powerUps.Count <= 0) return;
            int randomIndex = RandomGenerator.GetRandomInt(0, powerUps.Count - 1);
            PowerUp p = powerUps[randomIndex];
            powerUps.Remove(p);
            p.Spawn();
            timer.Reset();
        }

        public void LateUpdate () {
            //ciaone
        }

        public void Restore (PowerUp p) {
            if (powerUps.Contains(p)) return;
            powerUps.Add(p);
        }

        public void ClearAll () {
            powerUps.Clear();
        }
    }
}
