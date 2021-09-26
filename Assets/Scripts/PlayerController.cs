
namespace BallGatherer {
    public class PlayerController : CharacterController {
        private const string key = nameof(PlayerController);

        public override void Initialize(Level level) {
            level.AddLevelObjectToDictionary(key, this);
        }

        public override void Prepare(Level level) {
            var inputManager = PointerInputManager.GetForLevel(level);
            inputManager.AssignController(this);
        }
    }
}

