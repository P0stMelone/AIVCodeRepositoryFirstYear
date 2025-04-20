using System;

namespace Aiv.Fast2D.Component {
    public class TestComponent : UserComponent {

        public TestComponent (GameObject owner) : base (owner) {
            Console.WriteLine("Creato componente su gameobject: " + owner.Name);
        }

        public override void Awake() {
            Console.WriteLine("Chiamata Awake sul component di: " + gameObject.Name);
            base.Awake();
        }

        public override void OnEnable() {
            Console.WriteLine("Chiamata OnEnable sul component di: " + gameObject.Name);
        }

        public override void OnDisable() {
            Console.WriteLine("Chiamata OnDisable sul component di: " + gameObject.Name);
        }

        public override void Start() {
            Console.WriteLine("Chiamata Start sul component di: " + gameObject.Name);
        }

        public override void OnDestroy() {
            Console.WriteLine("Chiamata OnDestroy sul component di: " + gameObject.Name);
        }

    }
}
