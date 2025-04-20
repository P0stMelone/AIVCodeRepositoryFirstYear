namespace Aiv.Fast2D.Component {
    public abstract class Action : ExecutableNode {

        public virtual void OnUpdate () { }
        public virtual  void OnLateUpdate () { }
        public virtual void OnCollide(GameObject other) { }
        
    }
}
