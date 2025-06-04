namespace Aiv.Fast2D.Component {

    public class EventArgs {

        private object[] args;
        public object[] Args {
            get { return args; }
        }

        public EventArgs (object[] args) {
            this.args = args;
        }

    }

}
