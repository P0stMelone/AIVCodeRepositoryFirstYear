using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter_NoComponent {
    public class TitleScene : Scene {

        public override void Start() {
            base.Start();
            new StaticGameObject("titleBackground");
        }

        protected override void LoadAssets() {
            GfxMgr.AddTexture("titleBackground", "Assets/aivBG.png");
        }

        public override void GameLoop() {
            base.GameLoop();
            if (Game.Win.GetKey(Aiv.Fast2D.KeyCode.Esc)) {
                IsPlaying = false;
                nextScene = null;
            } else if (Game.Win.GetKey(Aiv.Fast2D.KeyCode.Return)) {
                IsPlaying = false;
                nextScene = new PlayScene();
            }
        }

    }
}
