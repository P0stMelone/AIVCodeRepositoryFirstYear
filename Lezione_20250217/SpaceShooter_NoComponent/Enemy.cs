﻿using Aiv.Fast2D;
using OpenTK;

namespace SpaceShooter_NoComponent {
    public abstract class Enemy : Actor {

        protected BulletType myBulletType;
        protected RandomTimer randomTimer;
        protected int score;

        public Enemy(Vector2 position, string texturePath, int maxHp, BulletType myBulletType) : base (position, texturePath, maxHp) {
            randomTimer = new RandomTimer(1, 3);
            hp = 0;
            sprite.FlipX = true;
            rigidbody.Velocity = -Vector2.UnitX * speed;
            this.myBulletType = myBulletType;
            rigidbody.Type = RigidbodyType.Enemy;
            rigidbody.AddCollisionType(RigidbodyType.Player);
        }

        public void SpawnMe (Vector2 startPosition) {
            healthBar.IsActive = true;
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
                PlayScene playScene = (PlayScene)Game.CurrentScene;
                playScene.Player.Score += score;
                Die();
            }
        }

        private void Die () {
            PlayScene playScene = (PlayScene)Game.CurrentScene;
            playScene.EnemyMgr.RestoreEnemy(this);
            healthBar.IsActive = false;
        }

        public override void OnCollide(GameObject other) {
            if (other is Player) {
                Player p = (Player)other;
                p.TakeDamge((int)(hp * 0.25f));
                TakeDamge(int.MaxValue);
            }
        }
    }
}
