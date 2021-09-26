using System;
using UnityEngine;

namespace BallGatherer {
    public class PlayerFollower : LevelObject {
        public float lerpSpeed = 0.7f;
        
        private Transform _targetTr;
        private Vector3 _offset;
        private Vector3 _initialLocalPos;
        private Quaternion _initialLocalRot;
        private Vector3 _initialLocalScale;
        private Level _level;

        public override void Initialize(Level level) {
            _level = level;
            RecordInitialPos();
        }

        private void RecordInitialPos() {
            _initialLocalPos = transform.localPosition;
            _initialLocalRot = transform.localRotation;
            _initialLocalScale = transform.localScale;
        }

        public override void Prepare(Level level) {
            SetToInitialPose();

            var player = Player.GetForLevel(level);
            AssignTargetTransform(player.transform);
        }

        private void SetToInitialPose() {
            transform.localPosition = _initialLocalPos;
            transform.localRotation = _initialLocalRot;
            transform.localScale = _initialLocalScale;
        }

        private void AssignTargetTransform(Transform targetTr) {
            _targetTr = targetTr;
            CalculateOffset(targetTr);
        }

        private void CalculateOffset(Transform targetTr) {
            //Ensure PlayerMotor is prepared for calculating the right offset
            PlayerMotor.GetForLevel(_level).Prepare(_level);
            _offset = transform.position - targetTr.position;
        }

        private void LateUpdate() {
            if (_targetTr != null) {
                transform.position = Vector3.Lerp(transform.position, _targetTr.position + _offset, lerpSpeed);
            }
        }

        public override void OnLevelFinish(Level level) { }
    }
}

