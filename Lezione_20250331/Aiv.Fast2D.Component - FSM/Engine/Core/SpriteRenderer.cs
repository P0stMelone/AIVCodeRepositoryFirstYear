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
            get { return sprite.Width * transform.Scale.X; }
        }
        public float Height {
            get { return sprite.Height * transform.Scale.Y; }
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
            sprite = new Sprite(texture.Width, texture.Height);
            Pivot = pivot;
            this.layer = layer;
            DrawMgr.AddItem(this);
        }

        public void SetAdditiveColor (Vector4 color) {
            sprite.SetAdditiveTint(color);
        }

        public void Draw() {
            sprite.position = transform.Position;
            sprite.Rotation = transform.Rotation;
            sprite.scale = transform.Scale;
            sprite.DrawTexture(texture);
        }
    }
}
