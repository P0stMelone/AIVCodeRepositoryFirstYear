using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class CircleCollider : Collider {

        private float radius;
        public float Radius {
            get { return radius * transform.Scale.X; }
        }

        public CircleCollider(GameObject owner, Vector2 offset, float radius) : base(owner, offset) {
            this.radius = radius;
        }

        public override bool Collides(Collider collider, ref Collision collisionInfo) {
            return collider.Collides(this, ref collisionInfo);
        }

        public override bool Collides(BoxCollider rect, ref Collision collisionInfo) {
            return rect.Collides(this, ref collisionInfo);
        }

        public override bool Collides(CircleCollider circle, ref Collision collisionInfo) {
            float distanceSquared = (circle.Position - Position).LengthSquared;
            float sumRadiusSquared = (float)Math.Pow(Radius + circle.Radius, 2);
            if (distanceSquared <= sumRadiusSquared) {
                collisionInfo.Type = CollisionType.CircleIntersection;
                collisionInfo.DeltaCircle = (float)Math.Sqrt(distanceSquared) - (Radius + circle.Radius);
                return true;
            } else {
                collisionInfo.Type = CollisionType.None;
                return false;
            }
        }

        public override bool Contains(Vector2 point) {
            float distanceSquared = (point - Position).LengthSquared;
            return distanceSquared <= Math.Pow(Radius, 2);
        }
#if DEBUG
        void DrawCircle(int centerX, int centerY, int radius) {

            int x = 0;
            int y = radius;
            int d = 3 - 2 * radius;

            while (x <= y) {
                // Disegna gli 8 punti simmetrici
                PlotPoint(centerX + x, centerY + y);
                PlotPoint(centerX - x, centerY + y);
                PlotPoint(centerX + x, centerY - y);
                PlotPoint(centerX - x, centerY - y);
                PlotPoint(centerX + y, centerY + x);
                PlotPoint(centerX - y, centerY + x);
                PlotPoint(centerX + y, centerY - x);
                PlotPoint(centerX - y, centerY - x);

                if (d < 0)
                    d += 4 * x + 6;
                else {
                    d += 4 * (x - y) + 10;
                    y--;
                }
                x++;
            }
        }

        // Funzione helper per disegnare un singolo punto
        void PlotPoint(int x, int y) {
            // Qui inserisci il codice per disegnare un pixel
            // usando il sistema di rendering che stai utilizzando
        }
#endif
    }
}
