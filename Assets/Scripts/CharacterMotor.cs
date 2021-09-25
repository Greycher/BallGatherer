using System.Data;
using UnityEngine;

namespace BallGatherer {
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMotor : MonoBehaviour {
        public Rigidbody rb;
        public float speed;
        
        public void Move(Vector3 direction) {
            Rotate(direction);
            rb.MovePosition(rb.position + direction * (speed * Time.deltaTime));
        }

        private void Rotate(Vector3 direction) {
            rb.MoveRotation(Quaternion.LookRotation(direction));
        }
    }
}

