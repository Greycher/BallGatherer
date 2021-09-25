using UnityEngine;

namespace BallGatherer {
    public class Context : MonoBehaviour {
        private void Awake() {
            var gameConfiguration = GameConfiguration.GetGameConfiguration();
            var playerController = GameObject.FindGameObjectWithTag(gameConfiguration.playerTag).GetComponent<CharacterController>();
            AssignPlayerTransformToTargetFollowers(playerController.transform);
            AssignPlayerControllerToInputManagers(playerController);
        }

        private void AssignPlayerTransformToTargetFollowers(Transform transform) {
            var targetFollowers = GetComponentsInChildren<TargetFollower>();
            for (int i = 0; i < targetFollowers.Length; i++) {
                targetFollowers[i].AssignTargetTransform(transform);
            }
        }
        
        private void AssignPlayerControllerToInputManagers(CharacterController playerController) {
            var inputManagers = GetComponentsInChildren<InputManager>();
            for (int i = 0; i < inputManagers.Length; i++) {
                inputManagers[i].AssignController(playerController);
            }
        }
    }
}

