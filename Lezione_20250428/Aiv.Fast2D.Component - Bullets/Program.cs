using OpenTK;

namespace Aiv.Fast2D.Component {

    public enum Layer {
        Player =1,
        PlayerBullet = 2,
        Enemy = 4,
        EnemyBullet = 8,
        PowerUp = 16
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

            Input.AddInputActionAxis("Horizontal",
                new AxisMatch[] {
                    new KeyAxisMatch(KeyCode.A, KeyCode.D),
                    new KeyAxisMatch(KeyCode.Left, KeyCode.Right),
                    new JoystickAxisMatch(JoystickAxis.leftStick_Horizontal, 0)
                });
            Input.AddInputActionAxis("Vertical",
                new AxisMatch[] {
                    new KeyAxisMatch(KeyCode.W, KeyCode.S),
                    new KeyAxisMatch(KeyCode.Up, KeyCode.Down),
                    new JoystickAxisMatch(JoystickAxis.leftStick_Vertical, 0)
                });
            Input.AddInputActionButton("Shoot",
                new ButtonMatch[] {
                                new KeyButtonMatch(KeyCode.Space),
                                new MouseButtonMatch(MouseButton.leftMouse),
                                new JoystickButtonMatch(JoystickButton.buttonA, 0)
                });

            Game.Init(1280, 720, "SpaceShooter", new PlayScene(), 720, 10);
            Game.Play();
        }
    }
}
