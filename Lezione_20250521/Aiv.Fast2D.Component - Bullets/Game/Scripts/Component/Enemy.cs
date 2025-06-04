namespace Aiv.Fast2D.Component {

    public enum EnemyList {
        white,
        red,
        last
    }

    public class Enemy : UserComponent {

        public Enemy (GameObject owner) :base (owner) {

        }

        public override void Awake() {
            gameObject.IsActive = false;
        }

    }
}
