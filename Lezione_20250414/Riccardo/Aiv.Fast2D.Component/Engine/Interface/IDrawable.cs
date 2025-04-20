namespace Aiv.Fast2D.Component {
    internal interface IDrawable {

        DrawLayer Layer { get;  }
        bool Enabled { get; }
        void Draw();

    }
}