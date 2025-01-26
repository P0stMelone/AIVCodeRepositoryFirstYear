using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvaders {
    public class BulletPooler {

        private const int bulletWidth = 10;
        private const int bulletHeight = 20;

        public Bullet[] playerBullets;
        public Bullet[] enemiesBullets;

        public BulletPooler (int numPlayerBullet, int numEnemiesBullet) {
            playerBullets = new Bullet[numPlayerBullet];
            for (int i = 0; i < playerBullets.Length; i++) {
                playerBullets[i] = new Bullet(bulletWidth, bulletHeight, new Color(255, 0, 0));
            }
            enemiesBullets = new Bullet[numEnemiesBullet];
            for (int i = 0; i < enemiesBullets.Length; i++) {
                enemiesBullets[i] = new Bullet(bulletWidth, bulletHeight, new Color(0, 255, 0));
            }
        }

        public void Update () {
            InternalUpdate(playerBullets);
            InternalUpdate(enemiesBullets);
        }

        public void Draw () {
            InternalDraw(playerBullets);
            InternalDraw(enemiesBullets);
        }

        public void DetectCollision (Player player, Alien[] aliens) {
            DetectPlayerCollision(player);
            DetectEnemiesCollision(aliens);
        }

        private void DetectPlayerCollision (Player player) {
            Vector2 playerCenter = player.GetPosition();
            float playerRadius = player.GetWidth() / 2f;
            for (int i = 0; i < enemiesBullets.Length; i++) {
                if (!enemiesBullets[i].IsAlive) continue;
                if (!enemiesBullets[i].Collide(playerCenter, playerRadius)) continue;
                enemiesBullets[i].IsAlive = false;
                player.OnHit();
            }
        }

        private void DetectEnemiesCollision (Alien[] aliens) {
            for (int i = 0; i < aliens.Length; i++) {
                if (!aliens[i].IsAlive) continue;
                Vector2 alienCenter = aliens[i].Position;
                float alienRadius = aliens[i].GetWidth() / 2f;
                for (int j = 0; j < playerBullets.Length; j++) {
                    if (!playerBullets[j].IsAlive) continue;
                    if (!playerBullets[j].Collide(alienCenter, alienRadius)) continue;
                    playerBullets[j].IsAlive = false;
                    aliens[i].OnHit();
                }
            }
        }

        /// <summary>
        /// Get a player bullet from the pool, if any is available
        /// </summary>
        /// <returns>A bullet or null if none is available</returns>
        public Bullet GetPlayerBullet () {
            return InternalGetBullet(playerBullets);
        }

        public Bullet GetEnemyBullet() {
            return InternalGetBullet(enemiesBullets);
        }

        private Bullet InternalGetBullet (Bullet[] array) {
            for (int i = 0; i < array.Length; i++) {
                if (array[i].IsAlive) continue;
                return array[i];
            }
            return null;
        }

        private void InternalUpdate (Bullet[] array) {
            for (int i = 0; i < array.Length; i++) {
                array[i].Update();
            }
        }

        private void InternalDraw (Bullet[] array) {
            for (int i = 0; i < array.Length; i++) {
                array[i].Draw();
            }
        }

    }
}
