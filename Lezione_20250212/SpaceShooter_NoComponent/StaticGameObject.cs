using OpenTK;

namespace SpaceShooter_NoComponent {
    public class StaticGameObject : GameObject{

        public StaticGameObject(string textureName) : base(textureName) {
            rigidbody.Velocity = Vector2.Zero;
            rigidbody.IsGravityAffected = false;
            rigidbody.IsCollisionAffected = false;
            IsActive = true;
        }


    }
}
