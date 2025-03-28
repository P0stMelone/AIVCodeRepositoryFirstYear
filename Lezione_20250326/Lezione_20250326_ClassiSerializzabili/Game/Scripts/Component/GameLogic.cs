using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Aiv.Fast2D.Component {
    public class GameLogic : UserComponent {

        private const string filePath = @"Game/Assets/savedData.triceratopo";

        private GameData savedData;

        private TextBox shootNumberText;
        private TextBox timePlayedText;

        public GameLogic (GameObject owner) : base (owner) {

        }

        public override void Awake() {
            LoadSavedData();
            shootNumberText = GameObject.Find("ShootNumberText").GetComponent<TextBox>();
            timePlayedText = GameObject.Find("TimePlayedText").GetComponent<TextBox>();
        }

        public override void Start() {
            UpdateUI();
        }

        public override void OnDestroy() {
            SaveSavedData();
        }

        public override void Update() {
            if(Input.GetKeyDown(KeyCode.Esc)) {
                Game.TriggerChangeScene(null);
            }
            if (Input.GetInputActionButtonDown("Shoot")) {
                savedData.IncreaseNumberOfShoot();
                UpdateUI();
            }
            savedData.IncreaseTimePlayed(Game.DeltaTime);
            UpdateUI();
        }

        private void LoadSavedData () {
            if (!File.Exists(filePath)) {
                savedData = new GameData();
                SaveSavedData();
                return;
            }
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            savedData = (GameData)bf.Deserialize(file);
            file.Close();
        }

        private void SaveSavedData() {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            bf.Serialize(file, savedData);
            file.Close();
        }

        private void UpdateUI () {
            shootNumberText.SetText(savedData.NumberOfShoot.ToString());
            timePlayedText.SetText(((int)savedData.TimePlayed).ToString());
        }

    }
}
