using UnityEngine;

namespace BallGatherer {
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMotor : MonoBehaviour {
        public Rigidbody rb;
        public float speed;
        
        public void Move(Vector3 weightedDirection) {
            rb.MovePosition(rb.position + weightedDirection * speed * Time.deltaTime);
        }
    }
}

