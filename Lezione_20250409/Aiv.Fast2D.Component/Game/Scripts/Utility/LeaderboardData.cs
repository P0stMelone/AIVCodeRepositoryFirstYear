using System;
using System.IO;

namespace Aiv.Fast2D.Component {
    [Serializable]
    public class LeaderboardData {

        //questo lo devo spostare nella classe leaderboard?
        private const string filePath = @"Game/Assets/Leadearborad.aiv";
        private int maxScore = 10;
        private string[] names;

        public string[] Names {
            get { return names; }
        }

        private int[] scores;

        public int[] Scores {
            get { return scores; }
        }

        public LeaderboardData() {
            names = new string[maxScore];
            scores = new int[maxScore];
            //initialize è farlocco, mi serviva per testare
            if (!File.Exists(filePath)) {
                InitializeTestData(); 
                SaveToFile();         
            }
            else {
                LoadFromFile();       
            }
        }

        public void SetScore(int score, string name) {
            for (int i = 0; i < scores.Length; i++) {
                //trovo punteggio più alto partendo dalla cima
                if (score > scores[i]) {
                    //se lo trovo sposto il penultimo elemento in ultima posizione e salgo finchè non trovo lo spazio "i"
                    for (int j = scores.Length - 1; j > i; j--) {
                        int tempScore = scores[j];
                        scores[j] = scores[j - 1];
                        names[j] = names[j - 1];
                    }
                    //setto il punteggio
                    scores[i] = score;
                    names[i] = name;
                    break;
                }
            }
            SaveToFile();
        }
        public bool[] IsNewHighScore(int[] score) {
            bool[] returnValue = new bool[score.Length];
            for (int i = 0; i < scores.Length-1; i++) {
                returnValue[i] = score[i] > scores[scores.Length - 1];
            }
            return returnValue;
        }

        public bool IsNewHighScore(int score) {
            return score > scores[scores.Length - 1];
        }

        public void SaveToFile() {
            using (StreamWriter writer = new StreamWriter(filePath)) {
                for (int i = 0; i < scores.Length; i++) {
                    writer.WriteLine(names[i] + "," + scores[i]);
                }
            }
        }

        private void LoadFromFile() {
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length && i < maxScore; i++) {
                string[] parts = lines[i].Split(',');
                if (parts.Length == 2) {
                    names[i] = parts[0];
                    scores[i] = int.Parse(parts[1]);
                }
            }
        }

        private void InitializeTestData() {
            string[] testNames = { "CIH", "OME", "SSO", "DUE", "GIO", "RNI", "AFA", "RES", "TAC", "OSA" };
            int[] testScores = { 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 };

            for (int i = 0; i < maxScore; i++) {
                names[i] = testNames[i];
                scores[i] = testScores[i];
            }
        }

    }
}
