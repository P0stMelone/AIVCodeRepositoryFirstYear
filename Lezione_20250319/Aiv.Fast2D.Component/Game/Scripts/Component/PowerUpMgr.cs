using OpenTK;

namespace Aiv.Fast2D.Component {
    public class PowerUpMgr : UserComponent {

        private PowerUp[] pooledPowerUps;
        private RandomTimer timer;

        public PowerUpMgr(GameObject owner, int minTime, int maxTime) :base(owner) {
            timer = new RandomTimer(minTime, maxTime);
            timer.Reset();
            CreatePool();
        }

        private void CreatePool () {
            pooledPowerUps = new PowerUp[2];
            pooledPowerUps[0] = CreateEnergyPowerUp();
            pooledPowerUps[1] = CreateTriplePowerUp();
        }

        private PowerUp CreateEnergyPowerUp () {
            GameObject powerUp = GameObject.CreateGameObject("EnergyPowerUp", Vector2.Zero);
            powerUp.Layer = (uint)Layer.PowerUp;
            powerUp.AddCollisionLayer((uint)Layer.Player);
            powerUp.AddComponent(new SpriteRenderer(powerUp, "energyPowerUp", Vector2.One / 2, 
                DrawLayer.Playground));
            powerUp.AddComponent(typeof(Rigidbody));
            powerUp.AddComponent(ColliderFactory.CreateCircleFor(powerUp));
            EnergyPowerUp pu_Component = new EnergyPowerUp(powerUp, 20);
            powerUp.AddComponent(pu_Component);
            return pu_Component;
        }

        private PowerUp CreateTriplePowerUp () {
            GameObject powerUp = GameObject.CreateGameObject("TriplePowerUp", Vector2.Zero);
            powerUp.Layer = (uint)Layer.PowerUp;
            powerUp.AddCollisionLayer((uint)Layer.Player);
            powerUp.AddComponent(new SpriteRenderer(powerUp, "triplePowerUp", Vector2.One / 2,
                DrawLayer.Playground));
            powerUp.AddComponent(typeof(Rigidbody));
            powerUp.AddComponent(ColliderFactory.CreateCircleFor(powerUp));
            TriplePowerUp pu_Component = new TriplePowerUp(powerUp, 5);
            powerUp.AddComponent(pu_Component);
            return pu_Component;
        }

        public override void Update() {
            timer.Tick();
            if (!timer.IsOver()) return;
            if (!pooledPowerUps[0].gameObject.IsActive && !pooledPowerUps[1].gameObject.IsActive) {
                int randomIndex = RandomGenerator.GetRandomInt(0, 2);
                pooledPowerUps[randomIndex].Spawn();
                timer.Reset();
            } else {
                for (int i = 0; i < pooledPowerUps.Length; i++) {
                    if (pooledPowerUps[i].gameObject.IsActive) continue;
                    pooledPowerUps[i].Spawn();
                    timer.Reset();
                    return;
                }
            }
        }

    }
}
