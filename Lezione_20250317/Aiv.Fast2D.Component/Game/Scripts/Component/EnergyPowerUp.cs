
namespace Aiv.Fast2D.Component {
    public class EnergyPowerUp : PowerUp {

        private int healAmount;

        public EnergyPowerUp(GameObject owner, int healAmount) : base (owner) {
            this.healAmount = healAmount;
        }

        public override void OnAttach(GameObject p) {
            base.OnAttach(p);
            //(p.GetComponent(typeof(PlayerController)) as PlayerController).HealMe(healAmount);
            p.GetComponent<PlayerController>().HealMe(healAmount);
            base.OnDetach();
        }

    }
}
