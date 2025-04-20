

using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class AsteroidPool : UserComponent {

        private AsteroidComponent[] pool;

        private RandomTimer randomTimer;

        public AsteroidPool(GameObject gameObject, int poolSize, int timeMin, int timeMax) : base(gameObject) {
            randomTimer = new RandomTimer(timeMin, timeMax);
            randomTimer.Reset();
            pool = new AsteroidComponent[poolSize];
            CreateAsteroids();
        }

        private void CreateAsteroids() {
            GameObject[] asteroidPool = new GameObject[pool.Length];
            for (int i = 0; i < pool.Length; i++) {
                asteroidPool[i] = GameObject.CreateGameObject("Asteroid" + (i+1), Vector2.Zero);
                asteroidPool[i].AddComponent<SpriteRenderer>("asteroid", Vector2.One * 0.5f, DrawLayer.Playground);
                float tempScale = RandomGenerator.GetRandomFloat(0.5f, 1.5f);
                asteroidPool[i].transform.Scale = new Vector2(tempScale, tempScale);
                asteroidPool[i].AddComponent<Rigidbody>();
                asteroidPool[i].AddComponent(ColliderFactory.CreateCircleFor(asteroidPool[i]));
                asteroidPool[i].Layer = (uint)Layer.Asteroid;
                asteroidPool[i].AddCollisionLayer((uint)Layer.Player);
                pool[i] = new AsteroidComponent(asteroidPool[i], RandomGenerator.GetRandomInt(50, 91));
                asteroidPool[i].AddComponent(pool[i]);
                asteroidPool[i].AddComponent(new BounceBorderComponent(asteroidPool[i], true,true,false,false));
            }
        }

        public override void Update() {
            randomTimer.Tick();
            if (!randomTimer.IsOver()) return;
            AsteroidComponent asteroidToSpawn = GetAsteroidFromPool();
            if (asteroidToSpawn == null) return;
            asteroidToSpawn.SpawnAsteroid(new Vector2(Game.Win.Width + asteroidToSpawn.Width / 2,
                RandomGenerator.GetRandomFloat(asteroidToSpawn.Height / 2, Game.Win.Height - asteroidToSpawn.Height / 2)),
                new Vector2(RandomGenerator.GetRandomFloat(-200, -400), RandomGenerator.GetRandomFloat(90, -90)));
            randomTimer.Reset();
        }

        private AsteroidComponent GetAsteroidFromPool() {
            for (int i = 0; i < pool.Length; i++) {
                if (pool[i].gameObject.IsActive) continue;
                return pool[i];
            }
            return null;
        }
    }
}
