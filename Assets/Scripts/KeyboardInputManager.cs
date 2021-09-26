using UnityEngine;

namespace BallGatherer {
    public class KeyboardInputManager : InputManager {
        private const string key = nameof(KeyboardInputManager);
        
        public static KeyboardInputManager GetForLevel(Level level) {
            level.TryGetLevelObjectFromDictionary(key, out KeyboardInputManager keyboardInputManager);
            return keyboardInputManager;
        }

        public bool Enable {
            get => _enable;
            set => _enable = value;
        }

        private bool _enable;
        
        public override void Initialize(Level level) {
            level.AddLevelObjectToDictionary(key, this);
        }
        
        private void Update() {
            if (_enable) {
                Vector2 input = Vector2.zero;
                if (Input.GetKey(KeyCode.W)) {
                    input.y += 1;
                }
                if (Input.GetKey(KeyCode.S)) {
                    input.y -= 1;
                }
                if (Input.GetKey(KeyCode.D)) {
                    input.x += 1;
                }
                if (Input.GetKey(KeyCode.A)) {
                    input.x -= 1;
                }

                if (_controller != null && input != Vector2.zero) {
                    _controller.Drag(input);
                }
            }
        }

        public override void OnLevelFinish(Level level) {
            base.OnLevelFinish(level);
            _enable = false;
        }
    }
}

