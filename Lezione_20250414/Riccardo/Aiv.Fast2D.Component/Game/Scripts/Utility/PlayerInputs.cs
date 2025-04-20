namespace Aiv.Fast2D.Component {
    public static class PlayerInputs {
        public static void GamePlayInputMapping() {
            if (GameData.PlayerCount == 1) {
                Input.AddInputActionAxis("Horizontal1",
                    new AxisMatch[] {
                    new KeyAxisMatch(KeyCode.A, KeyCode.D),
                    new KeyAxisMatch(KeyCode.Left, KeyCode.Right),
                    new JoystickAxisMatch(JoystickAxis.leftStick_Horizontal, 0)
                    });
                Input.AddInputActionAxis("Vertical1",
                    new AxisMatch[] {
                    new KeyAxisMatch(KeyCode.W, KeyCode.S),
                    new KeyAxisMatch(KeyCode.Up, KeyCode.Down),
                    new JoystickAxisMatch(JoystickAxis.leftStick_Vertical, 0)
                    });
                Input.AddInputActionButton("Shoot1",
                    new ButtonMatch[] {
                                new KeyButtonMatch(KeyCode.Space),
                                new MouseButtonMatch(MouseButton.leftMouse),
                                new JoystickButtonMatch(JoystickButton.buttonA, 0)
                    });
            }
            else {
                //p1
                Input.AddInputActionAxis("Horizontal1",
                new AxisMatch[] {
                    new KeyAxisMatch(KeyCode.A, KeyCode.D),
                    new JoystickAxisMatch(JoystickAxis.leftStick_Horizontal, 0)
                });
                Input.AddInputActionAxis("Vertical1",
                    new AxisMatch[] {
                    new KeyAxisMatch(KeyCode.W, KeyCode.S),
                    new JoystickAxisMatch(JoystickAxis.leftStick_Vertical, 0)
                    });
                Input.AddInputActionButton("Shoot1",
                    new ButtonMatch[] {
                                new KeyButtonMatch(KeyCode.Space),
                                new JoystickButtonMatch(JoystickButton.buttonA, 0)
                    });
                //p2
                Input.AddInputActionAxis("Horizontal2",
                    new AxisMatch[] {
                    new KeyAxisMatch(KeyCode.Left, KeyCode.Right),
                    new JoystickAxisMatch(JoystickAxis.leftStick_Horizontal, 1)
                    });
                Input.AddInputActionAxis("Vertical2",
                    new AxisMatch[] {
                    new KeyAxisMatch(KeyCode.Up, KeyCode.Down),
                    new JoystickAxisMatch(JoystickAxis.leftStick_Vertical, 1)
                    });
                Input.AddInputActionButton("Shoot2",
                    new ButtonMatch[] {
                                new KeyButtonMatch(KeyCode.CtrlRight),
                                new JoystickButtonMatch(JoystickButton.buttonA, 1)
                    });
            }
        }

    }
}
