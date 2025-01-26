namespace SquareInvaders {
    public class Pixel {

        Vector2 position;
        Vector2 velocity;
        int width;
        Color color;
        bool isGravityAffected;

        public void SetIsGravity (bool value) {
            isGravityAffected = value;
        }

        public Pixel (Vector2 pos, int w, Color c) {
            position = pos;
            width = w;
            color = c;
        }

        public Vector2 GetPosition () {
            return position;
        }

        public void SetPosition (Vector2 position) {
            this.position = position;
        }

        public Vector2 GetVelocity () {
            return velocity;
        }

        public void SetVelocity (Vector2 velocity) {
            this.velocity = velocity;
        }

        public void Update () {
            if (isGravityAffected) {
                velocity.Y += Game.gravity * GfxTools.Win.DeltaTime;
            }
            position += velocity * GfxTools.Win.DeltaTime;
        }

        public bool GetIsVisible () {
            return position.X > -width && position.X < GfxTools.Win.Width 
                && position.Y < GfxTools.Win.Height;
        }

        public void DrawPixel () {
            GfxTools.DrawRect((int)position.X, 
                (int)position.Y, width, width, color.R, color.G, color.B);
        }

        public void Translate (Vector2 transVec) {
            position.X += transVec.X;
            position.Y += transVec.Y;
        }

    }
}
