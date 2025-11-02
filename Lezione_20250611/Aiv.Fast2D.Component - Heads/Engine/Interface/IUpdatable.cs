namespace Aiv.Fast2D.Component {
    internal interface IUpdatable {

        bool Enabled { get; }
        void Update();
        void LateUpdate();

    }
}