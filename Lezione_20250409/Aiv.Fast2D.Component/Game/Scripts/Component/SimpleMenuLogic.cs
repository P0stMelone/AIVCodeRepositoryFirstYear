using System;
using OpenTK;

namespace Aiv.Fast2D.Component {
    public class SimpleMenuLogic : UserComponent {

        //scene
        protected Type confirmScene;
        protected Type menuScene;
        protected Type leaderboardScene;

        //configurazione tasti
        protected string confirmButton;
        protected string upButton;
        protected string downButton;

        //menu e testo
        protected Vector2 standardSize = Vector2.One;
        protected Vector2 selectSize = new Vector2(1.5f, 1.5f);
        protected TextBox[] textbox;
        protected int selectionIndex = 0;

        //logica
        protected int numberOfSelection;

        public SimpleMenuLogic(GameObject owner, Type confirmScene, Type menuScene, Type leaderboardScene,
            string confirmButton, string upButton, string downButton, int numberOfSelection) : base(owner) {
            this.confirmScene = confirmScene;
            this.menuScene = menuScene;
            this.leaderboardScene = leaderboardScene;
            this.confirmButton = confirmButton;
            this.upButton = upButton;
            this.downButton = downButton;
            this.numberOfSelection = numberOfSelection;
        }

        public override void Awake() {
        }

        //metodo per aggiornare il la grandezza dei caratteri in base all'indice corrente
        public void UpdateSize() {
            for (int i = 0; i < textbox.Length; i++) {
                textbox[i].SetScale((i == selectionIndex) ? selectSize : standardSize);
            }
        }

        public override void Update() {
            if (Input.GetInputActionButtonDown(confirmButton)) {
                Selection();
            }
            else if (Input.GetInputActionButtonDown(upButton)) {
                selectionIndex = (selectionIndex - 1 + numberOfSelection) % numberOfSelection;
                
            }
            else if (Input.GetInputActionButtonDown(downButton)) {
                selectionIndex = (selectionIndex + 1) % numberOfSelection;
            }
            UpdateText();
            UpdateSize();
        }

        public virtual void Selection() {

        }

        public virtual void UpdateText() {

        }


    }
}
