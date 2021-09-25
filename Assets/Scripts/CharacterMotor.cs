using UnityEngine;

namespace BallGatherer {
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMotor : MonoBehaviour {
        public Rigidbody rb;
        public float speed;
        public float visualRad;

        private RectangleBorder _border;
        private Context _context;

        public void Move(Vector3 direction) {
            Rotate(direction);
            var newPos = rb.position + direction * (speed * Time.deltaTime);
            var minPos = _context.GetBorderMinPos();
            var maxPos = _context.GetBorderMaxPos();
            newPos.x = Mathf.Clamp(newPos.x, minPos.x + visualRad, maxPos.x - visualRad);
            newPos.z = Mathf.Clamp(newPos.z, minPos.z + visualRad, maxPos.z - visualRad);
            rb.MovePosition(newPos);
        }

        private void Rotate(Vector3 direction) {
            rb.MoveRotation(Quaternion.LookRotation(direction));
        }

        public void AssignContext(Context context) {
            _context = context;
        }
    }
}

