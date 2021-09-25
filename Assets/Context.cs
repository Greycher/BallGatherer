using UnityEngine;

namespace BallGatherer {
    public class Context : MonoBehaviour {
        private const string PLAYER_TAG = "Player";
        private void Awake() {
            var playerController = GameObject.FindGameObjectWithTag(PLAYER_TAG).GetComponent<CharacterController>();
            var inputManagers = GetComponentsInChildren<InputManager>();
            for (int i = 0; i < inputManagers.Length; i++) {
                inputManagers[i].AssignController(playerController);
            }
        }
    }
}

