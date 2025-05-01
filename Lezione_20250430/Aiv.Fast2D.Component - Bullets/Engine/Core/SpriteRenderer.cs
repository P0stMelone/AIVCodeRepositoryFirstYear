using OpenTK;

namespace Aiv.Fast2D.Component {
    public class SpriteRenderer : Component, IDrawable {

        private DrawLayer layer;
        public DrawLayer Layer {
            get { return layer; }
            set { layer = value; }
        }
        private Sprite sprite;
        public Sprite Sprite {
            get { return sprite; }
        }
        private Vector2 textureOffset;
        public Vector2 TextureOffset {
            get { return textureOffset; }
            set { textureOffset = value; }
        }
        private Texture texture;
        public Texture Texture {
            get { return texture; }
            set { texture = value; }
        }
        private Vector2 pivot;
        public Vector2 Pivot {
            get { return pivot; }
            set {
                pivot = value;
                sprite.pivot = new Vector2(sprite.Width * pivot.X, sprite.Height * pivot.Y);
            }
        }
        public Vector2 CenterOffset {
            get {
                Vector2 temp = Vector2.Zero;
                temp.X = (0.5f - pivot.X) * Width;
                temp.Y = (0.5f - pivot.Y) * Height;
                return temp;
            }
        }
        public float Width {
            get { return sprite.Width; }
        }
        public float Height {
            get { return sprite.Height; }
        }
        public Vector2 TopLeft {
            get {
                return transform.Position - new Vector2(Width * pivot.X, Height * pivot.Y);
            }
        }
        public Vector2 BottomRight {
            get {
                return transform.Position + new Vector2(Width * (1 - pivot.X), Height * (1 - pivot.Y));
            }
        }

        public SpriteRenderer (GameObject gameObject, string textureName, Vector2 pivot, DrawLayer layer) : base (gameObject) {
            texture = GfxMgr.GetTexture(textureName);
            sprite = new Sprite(Game.PixelsToUnit(texture.Width), 
                Game.PixelsToUnit(texture.Height));
            Pivot = pivot;
            textureOffset = Vector2.Zero;
            this.layer = layer;
            DrawMgr.AddItem(this);
        }

        public SpriteRenderer (GameObject gameObject, string textureName, Vector2 pivot, DrawLayer layer, 
            float width, float height, Vector2 textureOffset) : base (gameObject) {
            texture = GfxMgr.GetTexture(textureName);
            sprite = new Sprite(Game.PixelsToUnit(width),Game.PixelsToUnit(height));
            Pivot = pivot;
            this.textureOffset = textureOffset;
            this.layer = layer;
            DrawMgr.AddItem(this);
        }

        private SpriteRenderer(GameObject gameObject) : base(gameObject) {
            DrawMgr.AddItem(this);
        }

        public void SetAdditiveColor (Vector4 color) {
            sprite.SetAdditiveTint(color);
        }

        public void Draw() {
            sprite.position = transform.Position;
            sprite.Rotation = transform.Rotation;
            sprite.scale = transform.Scale;
            sprite.DrawTexture(texture, (int)textureOffset.X, 
                (int)textureOffset.Y, (int)Game.UnitToPixels(Width), 
                (int)Game.UnitToPixels(Height));
        }

        public override Component Clone(GameObject owner) {
            SpriteRenderer clonedSR = new SpriteRenderer(owner);
            clonedSR.layer = layer;
            clonedSR.texture = texture;
            clonedSR.textureOffset = textureOffset;
            clonedSR.pivot = pivot;
            clonedSR.sprite = new Sprite(sprite.Width, sprite.Height);
            return clonedSR;
        }
    }
}
