using UnityEngine;

namespace BallGatherer {
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMotor : MonoBehaviour {
        public Rigidbody rb;
        public float speed;
        public float visualRad;

        private RectangleBorder _border;

        public void Move(Vector3 direction) {
            Rotate(direction);
            var newPos = rb.position + direction * (speed * Time.deltaTime);
            var minPos = _border.GetMinPosition();
            var maxPos = _border.GetMaxPosition();
            newPos.x = Mathf.Clamp(newPos.x, minPos.x + visualRad, maxPos.x - visualRad);
            newPos.z = Mathf.Clamp(newPos.z, minPos.z + visualRad, maxPos.z - visualRad);
            rb.MovePosition(newPos);
        }

        private void Rotate(Vector3 direction) {
            rb.MoveRotation(Quaternion.LookRotation(direction));
        }

        public void AssignBorder(RectangleBorder border) {
            _border = border;
        }
    }
}

