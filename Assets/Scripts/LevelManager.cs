using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallGatherer {
    public class LevelManager : LevelObject {
        private BallSpawner _ballSpawner = new BallSpawner();
        private RectangleBorder _border;
        
        public override void Initialize(Level level) { }

        public override void Prepare(Level level) {
            _border = RectangleBorder.GetForLevel(level);
            var gameConfiguration = GameConfiguration.Instance;
            for (int i = 0; i < gameConfiguration.ballCount; i++) {
                _ballSpawner.SpawnNewBallAtPosition(level.Parent, _border.GetRandomPointInBounds());
            }
        }
    }
}

