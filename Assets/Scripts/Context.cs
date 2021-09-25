using UnityEngine;

namespace BallGatherer {
    public class Context : MonoBehaviour {
        public Transform Parent => transform;
        
        private RectangleBorder _border;
        
        private void Awake() {
            var gameConfiguration = GameConfiguration.GetGameConfiguration();
            var playerGameObject = FindPlayer(gameConfiguration.playerTag);
            
            var playerMotor = playerGameObject.GetComponent<CharacterMotor>();
            playerMotor.AssignContext(this);
            
            var playerController = playerGameObject.GetComponent<CharacterController>();
            PrepareTargetFollowers(playerController.transform);
            PrepareInputManagers(playerController);

            _border = GetComponentInChildren<RectangleBorder>();
        }

        private GameObject FindPlayer(string playerTag) {
            var parent = Parent;
            for (int i = 0; i < parent.childCount; i++) {
                var child = parent.GetChild(i);
                if (child.CompareTag(playerTag)) {
                    return child.gameObject;
                }
            }

            return null;
        }

        private void PrepareTargetFollowers(Transform transform) {
            var targetFollowers = GetComponentsInChildren<TargetFollower>();
            for (int i = 0; i < targetFollowers.Length; i++) {
                targetFollowers[i].AssignTargetTransform(transform);
            }
        }
        
        private void PrepareInputManagers(CharacterController playerController) {
            var camera = GetComponentInChildren<Camera>();
            var inputManagers = GetComponentsInChildren<InputManager>();
            for (int i = 0; i < inputManagers.Length; i++) {
                var inputManager = inputManagers[i];
                inputManager.AssignController(playerController);
            }
        }

        public Vector3 GetBorderMinPos() {
            return Parent.TransformPoint(_border.GetMinPosition());
        }
        
        public Vector3 GetBorderMaxPos() {
            return Parent.TransformPoint(_border.GetMaxPosition());
        }
    }
}

