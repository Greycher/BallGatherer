using UnityEngine;

namespace BallGatherer {
    public class Player : LevelObject {
        private const string key = nameof(Player) + nameof(Transform);
        
        public static Player GetForLevel(Level level) {
            level.TryGetLevelObjectFromDictionary(key, out Player player);
            return player;
        }
        
        private LevelManager _levelManager;

        public override void Initialize(Level level) {
            level.AddLevelObjectToDictionary(key, this);
        }

        public override void Prepare(Level level) {
            _levelManager = LevelManager.GetForLevel(level);
        }

        private void OnTriggerEnter(Collider collider) {
            if (collider.TryGetComponent(out Ball ball)) {
                _levelManager.IncreaseProgress(ball.progressionAmount);
                ball.Destroy();
            }
        }

        public override void OnLevelFinish(Level level) { }
    }
}

