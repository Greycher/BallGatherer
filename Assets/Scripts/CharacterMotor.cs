using UnityEngine;

namespace BallGatherer {
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMotor : LevelObject {
        public float speed;
        public float visualRad;

        protected RectangleBorder _border;
        private Rigidbody _rb;
        private Vector3 _initialLocalPos;
        private Quaternion _initialLocalRot;
        private Vector3 _initialLocalScale;

        public override void Initialize(Level level) {
            _rb = GetComponent<Rigidbody>();
            RecordInitialPos();
        }
        
        public override void Prepare(Level level) {
            SetToInitialPose();
            _border = RectangleBorder.GetForLevel(level);
        }
        
        private void RecordInitialPos() {
            _initialLocalPos = transform.localPosition;
            _initialLocalRot = transform.localRotation;
            _initialLocalScale = transform.localScale;
        }
        
        private void SetToInitialPose() {
            transform.localPosition = _initialLocalPos;
            transform.localRotation = _initialLocalRot;
            transform.localScale = _initialLocalScale;
        }

        public void Move(Vector3 direction) {
            Rotate(direction);
            var newPos = _rb.position + direction * (speed * Time.deltaTime);
            var minPos = _border.GetMinPosition();
            var maxPos = _border.GetMaxPosition();
            newPos.x = Mathf.Clamp(newPos.x, minPos.x + visualRad, maxPos.x - visualRad);
            newPos.z = Mathf.Clamp(newPos.z, minPos.z + visualRad, maxPos.z - visualRad);
            _rb.position = newPos;
        }

        private void Rotate(Vector3 direction) {
            _rb.MoveRotation(Quaternion.LookRotation(direction));
        }
        
        public override void OnLevelFinish(Level level) { }
    }
}

