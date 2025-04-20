using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class TestListener : UserComponent {


        private AudioSource audioSource;

        public TestListener(GameObject owner) : base(owner) {

        }


        public override void Awake () {
            audioSource = GetComponent<AudioSource>();
            audioSource.Volume = 1;
        }

        public override void OnEnable() {
            EventManager.AddListener(EventName.playerDeath, OnPlayerDeath);
        }

        public override void OnDisable() {
            EventManager.RemoveListener (EventName.playerDeath, OnPlayerDeath);
        }

        private void OnPlayerDeath (EventArgs message) {
            EventArgsFactory.PlayerDeathParser(message, out Vector2 deathPosition, out GameObject killer);
            Console.WriteLine("Il Player è morto in: " + deathPosition + " per colpa di " + killer.Name);
            audioSource.PlayOneShot(AudioMgr.GetClip("PlayerDeath"));
        }

    }
}
