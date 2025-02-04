using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter_NoComponent {
    public static class PhysicsMgr {

        static List<Rigidbody> items;

        static PhysicsMgr () {
            items = new List<Rigidbody>();
        }

        public static void AddItem (Rigidbody item) {
            if (items.Contains(item)) return;
            items.Add(item);
        }

        public static void RemoveItem (Rigidbody item) {
            if (!items.Contains(item)) return;
            items.Remove(item);
        }

        public static void FixedUpdate () {
            for (int i = 0; i < items.Count; i++) {
                if (!items[i].IsActive) continue;
                items[i].FixedUpdate();
            }
        }

        public static void ClearAll () {
            items.Clear();
        }

    }
}
