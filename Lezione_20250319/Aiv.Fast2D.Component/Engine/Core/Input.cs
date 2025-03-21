using System;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {

    public enum MouseButton {
        leftMouse,
        rightMouse,
        mouseButton1,
        mouseButton2,
        mouseButton3,
        mouseButton4,
        mouseButton5,
        mouseButton6,
        mouseButton7,
        mouseButton8,
        mouseButton9
    }

    public enum JoystickButton {
        buttonA,
        buttonB,
        buttonX,
        buttonY,
        buttonLeft,
        buttonRight,
        buttonUp,
        buttonDown,
        shoulderLeft,
        shoulderRight,
        start,
        back,
        bigButton,
        none
    }

    public enum JoystickAxis {
        leftStick_Horizontal,
        leftStick_Vertical,
        rightStick_Horizontal,
        rightStick_Vertical,
        sholuderTriggerLeft,
        shoulderTriggerRight
    }

    public static class Input {

        private static Array keyCodeValues;
        private static Array mouseButtonValues;
        private static Array joystickButtonValues;

        private static Dictionary<KeyCode, bool> lastKeyValue;
        private static Dictionary<MouseButton, bool> lastMouseValue;
        private static Dictionary<JoystickButton, bool>[] lastJoystickValue;
        private static Dictionary<string, InputAction_Button> inputActionsButton;

        static Input() {
            inputActionsButton = new Dictionary<string, InputAction_Button>();
            keyCodeValues = Enum.GetValues(typeof(KeyCode));
            mouseButtonValues = Enum.GetValues(typeof(MouseButton));
            joystickButtonValues = Enum.GetValues(typeof(JoystickButton));
            lastKeyValue = new Dictionary<KeyCode, bool>();
            lastMouseValue = new Dictionary<MouseButton, bool>();
            lastJoystickValue = new Dictionary<JoystickButton, bool>[Window.Joysticks.Length];
            for (int i = 0; i < lastJoystickValue.Length;i++) {
                lastJoystickValue[i] = new Dictionary<JoystickButton, bool>();
            }
            foreach(KeyCode key in keyCodeValues) {
                lastKeyValue.Add(key, false);
            }
            foreach(MouseButton mb in mouseButtonValues) {
                lastMouseValue.Add(mb, false);
            }
            for (int i = 0; i < lastJoystickValue.Length; i++) {
                foreach(JoystickButton jb in joystickButtonValues) {
                    lastJoystickValue[i].Add(jb, false);
                }
            }
        }

        public static void AddInputActionButton (string name, ButtonMatch[] bindedButtons) {
            inputActionsButton.Add(name, new InputAction_Button(bindedButtons));
        }

        public static void PerformLastKey () {
            foreach(KeyCode key in keyCodeValues) {
                lastKeyValue[key] = Game.Win.GetKey(key);
            }
            foreach(MouseButton mb in mouseButtonValues) {
                lastMouseValue[mb] = FromMouseButtonToBool(mb);
            }
            for (int i = 0; i < lastJoystickValue.Length; i++) {
                foreach(JoystickButton jb in joystickButtonValues) {
                    lastJoystickValue[i][jb] = FromJoystickButtonToBool(jb, i);
                }
            }
        }

        public static bool GetKeyDown (KeyCode key) {
            return !lastKeyValue[key] && Game.Win.GetKey(key);
        }

        public static bool GetKey (KeyCode key) {
            return Game.Win.GetKey(key);
        }

        public static bool GetKeyUp (KeyCode key) {
            return lastKeyValue[key] && !Game.Win.GetKey(key);
        }

        public static bool GetMouseButtonDown (MouseButton mb) {
            return !lastMouseValue[mb] && FromMouseButtonToBool(mb);
        }

        public static bool GetMouseButton (MouseButton mb) {
            return FromMouseButtonToBool(mb);
        }

        public static bool GetMouseButtonUp (MouseButton mb) {
            return lastMouseValue[mb] && !FromMouseButtonToBool(mb);
        }

        public static bool GetJoystickButtonDown (JoystickButton jb, int index) {
            return !lastJoystickValue[index][jb] && FromJoystickButtonToBool(jb, index);
        }

        public static float GetJoystickAxis (JoystickAxis joystickAxis, int index) {
            switch(joystickAxis) {
                case JoystickAxis.leftStick_Horizontal:
                    return Game.Win.JoystickAxisLeft(index).X;
                case JoystickAxis.leftStick_Vertical:
                    return Game.Win.JoystickAxisLeft(index).Y;
                case JoystickAxis.rightStick_Horizontal:
                    return Game.Win.JoystickAxisRight(index).X;
                case JoystickAxis.rightStick_Vertical:
                    return Game.Win.JoystickAxisRight(index).Y;
                case JoystickAxis.sholuderTriggerLeft:
                    return Game.Win.JoystickTriggerLeft(index);
                case JoystickAxis.shoulderTriggerRight:
                    return Game.Win.JoystickTriggerRight(index);
                default:
                    return 0;
            }
        }

        public static bool GetJoystickButton (JoystickButton jb, int index) {
            return FromJoystickButtonToBool(jb, index);
        }

        public static bool GetJoystickButtonUp (JoystickButton jb, int index) {
            return lastJoystickValue[index][jb] && !FromJoystickButtonToBool(jb, index);
        }

        public static bool GetInputActionButtonDown (string name) {
            return inputActionsButton[name].GetButtonDown();
        }

        public static bool GetInputActionButton(string name) {
            return inputActionsButton[name].GetButton();
        }

        public static bool GetInputActionButtonUp (string name) {
            return inputActionsButton[name].GetButtonUp();
        }

        #region Internal
        private static bool FromMouseButtonToBool (MouseButton mouseButton) {
            switch(mouseButton) {
                case MouseButton.leftMouse:
                    return Game.Win.MouseLeft;
                case MouseButton.rightMouse:
                    return Game.Win.MouseRight;
                case MouseButton.mouseButton1:
                    return Game.Win.MouseButton1;
                case MouseButton.mouseButton2:
                    return Game.Win.MouseButton2;
                case MouseButton.mouseButton3:
                    return Game.Win.MouseButton3;
                case MouseButton.mouseButton4:
                    return Game.Win.MouseButton4;
                case MouseButton.mouseButton5:
                    return Game.Win.MouseButton5;
                case MouseButton.mouseButton6:
                    return Game.Win.MouseButton6;
                case MouseButton.mouseButton7:
                    return Game.Win.MouseButton7;
                case MouseButton.mouseButton8:
                    return Game.Win.MouseButton8;
                case MouseButton.mouseButton9:
                    return Game.Win.MouseButton9;
                default:
                    return false;
            }
        }

        private static bool FromJoystickButtonToBool (JoystickButton jb, int index) {
            switch (jb) {
                case JoystickButton.buttonA:
                    return Game.Win.JoystickA(index);
                case JoystickButton.buttonB:
                    return Game.Win.JoystickB(index);
                case JoystickButton.buttonX:
                    return Game.Win.JoystickX(index);
                case JoystickButton.buttonY:
                    return Game.Win.JoystickY(index);
                case JoystickButton.buttonUp:
                    return Game.Win.JoystickUp(index);
                case JoystickButton.buttonDown:
                    return Game.Win.JoystickDown(index);
                case JoystickButton.buttonLeft:
                    return Game.Win.JoystickLeft(index);
                case JoystickButton.buttonRight:
                    return Game.Win.JoystickRight(index);
                case JoystickButton.shoulderLeft:
                    return Game.Win.JoystickShoulderLeft(index);
                case JoystickButton.shoulderRight:
                    return Game.Win.JoystickShoulderRight(index);
                case JoystickButton.start:
                    return Game.Win.JoystickStart(index);
                case JoystickButton.back:
                    return Game.Win.JoystickBack(index);
                case JoystickButton.bigButton:
                    return Game.Win.JoystickBigButton(index);
                default:
                    return false;
            }
        }
        #endregion

    }

    public class InputAction_Button {

        //array di tutti i bottoni fisici a cui è mappata questa azione
        public ButtonMatch[] bindedButtons;

        public InputAction_Button(ButtonMatch[] bindedButtons) {
            this.bindedButtons = bindedButtons;
        }

        public bool GetButton () {
            foreach(ButtonMatch button in bindedButtons) {
                if (button.GetButton()) return true;
            }
            return false;
        }

        public bool GetButtonDown() {
            foreach (ButtonMatch button in bindedButtons) {
                if (button.GetButtonDown()) return true;
            }
            return false;
        }

        public bool GetButtonUp() {
            foreach (ButtonMatch button in bindedButtons) {
                if (button.GetButtonUp()) return true;
            }
            return false;
        }
    }


    public abstract class ButtonMatch {
        public abstract bool GetButtonDown();
        public abstract bool GetButton();
        public abstract bool GetButtonUp();
    }

    public class KeyButtonMatch : ButtonMatch {

        private KeyCode keyButton;

        public KeyButtonMatch(KeyCode keyButton) {
            this.keyButton = keyButton;
        }

        public override bool GetButton() {
            return Input.GetKey(keyButton);
        }

        public override bool GetButtonDown() {
            return Input.GetKeyDown(keyButton);
        }

        public override bool GetButtonUp() {
            return Input.GetKeyUp(keyButton);
        }

    }

    public class MouseButtonMatch : ButtonMatch {
        private MouseButton mouseButton;

        public MouseButtonMatch(MouseButton mouseButton) {
            this.mouseButton = mouseButton;
        }

        public override bool GetButton() {
            return Input.GetMouseButton(mouseButton);
        }

        public override bool GetButtonDown() {
            return Input.GetMouseButtonDown(mouseButton);
        }

        public override bool GetButtonUp() {
            return Input.GetMouseButtonUp(mouseButton);
        }
    }

    public class JoystickButtonMatch : ButtonMatch {
        private JoystickButton joystickButton;
        private int index;

        public JoystickButtonMatch(JoystickButton joystickButton, int index) {
            this.joystickButton = joystickButton;
            this.index = index;
        }

        public override bool GetButton() {
            return Input.GetJoystickButton(joystickButton, index);
        }

        public override bool GetButtonDown() {
            return Input.GetJoystickButtonDown(joystickButton, index);
        }

        public override bool GetButtonUp() {
            return Input.GetJoystickButtonUp(joystickButton, index);
        }
    }

}
