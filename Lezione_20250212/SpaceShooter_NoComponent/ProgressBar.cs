using Aiv.Fast2D;
using OpenTK;

namespace SpaceShooter_NoComponent {
    public class ProgressBar : GameObject {

        protected Sprite barSprite;
        protected Texture barTexture;

        protected int barWidth;

        protected Vector2 barOffset;

        protected bool _internalActive;

        public override bool IsActive {
            get { return _internalActive; }
            set { _internalActive = value; }
        }

        public override Vector2 Position {
            get { return sprite.position; }
            set {
                sprite.position = value;
                barSprite.position = value + barOffset;
            }
        }

        public ProgressBar (string textureName, string barName, Vector2 offset) : base(textureName) {
            //sprite.position = Vector2.Zero;
            IsActive = false;
            barOffset = offset;
            barTexture = GfxMgr.GetTexture(barName);
            barSprite = new Sprite(barTexture.Width, barTexture.Height);
            barWidth = (int)barSprite.Width;
            rigidbody.IsCollisionAffected = false;
            rigidbody.IsGravityAffected = false;
        }


        public virtual void Scale (float scale) {
            if (scale < 0) {
                scale = 0;
            }
            barSprite.scale.X = scale;
            barWidth = (int)(barSprite.Width * scale);
            barSprite.SetMultiplyTint((1 - scale) * 50, scale * 2, scale,1);

        }

        public override void Draw() {
            base.Draw();
            barSprite.DrawTexture(barTexture, 0, 0, barWidth, (int)barSprite.Height);
        }



    }
}
