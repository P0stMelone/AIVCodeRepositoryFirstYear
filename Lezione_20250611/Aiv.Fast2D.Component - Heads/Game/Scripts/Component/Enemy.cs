using OpenTK;
using System.Collections.Generic;
using System;

namespace Aiv.Fast2D.Component {
    public class Enemy : UserComponent, IDamagable {

        private FSMComponent fsm;
        private GameObject[] players;

        private InLineOfSight[] lineOfSight;
        private CheckDistance[] checkDistances;

        private HealthModule hm;

        public Enemy (GameObject owner, int hp) : base (owner) {
            hm = new HealthModule(hp);
        }

        public override void Awake() {
            players = GameObject.FindAll("Player");
            fsm = GetComponent<FSMComponent>();
            lineOfSight = new InLineOfSight[players.Length];
            checkDistances = new CheckDistance[players.Length];
            for (int i = 0; i < lineOfSight.Length; i++) {
                lineOfSight[i] = InLineOfSight.Factory(
                    transform,
                    players[i].transform,
                    FSMTemplate.enemyViewAngle, true);
                checkDistances[i] = CheckDistance.Factory(
                    transform,
                    players[i].transform,
                    FSMTemplate.enemyMaxDistance,
                    ComparerType.LessOrEqual);
                checkDistances[i].OnEnter();
            }
        }

        public DamageFeedback TakeDamage(DamageContainer damage) {
            if (hm.TakeDamage((int)damage.damage)) {
                Die();
            }
            return DamageFeedback.DefaultFeedback;
        }

        private void Die () {
            gameObject.IsActive = false;
        }

        public override void Update() {
            fsm.SetVariable(FSMTemplate.targetName, FindBestPlayer());
        }

        private Transform FindBestPlayer () {
            List<GameObject> visiblePlayers = new List<GameObject>();
            for (int i = 0; i< players.Length; i++) {
                bool close = checkDistances[i].Validate();
                bool inLine = lineOfSight[i].Validate();
                if (close && inLine) visiblePlayers.Add(players[i]);
            }
            if (visiblePlayers.Count > 0) {
                int bestPlayerIndex = 0;
                float maxPlayerScore = 0;
                for (int i = 0; i < visiblePlayers.Count; i++) {
                    Console.WriteLine("Distance value: " + GetDistanceWeight(visiblePlayers[i])
                        + " Health value: " + GetHealthWeight(visiblePlayers[i]) 
                        + " Direction value: " + GetDirectionWeight(visiblePlayers[i])
                        );

                    float playerScore = GetDirectionWeight(visiblePlayers[i]) + GetDistanceWeight(visiblePlayers[i]) + GetHealthWeight(visiblePlayers[i]);
                    if (playerScore > maxPlayerScore) {
                        bestPlayerIndex = i;
                        maxPlayerScore = playerScore;
                    }
                }
                return visiblePlayers[bestPlayerIndex].transform;
            }
            return players[0].transform;//tanto poi il controllo lo fa anche nelle condizioni se è nel campo visivo
        }

        private float GetDistanceWeight (GameObject player) {
            float distanceSquared = (transform.Position - player.transform.Position).LengthSquared;
            return MathHelper.Clamp(1 - (distanceSquared / (FSMTemplate.enemyMaxDistance * FSMTemplate.enemyMaxDistance)), 0, 1);
        }

        private float GetHealthWeight (GameObject player) {
            return 1 - player.GetComponent<PlayerController>().HealthPercentage;
        }

        private float GetDirectionWeight (GameObject player) {
            float degrees = MathHelper.RadiansToDegrees((float)Math.Acos(
                Vector2.Dot(transform.Forward, -player.transform.Forward)));
            return 1 - degrees/180f;
        }

    }
}
