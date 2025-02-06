using Aiv.Fast2D; 
namespace SpaceShooter_NoComponent {
    public class GreenGlobe : PlayerBullet {

        public GreenGlobe (string texturePath) : base (texturePath, 30, 600, BulletType.GreenGlobe) {
            rigidbody.Collider = ColliderFactory.CreateCircleFor(this);
        }

    }
}
