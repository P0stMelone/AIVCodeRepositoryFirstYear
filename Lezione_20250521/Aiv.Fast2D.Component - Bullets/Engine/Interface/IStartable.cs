namespace Aiv.Fast2D.Component {
    internal interface IStartable {

        bool Enabled { get; }
        void Awake();
        void Start();
        void OnEnable();
        void OnDisable();
        void OnDestroy();

    }
}
