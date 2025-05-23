﻿using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PlayerBullet : Bullet{

        public PlayerBullet(GameObject owner, BulletType bulletType, int damage, float speed) :
            base(owner, bulletType, damage, speed) {

        }

        public override void Start() {
            base.Start();
            gameObject.Layer = (uint)Layer.PlayerBullet;
            gameObject.AddCollisionLayer((uint)Layer.Enemy);
            gameObject.AddCollisionLayer((uint)Layer.EnemyBullet);
            rb.Velocity = new Vector2(speed, 0);
        }

        public override void LateUpdate() {
            if (sr.TopLeft.X > Game.Win.Width) {
                DestroyMe();
            }
        }

        public override void OnCollide(GameObject other) {
            //if (other.Layer == (uint)Layer.EnemyBullet) {
            if (other.Tag == "EnemyBullet") {
                (other.GetComponent(typeof(EnemyBullet)) as EnemyBullet).DestroyMe();
            } else if (other.Layer == (uint)Layer.Enemy) {
                EnemyComponent enemyComponent = other.GetComponent<EnemyComponent>();
                enemyComponent.TakeDamage(damage);
                if (!other.IsActive) {
                    shotBy.GetComponent<PlayerController>().Score += enemyComponent.Score;
                }
            }
            DestroyMe();
        }

    }
}
