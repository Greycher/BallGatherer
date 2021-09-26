using UnityEngine;

namespace BallGatherer {
    public class Ball : LevelObject {
        public float progressionAmount;
        public float visualRad;
        
        private BallSpawner _ballSpawner;
        private LevelManager _levelManager;

        public override void Initialize(Level level) { }

        public override void Prepare(Level level) {
            _ballSpawner = BallSpawner.GetForLevel(level);
            _levelManager = LevelManager.GetForLevel(level);
        }

        public void Destroy() {
            _ballSpawner.AddBallToStack(this);
            _levelManager.BallInstanceCount--;
        }
        
        public override void OnLevelFinish(Level level) {
            Destroy();
        }
    }
}

