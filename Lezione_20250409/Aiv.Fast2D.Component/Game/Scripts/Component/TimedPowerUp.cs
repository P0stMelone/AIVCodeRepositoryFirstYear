
namespace Aiv.Fast2D.Component {
    public abstract class TimedPowerUp : PowerUp {

        protected float duration;
        protected float counter;

        public TimedPowerUp (GameObject owner, float duration) : base(owner) {
            this.duration = duration;
        }

        public override void Spawn () {
            base.Spawn();
            counter = duration;
        }

        public override void Update() {
            base.Update();
            if (attachedPlayer == null) return;
            counter -= Game.DeltaTime;
            if (counter <= 0) OnDetach();
        }

    }
}
