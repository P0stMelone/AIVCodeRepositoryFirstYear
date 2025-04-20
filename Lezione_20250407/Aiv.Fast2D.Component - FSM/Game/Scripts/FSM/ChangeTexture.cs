using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component {
    public class ChangeTexture : Action {


        private SpriteRenderer sr;
        private Texture textureToSet;
        private bool everyFrame;

        public ChangeTexture (SpriteRenderer sr, Texture textureToSet, bool everyFrame) {
            this.sr = sr;
            this.textureToSet = textureToSet;
            this.everyFrame = everyFrame;
        }

        public override void OnEnter() {
            InternalChangeTexture();
        }

        public override void OnUpdate() {
            if (!everyFrame) return;
            InternalChangeTexture();
        }

        private void InternalChangeTexture () {
            sr.Texture = textureToSet;
        }

    }
}
