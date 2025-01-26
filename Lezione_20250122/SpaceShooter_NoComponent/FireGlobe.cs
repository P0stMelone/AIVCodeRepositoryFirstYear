using Aiv.Fast2D;
using OpenTK;
using System;

namespace SpaceShooter_NoComponent {
    public class FireGlobe : EnemyBullet {

        private float accumulator;

        public FireGlobe(Texture texture) : base(texture, 20, 500, BulletType.FireGlobe) {

        }

        public override void Shoot(Vector2 startPosition, Vector2 direction) {
            base.Shoot(startPosition, direction);
            accumulator = 0;
        }

        protected override void Move() {
            accumulator += 10 * Game.Win.DeltaTime;
            velocity.Y = (float)Math.Sin(accumulator) * 350;
            base.Move();
        }

    }
}
