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
            Game.Init(1280, 720, "SpaceShooter", new PlayScene());
            Game.Play();
        }
    }
}
