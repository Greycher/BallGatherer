using UnityEngine;

namespace BallGatherer {
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterController : MonoBehaviour, IController {
        private CharacterMotor _motor;

        private void Awake() {
            _motor = GetComponent<CharacterMotor>();
        }

        public void Drag(Vector2 weightedDragDirection) {
            weightedDragDirection = weightedDragDirection.magnitude > 1 ? weightedDragDirection.normalized : weightedDragDirection;
            _motor.Move(Map2DInputTo3D(weightedDragDirection));
        }

        private Vector3 Map2DInputTo3D(Vector2 input) {
            return new Vector3(input.x, 0, input.y);
        }
    }
}
