﻿using Aiv.Fast2D;
using OpenTK;

namespace SpaceShooter_NoComponent {
    public class Player : Actor {

        public BulletType BulletType;

        public Player (Vector2 position,string texturePath, int maxHp)
            : base (position, texturePath, maxHp) {
            Reset();
            rigidbody.Type = RigidbodyType.Player;
            BulletType = BulletType.BlueLaser;
            healthBar.IsActive = true;
        }

        public override void Update () {
            ManageShoot();
            CheckInput();
            base.Update();
            KeepInBorder();
        }

        private void ManageShoot () {
            currentReloadTime += Game.Win.DeltaTime;
            if (currentReloadTime < reloadTime) return;
            if (!Game.Win.GetKey(KeyCode.Space)) return;
            switch (BulletType) {
                case BulletType.BlueLaser:
                    ShootBlueLaser();
                    break;
                case BulletType.GreenGlobe:
                    ShootGreenGlobe();
                    break;
            }
        }

        private void ShootBlueLaser () {
            Bullet b = BulletMgr.GetBullet(BulletType);
            if (b == null) return; //questa situazione, se è una pool di gameplay non deve mai succedere
            b.Shoot(new Vector2(Position.X + sprite.Width / 2, Position.Y), Vector2.UnitX);
            currentReloadTime = 0;
        }

        private void ShootGreenGlobe () {
            Vector2 bulletDirection = new Vector2(1, -1);
            for (int i = 0; i < 3; i++) {
                Bullet b = BulletMgr.GetBullet(BulletType);
                if (b != null) {
                    b.Shoot(new Vector2(Position.X + sprite.Width / 2, Position.Y), bulletDirection);
                    bulletDirection.Y++;
                    currentReloadTime = 0;
                }
            }
        }

        private void CheckInput () {
            Vector2 inputDirection = Vector2.Zero;
            if (Game.Win.GetKey(KeyCode.W)) {
                inputDirection.Y = -1;
            }
            if (Game.Win.GetKey(KeyCode.S)) {
                inputDirection.Y = 1;
            }
            if (Game.Win.GetKey(KeyCode.A)) {
                inputDirection.X = -1;
            }
            if (Game.Win.GetKey(KeyCode.D)) {
                inputDirection.X = 1;
            }
            if (inputDirection == Vector2.Zero) {
                rigidbody.Velocity = Vector2.Zero;
            } else {
                rigidbody.Velocity = inputDirection.Normalized() * speed;
            }
        }

        private void KeepInBorder () {
            if (Position.X + Width / 2 > Game.Win.Width) {
                Position = new Vector2(Game.Win.Width - Width / 2, Position.Y);
            } else if (Position.X - Width / 2 < 0) {
                Position = new Vector2(Width / 2, Position.Y);
            }
            if (Position.Y + Height / 2 > Game.Win.Height) {
                Position = new Vector2(Position.X, Game.Win.Height - Height / 2);
            } else if (Position.Y - Height / 2 < 0) {
                Position = new Vector2(Position.X, Height / 2);
            }
        }

    }
}
