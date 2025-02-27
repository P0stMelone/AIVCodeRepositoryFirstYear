using OpenTK;

namespace Aiv.Fast2D.Component {
    public class KeepInBorder : UserComponent {

        private SpriteRenderer sr;

        public KeepInBorder(GameObject gameObject, SpriteRenderer sr) : base (gameObject) {
            this.sr = sr;
        }

        public override void LateUpdate() {
            if (sr.TopLeft.X < 0) {
                transform.Position = new Vector2(sr.Width * sr.Pivot.X, transform.Position.Y);
            } else if (sr.BottomRight.X > Game.Win.Width) {
                transform.Position = new Vector2(Game.Win.Width - (sr.Width * (1 - sr.Pivot.X)), transform.Position.Y);
            }
            if (sr.TopLeft.Y < 0) {
                transform.Position = new Vector2(transform.Position.X, sr.Height * sr.Pivot.Y);
            } else if (sr.BottomRight.Y > Game.Win.Height) {
                transform.Position = new Vector2(transform.Position.X, Game.Win.Height - (sr.Height * (1 - sr.Pivot.Y)));
            }
        }
    }
}
