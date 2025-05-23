﻿using Aiv.Fast2D;
using OpenTK;

namespace SpaceShooter_NoComponent {
    public class Enemy : Actor {

        protected BulletType myBulletType;
        protected RandomTimer randomTimer;

        public Enemy(Vector2 position, string texturePath, int maxHp, BulletType myBulletType) : base (position, texturePath, maxHp) {
            randomTimer = new RandomTimer(2, 5);
            hp = 0;
            sprite.FlipX = true;
            velocity = -Vector2.UnitX * speed;
            this.myBulletType = myBulletType;
        }

        public void SpawnMe (Vector2 startPosition) {
            randomTimer.Reset();
            this.Position = startPosition;
            Reset();
        }

        public override void Update() {
            base.Update();
            CheckBorder();
            ManageShoot();
        }

        protected virtual void ManageShoot () {
            randomTimer.Tick();
            if (!randomTimer.IsOver()) return;
            Bullet b = BulletMgr.GetBullet(myBulletType);
            if (b == null) return;
            b.Shoot(new Vector2 (Position.X - Width / 2, Position.Y), -Vector2.UnitX);
            randomTimer.Reset();
        }

        private void CheckBorder () {
            if (Position.X < -Width / 2) {
                Die();
            }
        }

        public override void TakeDamge(int damage) {
            base.TakeDamge(damage);
            if (hp <= 0) {
                Die();
            }
        }

        private void Die () {
            EnemyMgr.RestoreEnemy(this);
        }

    }
}
