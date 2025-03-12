namespace Aiv.Fast2D.Component {
    internal interface IFixedUpdatable {

        bool Enabled { get; }
        void FixedUpdate();

    }
}
