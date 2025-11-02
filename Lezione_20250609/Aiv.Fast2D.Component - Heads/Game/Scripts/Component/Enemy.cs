using System;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public class Enemy : UserComponent {

        private FSMComponent fsm;
        private GameObject[] players;

        private InLineOfSight[] lineOfSight;
        private CheckDistance[] checkDistances;

        public Enemy (GameObject owner) : base (owner) {

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

        public override void Update() {
            fsm.SetVariable(FSMTemplate.targetName, FindBestPlayer());
        }

        private Transform FindBestPlayer () {
            List<GameObject> visiblePlayers = new List<GameObject>();
            for (int i = 0; i< players.Length; i++) {
                bool close = checkDistances[i].Validate();
                bool inLine = lineOfSight[i].Validate();
                Console.WriteLine("Player: " + players[i].Name + " close: " + close + " inLine: " + inLine);
                if (close && inLine) visiblePlayers.Add(players[i]);
            }
            if (visiblePlayers.Count > 0) {
                return visiblePlayers[0].transform;
            }
            return players[0].transform;//tanto poi il controllo lo fa anche nelle condizioni se è nel campo visivo
        }

    }
}
