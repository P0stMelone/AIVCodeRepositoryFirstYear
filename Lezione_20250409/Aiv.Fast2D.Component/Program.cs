using OpenTK;

namespace Aiv.Fast2D.Component {

    public enum Layer {
        Player = 1,
        PlayerBullet = 2,
        Enemy = 4,
        EnemyBullet = 8,
        PowerUp = 16,
        Asteroid = 32
    }

    class Program {

        static void Main(string[] args) {
            
            #region MenuCommandsMapping
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
            Input.AddInputActionButton("MoveUp",
                new ButtonMatch[] {
                                new KeyButtonMatch(KeyCode.W),
                                new KeyButtonMatch(KeyCode.Up)
                });
            Input.AddInputActionButton("MoveDown",
                new ButtonMatch[] {
                                new KeyButtonMatch(KeyCode.S),
                                new KeyButtonMatch(KeyCode.Down)
                });
            Input.AddInputActionButton("MoveLeft",
                new ButtonMatch[] {
                                new KeyButtonMatch(KeyCode.A),
                                new KeyButtonMatch(KeyCode.Left)
                });
            Input.AddInputActionButton("MoveRight",
                new ButtonMatch[] {
                                new KeyButtonMatch(KeyCode.D),
                                new KeyButtonMatch(KeyCode.Right)
                });
            #endregion

            Game.Init(1280, 720, "SpaceShooter", new MainMenuScene());
            Game.Play();
        }
    }
}
