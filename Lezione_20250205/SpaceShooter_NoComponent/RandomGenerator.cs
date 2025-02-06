using System;

namespace SpaceShooter_NoComponent {
    public static class RandomGenerator {

        private static Random rand;

        static RandomGenerator () {
            rand = new Random();
        }

        public static int GetRandomInt (int min, int max) {
            return rand.Next(min, max + 1);
        }

    }
}
