﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter_NoComponent {
    public class EnergyPowerUp : PowerUp {

        public EnergyPowerUp() : base("energyPowerUp") {

        }

        public override void OnAttach(Player p) {
            base.OnAttach(p);
            attachedPlayer.Reset();
            OnDeatch();
        }

    }
}
