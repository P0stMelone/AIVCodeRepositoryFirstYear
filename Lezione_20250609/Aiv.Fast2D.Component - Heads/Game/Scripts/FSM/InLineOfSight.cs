using OpenTK;
using System;

namespace Aiv.Fast2D.Component {
    public class InLineOfSight : Condition {

        private StateMachineVariable<Transform> owner;
        private StateMachineVariable<Transform> target;
        private float lineAngle;
        private bool value;

        public InLineOfSight(StateMachineVariable<Transform> owner, StateMachineVariable<Transform> target, 
            float lineAngle, bool value) {
            this.owner = owner;
            this.target = target;
            this.lineAngle = lineAngle;
            this.value = value;
        }

        public override bool Validate() {
            Vector2 diffNormalized = (target.GetValue().Position - owner.GetValue().Position).Normalized();
            float dotProduct = Vector2.Dot(owner.GetValue().Forward, diffNormalized);
            float angleBetween = Transform.RadiantsToDegrees((float)Math.Acos(dotProduct));
            return angleBetween <= lineAngle == value;
        }

        public static InLineOfSight Factory (Transform owner, Transform target, float lineAngle, bool value,
            string ownerName = "", string targetName = "", FSMComponent fsm = null) {
            StateMachineVariable<Transform> ownerVar = string.IsNullOrEmpty(ownerName) ?
                new StateMachineVariable<Transform>(owner) :
                new StateMachineVariable<Transform>(fsm, ownerName);
            StateMachineVariable<Transform> targetVar = string.IsNullOrEmpty(targetName) ?
                new StateMachineVariable<Transform>(target) :
                new StateMachineVariable<Transform>(fsm, targetName);
            return new InLineOfSight(ownerVar, targetVar, lineAngle, value);
        }

    }
}
