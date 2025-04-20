using OpenTK; 

namespace Aiv.Fast2D.Component {

    public interface IDamaager {

    }

    public static class EventArgsFactory {


        public static EventArgs PlayerDeathFactory (Vector2 deathPosition, GameObject killer) {
            object[] args = new object[] { deathPosition, killer };
            return new EventArgs(args);
        }

        public static void PlayerDeathParser (EventArgs message, out Vector2 deathPosition, out GameObject killer) {
            deathPosition = (Vector2)message.Args[0];
            killer = (GameObject)message.Args[1];
        }

        public static EventArgs StartDialogueFactory (uint dialogueID, uint entryID) {
            object[] args = new object[] { dialogueID, entryID };
            return new EventArgs(args);
        }

        public static void StartDialogueParser (EventArgs message, out uint dialogueID, out uint entryID) {
            dialogueID = (uint)message.Args[0];
            entryID = (uint)message.Args[1];
        }

        public static EventArgs CiccioPasticcioAppearedFactory () {
            return new EventArgs(new object[0]);
        }

        public static void CiccioPasticcioAppearedParser (EventArgs message) {

        }
    }
}
