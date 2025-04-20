using System;

namespace Aiv.Fast2D.Component {
    public class TestController : UserComponent {

        GameObject GO1;
        GameObject GO2;
        TestComponent GO1_TC;
        TestComponent GO2_TC;

        float counter = 0.5f;


        public TestController(GameObject owner) : base (owner) {

        }

        public override void Awake() {
            GO1 = GameObject.Find("GO1");
            GO2 = GameObject.Find("GO2");
            GO1_TC = GO1.GetComponent<TestComponent>();
            GO2_TC = GO2.GetComponent<TestComponent>();
        }

        public override void Update() {
            counter -= Game.DeltaTime;
            if (counter > 0) return;
            if (Game.Win.GetKey(KeyCode.A)) {
                Console.WriteLine("Invertito valore di isactive GO1");
                GO1.IsActive = !GO1.IsActive;
                counter = 0.5f;
            }
            if (Game.Win.GetKey(KeyCode.S)) {
                Console.WriteLine("Invertito valore di isactive GO2");
                GO2.IsActive = !GO2.IsActive;
                counter = 0.5f;
            }
            if (Game.Win.GetKey(KeyCode.D)) {
                Console.WriteLine("Invertito valore di enable GO1_TC");
                GO1_TC.Enabled = !GO1_TC.EnabledSelf;
                counter = 0.5f;
            }
            if (Game.Win.GetKey(KeyCode.F)) {
                Console.WriteLine("Invertito valore di enable GO2_TC");
                GO2_TC.Enabled = !GO2_TC.EnabledSelf;
                counter = 0.5f;
            }
            if (Game.Win.GetKey(KeyCode.Space)) {
                Game.TriggerChangeScene(new PlayScene());
            }

        }

    }
}
