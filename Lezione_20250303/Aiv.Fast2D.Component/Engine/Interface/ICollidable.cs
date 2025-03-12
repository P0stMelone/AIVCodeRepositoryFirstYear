namespace Aiv.Fast2D.Component {
    internal interface ICollidable {

        bool Enabled { get; }
        void OnCollide(GameObject other);

    }
}