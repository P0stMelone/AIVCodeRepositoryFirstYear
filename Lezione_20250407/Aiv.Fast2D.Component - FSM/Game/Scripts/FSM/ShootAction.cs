using System;

namespace Aiv.Fast2D.Component {
    public class ShootAction : Action{


        private StateMachineVariable<float> reloadTime;

        private float currentReloadTime;

        public ShootAction (StateMachineVariable<float> reloadTime) {
            this.reloadTime = reloadTime;
            currentReloadTime = reloadTime.GetValue();
        }

        public override void OnUpdate() {
            currentReloadTime += Game.Win.DeltaTime;
            if (currentReloadTime > reloadTime.GetValue()) {
                InternalShoot();
            }
        }

        private void InternalShoot () {
            Console.WriteLine("Sparo");
            currentReloadTime = 0;
        }


    }
}
