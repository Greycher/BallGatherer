using UnityEngine;

namespace BallGatherer {
    public class CharacterMotor : LevelObject {
        public float speed;
        public float visualRad;

        protected RectangleBorder _border;
        
        public override void Initialize(Level level) { }

        public override void Prepare(Level level) { }

        public void Move(Vector3 direction) {
            Rotate(direction);
            var newPos = transform.position + direction * (speed * Time.deltaTime);
            var minPos = _border.GetMinPosition();
            var maxPos = _border.GetMaxPosition();
            newPos.x = Mathf.Clamp(newPos.x, minPos.x + visualRad, maxPos.x - visualRad);
            newPos.z = Mathf.Clamp(newPos.z, minPos.z + visualRad, maxPos.z - visualRad);
            transform.position = (newPos);
        }

        private void Rotate(Vector3 direction) {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}

