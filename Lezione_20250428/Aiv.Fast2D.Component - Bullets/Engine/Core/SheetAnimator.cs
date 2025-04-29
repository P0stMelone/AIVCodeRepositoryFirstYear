using System.Collections.Generic;
using OpenTK;

namespace Aiv.Fast2D.Component {
    public class SheetAnimator : UserComponent {

        private SpriteRenderer spriteRenderer;
        private List<SheetClip> clips;
        private SheetClip currentClip;

        private float sliceTime;
        private float currentSliceTime;
        private int currentIndexOfClipFrames;

        public SheetAnimator(GameObject owner, SpriteRenderer spriteRender) : base(owner) {
            this.spriteRenderer = spriteRender;
            clips = new List<SheetClip>();
        }

        public void AddClip(SheetClip clip) {
            clips.Add(clip);
        }

        public override void Start() {
            ChangeClip(clips[0].AnimationName);
        }

        public void ChangeClip(string name) {
            for (int i = 0; i < clips.Count; i++) {
                if (!(clips[i].AnimationName == name)) continue;
                currentClip = clips[i];
                sliceTime = 1f / currentClip.FPS;
                currentSliceTime = sliceTime;
                spriteRenderer.Texture = currentClip.Texture;
                currentIndexOfClipFrames = 0;
                SetNewFrame(currentClip.Frames[currentIndexOfClipFrames]);
                break;
            }
        }

        private void SetNewFrame(int index) {
            int rowIndex = index / currentClip.NumberOfColumn;
            int columnIndex = index % currentClip.NumberOfColumn;
            spriteRenderer.TextureOffset = new Vector2(currentClip.FrameWidth * columnIndex,
                currentClip.FrameHeight * rowIndex);
        }

        public override void LateUpdate() {
            currentSliceTime -= Game.DeltaTime;
            if (currentSliceTime > 0) return;
            currentIndexOfClipFrames++;
            if (currentIndexOfClipFrames < currentClip.Frames.Length) {
                currentSliceTime = sliceTime;
                SetNewFrame(currentClip.Frames[currentIndexOfClipFrames]);
                return;
            }
            if (currentClip.Loop) {
                currentSliceTime = sliceTime;
                currentIndexOfClipFrames = 0;
                SetNewFrame(currentClip.Frames[currentIndexOfClipFrames]);
                return;
            }
            if (!string.IsNullOrEmpty(currentClip.NextAnimation)) {
                ChangeClip(currentClip.NextAnimation);
            }
        }

        public string GetCurrentAnimationName () {
            if (currentClip == null) return string.Empty;
            return currentClip.AnimationName;
        }

    }

}
