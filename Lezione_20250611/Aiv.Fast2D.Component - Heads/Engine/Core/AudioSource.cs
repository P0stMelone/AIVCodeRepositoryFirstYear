using Aiv.Audio;

using AS = Aiv.Audio.AudioSource;

namespace Aiv.Fast2D.Component {

    public enum AudioSourceStatus {
        Play,
        Stop,
        Pause
    }

    public class AudioSource : Component {

        private AudioSourceStatus status;
        private AS internalAudioSource;
        private AudioClip clip;

        private float volume;
        public float Volume {
            get { return volume; }
            set {
                if (volume == value) return;
                volume = value;
                internalAudioSource.Volume = volume * AudioMgr.GetVolume(layer);
            }
        }
        public bool Loop {
            get;
            set;
        }


        private AudioLayer layer;
        public AudioLayer Layer {
            get { return layer; }
            set {
                if (layer == value) return;
                layer = value;
                internalAudioSource.Volume = volume * AudioMgr.GetVolume(layer);
            }
        }

        public void PlayOneShot(AudioClip clipToPlay) {
            AudioMgr.PlayOneShot(clipToPlay, Volume);
        }

        public AudioSource (GameObject owner) : base (owner) {
            status = AudioSourceStatus.Stop;
            internalAudioSource = new AS();
        }

        public void SetClip (AudioClip clip) {
            this.clip = clip;
        }

        public void Play () {
            if (clip == null) return;
            if (status == AudioSourceStatus.Play) return;
            switch(status) {
                case AudioSourceStatus.Pause:
                    internalAudioSource.Resume();
                    break;
                case AudioSourceStatus.Stop:
                    internalAudioSource.Play(clip, Loop);
                    break;
            }
            status = AudioSourceStatus.Play;
        }

        public void Pause () {
            if (clip == null) return;
            if (status == AudioSourceStatus.Pause) return;
            if (status == AudioSourceStatus.Stop) return;
            internalAudioSource.Pause();
            status = AudioSourceStatus.Pause;
        }

        public void Stop () {
            if (clip == null) return;
            if (status == AudioSourceStatus.Stop) return;
            internalAudioSource.Stop();
            status = AudioSourceStatus.Stop;
        }

    }
}
