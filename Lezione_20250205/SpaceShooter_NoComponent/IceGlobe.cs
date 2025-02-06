using Aiv.Fast2D;

namespace SpaceShooter_NoComponent {
    public class IceGlobe : EnemyBullet {

        public IceGlobe (string texturePath) : base (texturePath, 3, 500, BulletType.IceGlobe) {
            sprite.SetAdditiveTint(120, 255, 255, 0);
        }

    }
}
