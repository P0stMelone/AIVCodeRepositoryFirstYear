using Aiv.Audio;
using System;
using System.Collections.Generic;

using AS = Aiv.Audio.AudioSource;

namespace Aiv.Fast2D.Component {
    public enum AudioLayer {
        music,
        sfx
    }


    public static class AudioMgr {

        private const int maxPlayOneShotSource = 20;
        private static AS[] oneShotPool;
        private static float[] volumes;
        private static Dictionary<string, AudioClip> clips;

        static AudioMgr () {
            clips = new Dictionary<string, AudioClip>();
            volumes = new float[Enum.GetValues(typeof(AudioLayer)).Length];
            for(int i = 0; i < volumes.Length; i++) {
                volumes[i] = 1f;
            }
            oneShotPool = new AS[5];
            for (int i = 0; i < oneShotPool.Length; i++) {
                oneShotPool[i] = new AS();
            }
        }

        public static void AddClip (string name, string path) {
            AudioClip clip = new AudioClip(path);
            clips.Add(name, clip);
        }

        public static AudioClip GetClip (string name) {
            return clips[name];
        }

        public static void ClearAll () {
            clips.Clear();
        }

        public static void SetVolume (AudioLayer layer, float value) {
            volumes[(int)layer] = value;
        }

        public static float GetVolume (AudioLayer layer) {
            return volumes[(int)layer];
        }

        public static void PlayOneShot(AudioClip clip, float volume) {
            for (int i = 0; i < oneShotPool.Length; i++) {
                if (oneShotPool[i].IsPlaying) continue;
                oneShotPool[i].Volume = volume;
                oneShotPool[i].Play(clip);
                return;
            }
            if (oneShotPool.Length >= maxPlayOneShotSource) return;
            int newLength = oneShotPool.Length * 2;
            newLength = newLength > maxPlayOneShotSource ? maxPlayOneShotSource : newLength;
            AS[] tempAudioSources = new AS[newLength];
            int j = 0;
            for (; j < oneShotPool.Length; j++) {
                tempAudioSources[j] = oneShotPool[j];
            }
            for (; j < tempAudioSources.Length; j++) {
                tempAudioSources[j] = new AS();
            }
            oneShotPool = tempAudioSources;
            PlayOneShot(clip, volume);
        }
    }
}
