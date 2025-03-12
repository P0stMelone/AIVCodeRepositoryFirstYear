using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class EnemyBullet : Bullet {

        protected float accumulator;

        public EnemyBullet(GameObject owner, BulletType bulletType, int damage, float speed) :
            base(owner, bulletType, damage, speed) {

        }

        public override void Start() {
            base.Start();
            gameObject.Layer = (uint)Layer.EnemyBullet;
            gameObject.AddCollisionLayer((uint)Layer.Player);
            rb.Velocity = new Vector2(-speed, 0);
        }

        public override void Update() {
            if (bulletType == BulletType.FireGlobe) {
                accumulator += Game.DeltaTime * 10;
                rb.Velocity = new Vector2(rb.Velocity.X, (float)Math.Cos(accumulator) * 350);
            }
        }

        public override void LateUpdate() {
            if (sr.BottomRight.X <= 0) {
                DestroyMe();
            }
        }

        public override void OnCollide(GameObject other) {
            //Collisione rilevata con Player
        }

    }
}
