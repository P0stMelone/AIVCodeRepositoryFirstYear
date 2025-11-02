

namespace Aiv.Fast2D.Component {
    public class ShootAction : Action {

        private StateMachineVariable<ShootModule> shootModule;

        private ShootAction(StateMachineVariable<ShootModule> shootModule) {
            this.shootModule = shootModule;
        }

        public override void OnEnter() {
            shootModule.GetValue().Shoot(BulletType.Enemy);
        }

        public static ShootAction Factory (ShootModule sm, string smName = "", FSMComponent fsm = null) {

            var smVar = string.IsNullOrEmpty(smName) ?
                new StateMachineVariable<ShootModule>(sm) :
                new StateMachineVariable<ShootModule>(fsm, smName);

            return new ShootAction(smVar);
        }

    }
}
