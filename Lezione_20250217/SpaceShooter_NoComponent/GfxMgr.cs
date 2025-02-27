using Aiv.Fast2D;
using System.Collections.Generic;

namespace SpaceShooter_NoComponent {
    public static class GfxMgr {

        private static Dictionary<string, Texture> textures;


        static GfxMgr () {
            textures = new Dictionary<string, Texture>();
        }

        public static Texture AddTexture (string name, string path) {
            if (textures.ContainsKey(name)) return textures[name];
            Texture texture = new Texture(path);
            textures.Add(name, texture);
            return texture;
        }

        public static void RemoveTexture(string name) {
            if (!textures.ContainsKey(name)) return;
            textures.Remove(name);
        }

        public static void ClearAll () {
            textures.Clear();
        }

        public static Texture GetTexture(string name) {
            return textures[name];
        }

    }
}
