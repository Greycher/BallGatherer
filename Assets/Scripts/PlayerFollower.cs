using System;
using UnityEngine;

namespace BallGatherer {
    public class PlayerFollower : LevelObject {
        public float lerpSpeed = 0.7f;
        
        private Transform _targetTr;
        private Vector3 _offset;
        
        public override void Initialize(Level level) { }

        public override void Prepare(Level level) {
            var player = Player.GetForLevel(level);
            AssignTargetTransform(player.transform);
        }
        
        private void AssignTargetTransform(Transform targetTr) {
            _targetTr = targetTr;
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

