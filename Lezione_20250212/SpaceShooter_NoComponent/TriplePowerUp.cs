namespace SpaceShooter_NoComponent {
    public class TriplePowerUp : TimedPowerUp {

        public TriplePowerUp () : base ("triplePowerUp", 5) {

        }

        public override void OnAttach(Player p) {
            base.OnAttach(p);
            attachedPlayer.BulletType = BulletType.GreenGlobe;
        }

        public override void OnDeatch() {
            attachedPlayer.BulletType = BulletType.BlueLaser;
            base.OnDeatch();
        }

    }
}
