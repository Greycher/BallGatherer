using UnityEngine;

namespace BallGatherer {
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterController : LevelObject, IController {
        private CharacterMotor _motor;

        public override void Initialize(Level level) {
            _motor = GetComponent<CharacterMotor>();
        }

        public override void Prepare(Level level) { }

        public void Drag(Vector2 drag) {
            drag = drag.magnitude > 1 ? drag.normalized : drag;
            _motor.Move(Map2DInputTo3D(drag));
        }

        private Vector3 Map2DInputTo3D(Vector2 input) {
            return new Vector3(input.x, 0, input.y);
        }
        
        public override void OnLevelFinish(Level level) { }
    }
}
