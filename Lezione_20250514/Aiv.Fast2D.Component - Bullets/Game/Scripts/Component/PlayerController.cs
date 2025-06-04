using OpenTK;
using System;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public class PlayerController : UserComponent {

        private string axisName, jumpButtonName, shootButtonName;
        private float speed, jumpVelocity;
        private int maxConsecutiveJump;

        private Rigidbody rigibody;
        private SpriteRenderer sr;
        private SheetAnimator animator;
        private CollisionDetector cd;
        private ShootModule sm;

        private int jumpCount;

        private float reloadTime = 0.3f;
        private float currentReloadTime;

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
            sm = GetComponent<ShootModule>();
            cd = GetComponent<CollisionDetector>();
            cd.groundTouched += OnGroundTouched;
        }

        public override void OnDestroy() {
            cd.groundTouched -= OnGroundTouched;
        }

        private void OnGroundTouched () {
            jumpCount = 0;
        }

        public override void Update() {
            HandleMovement();
            HandleJump();
            HandleShoot();
        }


        private void HandleMovement () {
            float axis = Input.GetAxis(axisName);
            rigibody.Velocity = new Vector2(axis * speed, rigibody.Velocity.Y);
        }

        private void HandleShoot () {
            currentReloadTime += Game.DeltaTime;
            if (currentReloadTime < reloadTime) return;
            if (!Input.GetInputActionButton(shootButtonName)) return;
            Vector2 startPosition = transform.Position - Vector2.UnitY * sr.Width * 0.5f * transform.Scale.Y;
            Vector2 direction = Input.MousePosition + CameraMgr.MainCamera.position 
                - CameraMgr.MainCamera.pivot - startPosition;
            if (!sm.Shoot(startPosition, direction)) return;
            animator.ChangeClip("Shooting");
            currentReloadTime = 0;
        }

        private void HandleJump () {
            if (jumpCount >= maxConsecutiveJump) return;
            if (!Input.GetInputActionButtonDown(jumpButtonName)) return;
            rigibody.Velocity = new Vector2(rigibody.Velocity.X, jumpVelocity);
            jumpCount++;
        }
    }
}
