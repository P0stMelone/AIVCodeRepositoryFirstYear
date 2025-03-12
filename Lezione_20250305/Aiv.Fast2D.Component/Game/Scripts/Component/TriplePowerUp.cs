
namespace Aiv.Fast2D.Component {
    public class TriplePowerUp : TimedPowerUp {

        public TriplePowerUp (GameObject owner, float duration) : base (owner, duration) {

        }

        public override void OnAttach(GameObject p) {
            base.OnAttach(p);
            PlayerController pc = p.GetComponent(typeof(PlayerController)) as PlayerController;
            pc.BulletType = BulletType.GreenGlobe;
        }

        public override void OnDetach() {
            PlayerController pc = attachedPlayer.GetComponent(typeof(PlayerController)) as PlayerController;
            pc.BulletType = BulletType.BlueLaser;
            base.OnDetach();
        }

    }
}
