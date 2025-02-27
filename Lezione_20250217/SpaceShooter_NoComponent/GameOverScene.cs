using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter_NoComponent {
    public class GameOverScene : Scene {

        public override void Start() {
            base.Start();
            new StaticGameObject("gameOverBG");
        }

        protected override void LoadAssets() {
            GfxMgr.AddTexture("gameOverBG", "Assets/gameOverBG.png");
        }

        public override void GameLoop() {
            base.GameLoop();
            if (Game.Win.GetKey(Aiv.Fast2D.KeyCode.Y)) {
                IsPlaying = false;
                nextScene = new PlayScene();
            } else if (Game.Win.GetKey(Aiv.Fast2D.KeyCode.N)) {
                IsPlaying = false;
                nextScene = new TitleScene ();
            }
        }

    }
}
