using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    internal class BackgroundComponent : UserComponent {

        private SpriteRenderer srh;
        private SpriteRenderer srt;

        private float speed = 100;
        private float xOffset;

        public BackgroundComponent(GameObject gameObject) : base(gameObject) {
        }

        public override void Awake() {
            srh = GameObject.Find("Background").GetComponent<SpriteRenderer>();
            srt = GameObject.Find("BackgroundTail").GetComponent<SpriteRenderer>();
        }

        public override void Start() {
            
        }

        public override void Update() {
            srt.transform.Position = srh.transform.Position + Vector2.UnitX * (srh.Texture.Width-1);
            xOffset -= speed * Game.DeltaTime;
            xOffset = xOffset % srh.Texture.Width;
            srh.transform.Position = new Vector2(xOffset,0);
            if (srh.transform.Position.X + srh.Texture.Width < 0) {
                srh.transform.Position = Vector2.Zero;
            }
            
        }


    }
}
