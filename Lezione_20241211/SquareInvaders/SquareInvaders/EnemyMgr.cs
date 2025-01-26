using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvaders {
    public class EnemyMgr {

        private Alien[] aliens;

        public Alien[] Aliens {
            get { return aliens; }
        }

        private int numAlives;
        private int alienWidth = 55;
        private int alienHeight = 40;

        public EnemyMgr (int numOfAliens, int numOfRows, BulletPooler pooler) {

            int aliensPreRow = numOfAliens / numOfRows;
            numAlives = numOfAliens;

            aliens = new Alien[numOfAliens];

            int startX = 80;
            int posY = 40;
            int dist = 5;

            Color green = new Color(0, 255, 0);

            for (int i = 0; i < aliens.Length; i++) {
                if (i != 0 && i % aliensPreRow == 0) {
                    posY += alienHeight + dist;
                }
                int alienX = startX + (i % aliensPreRow) * (alienWidth + dist);

                aliens[i] = new Alien(new Vector2(alienX, posY), new Vector2(100, 0), 
                    alienWidth, alienHeight,green, pooler, i >= numOfAliens - aliensPreRow);
            }

        }

        public void Update () {
            bool endReached = false;
            float tempOverFlowX = 0;
            float overFlowX = 0;

            for (int i = 0; i <aliens.Length; i++) {
                if (!aliens[i].IsVisible) continue;
                if(aliens[i].Update(out tempOverFlowX)) {
                    overFlowX = tempOverFlowX;
                    endReached = true;
                }
            }

            if (!endReached) return;

            for (int i = 0; i < aliens.Length; i++) {
                if (!aliens[i].IsAlive) continue;
                aliens[i].Translate(new Vector2(overFlowX, alienHeight / 2));
                aliens[i].Velocity.X *= -1;
            }
        }

        public void Draw () {
            for (int i = 0; i < aliens.Length; i++) {
                if (!aliens[i].IsVisible) continue;
                aliens[i].Draw();
            }
        }

        /// <summary>
        /// Increase the alien speed
        /// </summary>
        /// <param name="percentage">The percentage to increase. Must be higher then 1 if we want to speedup the aliens, less then 1 if we want to slowdown them</param>
        public void IncAliensSpeed (float percentage) {
            for (int i = 0; i < aliens.Length; i++) {
                aliens[i].Velocity.X *= percentage;
            }
        }

        public bool AllDead () {
            for (int i = 0; i < aliens.Length; i++) {
                if (aliens[i].IsVisible) return false;
            }
            return true;
        }


    }
}
