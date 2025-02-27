using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter_NoComponent {
    public static class UpdateMgr {

        static List<IUpdatable> items;

        static UpdateMgr () {
            items = new List<IUpdatable>();
        }

        public static void AddItem (IUpdatable item) {
            if (items.Contains(item)) return;
            items.Add(item);
        }

        public static void RemoveItem (IUpdatable item) {
            if (!items.Contains(item)) return;
            items.Remove(item);
        }

        public static void ClearAll () {
            items.Clear();
        }

        public static void Update () {
            foreach (IUpdatable item in items) {
                if (!item.IsActive) continue;
                item.Update();
            }

        }


        public static void LateUpdate() {
            foreach(IUpdatable item in items) {
                if (!item.IsActive) continue;
                item.LateUpdate();
            }
        }

    }
}
