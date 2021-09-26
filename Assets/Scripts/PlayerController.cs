
namespace BallGatherer {
    public class PlayerController : CharacterController {
        private const string key = nameof(PlayerController);
        
        public override void Initialize(Level level) {
            base.Initialize(level);
            level.AddLevelObjectToDictionary(key, this);
        }

        public override void Prepare(Level level) {
            var pointerInputManager = PointerInputManager.GetForLevel(level);
            pointerInputManager.AssignController(this);
            var keyboardInputManager = KeyboardInputManager.GetForLevel(level);
            keyboardInputManager.AssignController(this);
        }
    }
}

