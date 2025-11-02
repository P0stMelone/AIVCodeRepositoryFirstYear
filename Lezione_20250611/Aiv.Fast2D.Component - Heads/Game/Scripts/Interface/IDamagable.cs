using OpenTK;

namespace Aiv.Fast2D.Component {
    public struct DamageFeedback {
        public bool damageHasBeenDone;

        public DamageFeedback(bool damageHasBeenDone) {
            this.damageHasBeenDone = damageHasBeenDone;
        }

        public static DamageFeedback DefaultFeedback {
            get { return new DamageFeedback(true); }
        }
    }

    public struct DamageContainer {
        public float damage;
        public Vector2 damagePoint;
        public GameObject sourceOfDamage;

        public DamageContainer(float damage, Vector2 damagePoint, GameObject sourceOfDamage) {
            this.damage = damage;
            this.damagePoint = damagePoint;
            this.sourceOfDamage = sourceOfDamage;
        }
    }
    public interface IDamagable {
        DamageFeedback TakeDamage(DamageContainer damage);
    }
}
