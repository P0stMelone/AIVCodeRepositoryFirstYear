using Aiv.Fast2D;
using OpenTK;

namespace SpaceShooter_NoComponent {
    public class GameObject : IUpdatable, IDrawable {

        protected Sprite sprite;
        protected Texture texture;

        protected float speed;
        protected Vector2 velocity;

        public Vector2 Position {
            get { return sprite.position; }
            set { sprite.position = value; }
        }
        public int Width {
            get { return (int)sprite.Width; }
        }
        public int Height {
            get { return (int)sprite.Height; }
        }
        public virtual bool IsActive {
            get {
                return true;
            }
            protected set {

            }
        }


        public GameObject(string textureName) {
            texture = GfxMgr.GetTexture(textureName);
            sprite = new Sprite(texture.Width, texture.Height);
            UpdateMgr.AddItem(this);
            DrawMgr.AddItem(this);
        }

        public virtual void Update () {
            Move();
        }

        public virtual void LateUpdate() {

        }

        protected virtual void Move () {
            Position += velocity * Game.Win.DeltaTime;
        }

        public virtual void Draw () {
            sprite.DrawTexture(texture);
        }

    }
}
