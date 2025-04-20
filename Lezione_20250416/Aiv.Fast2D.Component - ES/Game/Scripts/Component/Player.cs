using OpenTK;

namespace Aiv.Fast2D.Component {
    public class Player : UserComponent {


        private int hp;

        public Player (GameObject owner) : base (owner) {
            hp = 10;
        }

        public void TakeDamage (int damage, GameObject damager) {
            hp -= damage;
            if (hp <= 0) {
                EventManager.CastEvent(EventName.playerDeath, 
                    EventArgsFactory.PlayerDeathFactory(transform.Position, damager));
            }
        }

    }
}
