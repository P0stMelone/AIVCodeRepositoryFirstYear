using System;

namespace Aiv.Fast2D.Component {

    public enum EventName {
        playerDeath,
        startDialogue,
        ciccioPasticcioAppeared
    }

    public static class EventManager {

        private static Action<EventArgs>[] events;

        static EventManager () {
            events = new Action<EventArgs>[Enum.GetValues(typeof(EventName)).Length];
        }

        public static void CastEvent(EventName eventToCast, EventArgs message) {
            events[(int)eventToCast]?.Invoke(message);
        }

        public static void AddListener (EventName eventToListen, Action<EventArgs> listener) {
            events[(int)eventToListen] += listener;
        }

        public static void RemoveListener (EventName eventToUnlisten, Action<EventArgs> listenerToRemove) {
            events[(int)eventToUnlisten] -= listenerToRemove;
        }

    }
}
