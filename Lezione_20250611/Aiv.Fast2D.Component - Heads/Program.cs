namespace Aiv.Fast2D.Component {

    public enum Layer {
        Player =1,
        PlayerBullet = 2,
        Enemy = 4,
        EnemyBullet = 8,
        Tile = 16
    }

    class Program {

        static void Main(string[] args) {

            Input.AddInputActionButton("Confirm",
                new ButtonMatch[] {
                    new KeyButtonMatch(KeyCode.Return),
                    new KeyButtonMatch(KeyCode.Y),
                    new JoystickButtonMatch(JoystickButton.start, 0)
                });
            Input.AddInputActionButton("Cancel",
                new ButtonMatch[] {
                    new KeyButtonMatch(KeyCode.Esc),
                    new KeyButtonMatch(KeyCode.N),
                    new JoystickButtonMatch(JoystickButton.buttonB, 0)
                });

            Input.AddInputActionAxis("P1_Horizontal",
                new AxisMatch[] {
                    new KeyAxisMatch(KeyCode.A, KeyCode.D),
                    new JoystickAxisMatch(JoystickAxis.leftStick_Horizontal, 0)
                });
            Input.AddInputActionAxis("P1_Vertical",
                new AxisMatch[] {
                                new KeyAxisMatch(KeyCode.W, KeyCode.S),
                                new JoystickAxisMatch(JoystickAxis.leftStick_Horizontal, 0)
                });
            Input.AddInputActionButton("P1_Shoot",
                new ButtonMatch[] {
                    new KeyButtonMatch(KeyCode.Space),
                    new JoystickButtonMatch(JoystickButton.buttonA, 0)
                });

            Input.AddInputActionAxis("P2_Horizontal",
                new AxisMatch[] {
                                new KeyAxisMatch(KeyCode.Left, KeyCode.Right),
                                new JoystickAxisMatch(JoystickAxis.leftStick_Horizontal, 1)
                });
            Input.AddInputActionAxis("P2_Vertical",
                new AxisMatch[] {
                                new KeyAxisMatch(KeyCode.Up, KeyCode.Down),
                                new JoystickAxisMatch(JoystickAxis.leftStick_Horizontal, 1)
                });
            Input.AddInputActionButton("P2_Shoot",
                new ButtonMatch[] {
                    new KeyButtonMatch(KeyCode.CtrlRight),
                    new JoystickButtonMatch(JoystickButton.buttonA, 1)
                });
            Game.Init(1280, 720, "SpaceShooter", new PlayScene(), 1080, 10);
            Game.Play();
        }
    }
}
