using OpenTK;

namespace Aiv.Fast2D.Component {
    class Program {
        static void Main(string[] args) {
            Game.Init(1280, 720, "SpaceShooter", new PlayScene());
            Game.Play();
        }
    }
}
