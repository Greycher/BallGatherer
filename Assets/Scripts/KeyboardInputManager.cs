using UnityEngine;

namespace BallGatherer {
    public class KeyboardInputManager : InputManager {
        private const string key = nameof(KeyboardInputManager);
        
        public static KeyboardInputManager GetForLevel(Level level) {
            level.TryGetLevelObjectFromDictionary(key, out KeyboardInputManager keyboardInputManager);
            return keyboardInputManager;
        }
        
        public override void Initialize(Level level) {
            level.AddLevelObjectToDictionary(key, this);
        }
        
        private void Update() {
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

            if (_controller != null) {
                _controller.Drag(input);
            }
        }
    }
}

