using System;
using UnityEngine;

namespace BallGatherer {
    public class TargetFollower : MonoBehaviour {
        public float lerpSpeed = 0.7f;
        
        private Transform _targetTr;
        private Vector3 _offset;
        
        private void LateUpdate() {
            if (_targetTr != null) {
                transform.position = Vector3.Lerp(transform.position, _targetTr.position + _offset, lerpSpeed);
            }
        }
        
        public void AssignTargetTransform(Transform targetTr) {
            _targetTr = targetTr;
            _offset = transform.position - targetTr.position;
        }
    }
}

