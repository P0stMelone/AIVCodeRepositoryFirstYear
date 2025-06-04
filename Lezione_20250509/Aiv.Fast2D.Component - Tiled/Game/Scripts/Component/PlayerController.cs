using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class PlayerController : UserComponent {

        private string axisName, jumpButtonName, shootButtonName;
        private float speed, jumpVelocity;
        private int maxConsecutiveJump;

        private Rigidbody rigibody;
        private SpriteRenderer sr;
        private BulletMgr bulletMgr;
        private SheetAnimator animator;

        private int jumpCount;

        private float reloadTime = 0.3f;
        private float currentReloadTime;

        private bool isGrounded;
        public bool IsGrounded {
            get { return isGrounded; }
            set {
                isGrounded = value;
            }
        }

        public PlayerController (GameObject owner, string axisName, string jumpButtonName, string shootButtonName,
                float speed, float jumpVelocity, int maxConsecutiveJump) : base (owner) {
            this.axisName = axisName;
            this.jumpButtonName = jumpButtonName;
            this.shootButtonName = shootButtonName;
            this.speed = speed;
            this.jumpVelocity = jumpVelocity;
            this.maxConsecutiveJump = maxConsecutiveJump;
            jumpCount = 0;
        }

        public override void Awake() {
            rigibody = GetComponent<Rigidbody>();
            sr = GetComponent<SpriteRenderer>();
            animator = GetComponent<SheetAnimator>();
            bulletMgr = GameObject.Find("BulletMgr").GetComponent<BulletMgr>();
        }

        public override void Update() {
            HandleMovement();
            HandleJump();
            HandleShoot();
        }

        public override void LateUpdate() {
            if (transform.Position.Y <= Game.Win.OrthoHeight) return;
            TouchedGround(Game.Win.OrthoHeight);
        }

        private void HandleMovement () {
            float axis = Input.GetAxis(axisName);
            rigibody.Velocity = new Vector2(axis * speed, rigibody.Velocity.Y);
        }

        private void HandleShoot () {
            currentReloadTime += Game.DeltaTime;
            if (currentReloadTime < reloadTime) return;
            if (!Input.GetInputActionButton(shootButtonName)) return;
            Bullet bullet = bulletMgr.GetBullet(BulletType.GreenGlobe);
            if (bullet == null) return;
            animator.ChangeClip("Shooting");
            Vector2 startPosition = transform.Position - Vector2.UnitY * sr.Width * 0.5f * transform.Scale.Y;
            Vector2 direction = Input.MousePosition + CameraMgr.MainCamera.position 
                - CameraMgr.MainCamera.pivot - startPosition;
            bullet.Shoot(startPosition, direction, gameObject);
            currentReloadTime = 0;
        }

        private void HandleJump () {
            if (jumpCount >= maxConsecutiveJump) return;
            if (!Input.GetInputActionButtonDown(jumpButtonName)) return;
            IsGrounded = false;
            rigibody.Velocity = new Vector2(rigibody.Velocity.X, jumpVelocity);
            rigibody.IsGravityAffected = true;
            jumpCount++;
        }

        public void TouchedGround (float groundYPosition) {
            transform.Position = new Vector2(transform.Position.X, groundYPosition);
            rigibody.Velocity = new Vector2(rigibody.Velocity.X, 0);
            rigibody.IsGravityAffected = false;
            IsGrounded = true;
            jumpCount = 0;
        }

        public override void OnCollide(Collision collisionInfo) {
            if (collisionInfo.Collider.gameObject.Tag== "Tile") {
                HandleTileCollision(collisionInfo);
            }
        }

        private void HandleTileCollision (Collision collisionInfo) {
            SpriteRenderer otherSR = collisionInfo.Collider.GetComponent<SpriteRenderer>();
            if (collisionInfo.Delta.X < collisionInfo.Delta.Y) {
                if (MathHelper.ApproximatelyEquivalent(sr.BottomRight.X - collisionInfo.Delta.X,
                    otherSR.TopLeft.X, 0.0001d)) {
                    transform.Position = new Vector2(transform.Position.X - collisionInfo.Delta.X,
                        transform.Position.Y);
                    rigibody.Velocity = new Vector2(0, rigibody.Velocity.Y);
                } else {
                    transform.Position = new Vector2(transform.Position.X + collisionInfo.Delta.X,
                        transform.Position.Y);
                    rigibody.Velocity = new Vector2(0, rigibody.Velocity.Y);
                }
            } else {
                if (MathHelper.ApproximatelyEquivalent(sr.TopLeft.Y + collisionInfo.Delta.Y,
                    otherSR.BottomRight.Y, 0.0001d)) {
                    transform.Position = new Vector2(transform.Position.X,
                        sr.TopLeft.Y + collisionInfo.Delta.Y + sr.Height * transform.Scale.Y);
                    rigibody.Velocity = new Vector2(rigibody.Velocity.X, 0);
                } else {
                    TouchedGround(transform.Position.Y - collisionInfo.Delta.Y);
                    rigibody.IsGravityAffected = true;
                }
            }
        }


    }
}
