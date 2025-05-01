using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    internal static class GfxMgr {

        private static Dictionary<string, Texture> textures;

        static GfxMgr () {
            textures = new Dictionary<string, Texture>();
        }

        internal static Texture AddTexture (string name, string path) {
            if (textures.ContainsKey(name)) return textures[name];
            Texture texture = new Texture(path);
            textures.Add(name, texture);
            return texture;
        }

        internal static Texture GetTexture (string name) {
            if (!textures.ContainsKey(name)) return null;
            return textures[name];
        }

        internal static void ClearAll () {
            textures.Clear();
        }

    }
}
