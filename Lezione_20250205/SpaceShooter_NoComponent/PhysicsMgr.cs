using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter_NoComponent {

    public enum RigidbodyType {
        Player = 1, PlayerBullet = 2, Enemy = 4, EnemyBullet = 8, PowerUp = 16
    }

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

        public static void CheckCollisions() {
            for (int i = 0; i < items.Count - 1; i++) {
                if (!items[i].IsCollisionAffected || !items[i].IsActive) continue;
                for (int j = i + 1; j < items.Count; j++) {
                    if (!items[j].IsCollisionAffected || !items[j].IsActive) continue;
                    bool firstCheck = items[i].CollisionTypeMatches(items[j].Type);
                    bool secondCheck = items[j].CollisionTypeMatches(items[i].Type);
                    if (!firstCheck && !secondCheck) continue;
                    bool collided = items[i].Collides(items[j]);
                    if (!collided) continue;
                    if (firstCheck) items[i].GameObject.OnCollide(items[j].GameObject);
                    if (secondCheck) items[j].GameObject.OnCollide(items[i].GameObject);
                }
            }
        }

    }
}
