using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component.Game.Scripts.FSM {
    public class InLineOfSight : Condition {

        private StateMachineVariable<Transform> owner;
        private StateMachineVariable<Transform> target;
        private float lineAngle;

        public InLineOfSight(StateMachineVariable<Transform> owner, StateMachineVariable<Transform> target, float lineAngle) {
            this.owner = owner;
            this.target = target;
            this.lineAngle = lineAngle;
        }

        public override bool Validate() {
            Vector2 diffNormalized = (target.GetValue().Position - owner.GetValue().Position).Normalized();
            float dotProduct = Vector2.Dot(owner.GetValue().Forward, diffNormalized);
            float angleBetween = Transform.RadiantsToDegrees((float)Math.Acos(dotProduct));
            return angleBetween <= lineAngle;
        }

    }
}
