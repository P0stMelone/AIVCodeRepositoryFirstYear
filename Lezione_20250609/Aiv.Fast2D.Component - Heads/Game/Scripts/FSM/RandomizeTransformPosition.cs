using OpenTK;

namespace Aiv.Fast2D.Component {
    public class RandomizeTransformPosition : Action
        {


        private StateMachineVariable<Transform> target;
        private Vector2 xBound;
        private Vector2 yBound;

        public RandomizeTransformPosition(StateMachineVariable<Transform> target,
            Vector2 xBound, Vector2 yBound) {
            this.target = target;
            this.xBound = xBound;
            this.yBound = yBound;
        }

        public override void OnEnter() {
            Vector2 randomizedPosition = new Vector2(RandomGenerator.GetRandomFloat(xBound.X, xBound.Y),
                RandomGenerator.GetRandomFloat(yBound.X, yBound.Y));
            target.GetValue().Position = randomizedPosition;
        }

        public static RandomizeTransformPosition Factory(Transform transform, Vector2 xBound, Vector2 yBound,
            string transformName = "", FSMComponent fsm = null) {

            StateMachineVariable<Transform> target = string.IsNullOrEmpty(transformName) ?
                new StateMachineVariable<Transform>(transform) :
                new StateMachineVariable<Transform>(fsm, transformName);
            return new RandomizeTransformPosition(target, xBound, yBound);
        }

    }
}
