using System.Collections.Generic;

namespace Aiv.Fast2D.Component {

    public enum DrawLayer {
        Background,
        Middleground,
        Playground,
        Foreground, 
        GUI,
        Last
    }

    internal static class DrawMgr {


        private static List<IDrawable>[] items;

        static DrawMgr() {
            items = new List<IDrawable>[(int)DrawLayer.Last];
            for (int i = 0; i < items.Length; i++) {
                items[i] = new List<IDrawable>();
            }
        }

        internal static void AddItem (IDrawable item) {
            items[(int)item.Layer].Add(item);
        }

        internal static void RemoveItem (IDrawable item) {
            if (!items[(int)item.Layer].Contains(item)) return;
            items[(int)item.Layer].Remove(item);
        }

        internal static void Draw () {
            for (int i = 0; i < items.Length; i++) {
                foreach(IDrawable drawable in items[i]) {
                    if (!drawable.Enabled) continue;
                    drawable.Draw();
                }
            }
        }

        internal static void ClearAll () {
            for (int i = 0; i < items.Length; i++) {
                items[i].Clear();
            }
        }

    }
}
