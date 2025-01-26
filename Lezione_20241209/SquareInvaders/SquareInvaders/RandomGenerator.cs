using System;

namespace SquareInvaders {
    public static class RandomGenerator {

        private static Random random;

        static RandomGenerator () {
            random = new Random();
        }

        public static int GetRandom (int min, int max) {
            return random.Next(min, max);
        }

    }
}
