namespace Aiv.Fast2D.Component {
    internal interface ICollidable {

        bool Enabled { get; }
        void OnCollideEnter(Collision collisionInfo);
        void OnCollide(Collision collisionInfo);
        void OnCollideExit(Collision collisionInfo);
    }
}