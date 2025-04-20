using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public static class GameData {

        private static int playerCount;
        private static int[] scores;

        public static int PlayerCount {
            get { return playerCount; }
        }

        public static int[] Scores {
            get { return scores; }
        }

        public static bool IsMultiplayer {
            get { return PlayerCount > 1; }
        }

        public static List<int> PendingHighScores = new List<int>();

        public static LeaderboardData leaderboardData = new LeaderboardData();

        static GameData() {
        }

        public static void SetPlayerCount(int playerNumber) {
            playerCount = playerNumber;
        }

        public static void SetScores(int[] score) {
            if (playerCount > 1) {
                scores = new int[2];
                for (int i = 0; i < playerCount; i++) {
                    scores[i] = score[i];
                }
                return;
            }
            scores = new int[1];
            scores[0] = score[0];
        }

    }
}
