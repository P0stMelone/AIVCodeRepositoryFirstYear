using OpenTK;

namespace SpaceShooter_NoComponent {
    public class Enemy_0 : Enemy {

        public Enemy_0 () : base (Vector2.Zero, "enemy_0", 15, BulletType.IceGlobe) {
            score = 50;
        }

    }
}
