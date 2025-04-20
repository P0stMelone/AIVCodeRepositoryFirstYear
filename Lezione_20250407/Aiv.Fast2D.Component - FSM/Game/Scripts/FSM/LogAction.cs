using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component {
    public class LogAction : Action {

        private string textToLog;
        private bool everyFrame;

        public LogAction(string textToLog, bool everyFrame) {
            this.textToLog = textToLog;
            this.everyFrame = everyFrame;
        }

        public override void OnEnter() {
            InteralLogAction();
        }

        public override void OnUpdate() {
            if (!everyFrame) return;
            InteralLogAction();
        }

        private void InteralLogAction () {
            Console.WriteLine(textToLog);
        }




    }
}
