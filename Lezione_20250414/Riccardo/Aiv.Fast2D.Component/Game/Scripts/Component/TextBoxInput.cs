using System;

namespace Aiv.Fast2D.Component {
    public class TextBoxInput : UserComponent {

        public Action<string> onConfirm;

        private const int maxLetters = 3;

        private char[] availableLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private int[] currentLetter;
        private int letterIndex;

        private char[] charName;
        private string name;

        private bool confirmSelection = false;
        private bool ConfirmSelection {
            get { return confirmSelection; }
            set {
                if (value && !confirmSelection) {
                    onConfirm?.Invoke(name);
                }
                confirmSelection = value;
            }
        }

        private TextBox nameText;

        public TextBoxInput(GameObject owner) : base(owner) {

        }

        public override void Awake() {
            currentLetter = new int[maxLetters];
            letterIndex = 0;
            charName = new char[maxLetters];
            nameText = GameObject.Find("TextName").GetComponent<TextBox>();
            ResetName();
        }

        public void ResetName() {
            for (int i = 0; i < maxLetters; i++) {
                charName[i] = 'A';
                currentLetter[i] = 0;
            }
            letterIndex = 0;
        }

        public override void Update() {
            if (Input.GetInputActionButtonDown("MoveUp")) {
                currentLetter[letterIndex] = (currentLetter[letterIndex] - 1 + availableLetters.Length) % availableLetters.Length;
            }
            if (Input.GetInputActionButtonDown("MoveDown")) {
                currentLetter[letterIndex] = (currentLetter[letterIndex] + 1) % availableLetters.Length;
            }
            if (Input.GetInputActionButtonDown("MoveLeft")) {
                letterIndex = (letterIndex - 1 + maxLetters) % maxLetters;
            }
            if (Input.GetInputActionButtonDown("MoveRight")) {
                letterIndex = (letterIndex + 1) % maxLetters;
            }
            if (Input.GetInputActionButtonDown("Confirm")) {
                ConfirmSelection = true;
            }
            charName[letterIndex] = availableLetters[currentLetter[letterIndex]];
            UpdateText();
        }

        public void UpdateText() {
            name = new string(charName);
            nameText.SetText(name);
        }

    }
}
