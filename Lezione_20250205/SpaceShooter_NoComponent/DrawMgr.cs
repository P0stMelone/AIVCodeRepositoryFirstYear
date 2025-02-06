using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter_NoComponent {
    public static class DrawMgr {

        static List<IDrawable> items;

        static DrawMgr() {
            items = new List<IDrawable>();
        }

        public static void AddItem (IDrawable item) {
            if (items.Contains(item)) return;
            items.Add(item);
        }

        public static void RemoveItem (IDrawable item) {
            if (!items.Contains(item)) return;
            items.Remove(item);
        }

        public static void ClearAll () {
            items.Clear();
        }

        public static void Draw () {
            foreach(IDrawable item in items) {
                if (!item.IsActive) continue;
                item.Draw();
            }
        }

    }
}
