using OpenTK; 

namespace Aiv.Fast2D.Component {


    public static class EventArgsFactory {


        public static EventArgs EnemyDeathFactory (int score) {
            object[] args = new object[] { score };
            return new EventArgs(args);
        }

        public static void EnemyDeathParser (EventArgs message, out int score) {
            score = (int)message.Args[0];
        }

        public static EventArgs PlayerDamagedFactory (float maxHP, float currentHP, float damage) {
            object[] args = new object[] { maxHP, currentHP, damage };
            return new EventArgs(args);
        }

        public static void PlayerDamagedParser (EventArgs message, out float maxHP, 
            out float currentHP, out float damage) {
            maxHP = (float)message.Args[0];
            currentHP = (float)message.Args[1];
            damage = (float)message.Args[2];
        }
    }
}
