using System;

namespace Aiv.Fast2D.Component {
    public class TestInputSystemComponent : UserComponent {

        public TestInputSystemComponent(GameObject owner) : base(owner) {

        }


        public override void Update() {
            //TestInputSystemButton();
            TestInputSystemActions();
        }

        private void TestInputSystemButton () {
            if (Input.GetKeyDown(KeyCode.A)) {
                Console.WriteLine("Premuto A");
            }
            if (Input.GetKeyUp(KeyCode.A)) {
                Console.WriteLine("Rilasciato A");
            }
            if (Input.GetMouseButtonDown(MouseButton.leftMouse)) {
                Console.WriteLine("Premuto left mouse button");
            }
            if (Input.GetMouseButtonUp(MouseButton.leftMouse)) {
                Console.WriteLine("Rilasciato left mouse button");
            }
            if (Input.GetJoystickButtonDown(JoystickButton.buttonA, 0)) {
                Console.WriteLine("Premuto joystick button A");
            }
            if (Input.GetJoystickButtonUp(JoystickButton.buttonA, 0)) {
                Console.WriteLine("Rilasciato joystick button A");
            }
            //Console.WriteLine("Valore di leftStickHorizontal: " + 
            //    Input.GetJoystickAxis(JoystickAxis.leftStick_Horizontal, 0));
        }

        private void TestInputSystemActions () {
            if (Input.GetInputActionButtonDown("Jump")) {
                Console.WriteLine("Premuta azione Jump");
            }
            if (Input.GetInputActionButtonUp("Jump")) {
                Console.WriteLine("Rilasciata azione Jump");
            }
            Console.WriteLine("Asse horizontal: " + Input.GetAxis("Horizontal"));
            Console.WriteLine("Asse vertical: " + Input.GetAxis("Vertical"));
        }
    }
}
