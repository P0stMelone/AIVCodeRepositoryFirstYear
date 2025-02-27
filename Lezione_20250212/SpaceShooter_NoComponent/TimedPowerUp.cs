using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter_NoComponent {
    public abstract class TimedPowerUp : PowerUp {


        protected float duration;
        protected float counter;

        public TimedPowerUp (string textureName, float duration) : base (textureName) {
            this.duration = duration;
        }

        public override void OnAttach(Player p) {
            base.OnAttach(p);
            counter = duration;
        }

        public override void Update() {
            base.Update();
            if (attachedPlayer == null) return;
            counter -= Game.Win.DeltaTime;
            if (counter > 0) return;
            OnDeatch();
        }

    }
}
