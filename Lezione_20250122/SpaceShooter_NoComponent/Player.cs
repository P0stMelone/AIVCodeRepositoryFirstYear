using Aiv.Fast2D;
using OpenTK;

namespace SpaceShooter_NoComponent {
    public class Player : Actor {


        public Player (Vector2 position,string texturePath, int maxHp)
            : base (position, texturePath, maxHp) {

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
            Bullet b = BulletMgr.GetBullet(BulletType.BlueLaser);
            if (b == null) return; //questa situazione, se è una pool di gameplay non deve mai succedere
            b.Shoot(new Vector2(Position.X + sprite.Width / 2, Position.Y), Vector2.UnitX);
            currentReloadTime = 0;
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
                velocity = Vector2.Zero;
            } else {
                velocity = inputDirection.Normalized() * speed;
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
