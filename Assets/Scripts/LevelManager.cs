using System;
using UnityEngine;

namespace BallGatherer {
    public class LevelManager : LevelObject {
        private const string key = nameof(LevelManager);

        public static LevelManager GetForLevel(Level level) {
            level.TryGetLevelObjectFromDictionary(key, out LevelManager levelManager);
            return levelManager;
        }

        public float Progress => _progress;

        public int BallInstanceCount {
            get => _ballInstanceCount;
            set => _ballInstanceCount = value;
        }

        private BallSpawner _ballSpawner;
        private RectangleBorder _border;
        private float _progress;
        private int _ballInstanceCount;
        private UIManager _uiManager;
        private Level _level;

        public override void Initialize(Level level) {
            _level = level;
            level.AddLevelObjectToDictionary(key, this);
        }

        public override void Prepare(Level level) {
            Reset();

            _uiManager = UIManager.GetForLevel(level);
            _ballSpawner = BallSpawner.GetForLevel(level);
            _border = RectangleBorder.GetForLevel(level);
            
            PopulateInitialBalls();
        }

        private void Reset() {
            _progress = 0;
        }
        
        private void PopulateInitialBalls() {
            var gameConfiguration = GameConfiguration.Instance;
            for (int i = 0; i < gameConfiguration.ballCount; i++) {
                RequestNewBallSpawn();
            }
        }

        private void RequestNewBallSpawn() {
            _ballSpawner.SpawnNewBallAtPosition(_border.GetRandomPointInBounds(GameConfiguration.Instance.ballRes.visualRad));
            _ballInstanceCount++;
        }

        private void Update() {
            if (_level.IsPlaying) {
                var gameConfiguration = GameConfiguration.Instance;
                while (_ballInstanceCount < gameConfiguration.ballCount) {
                    RequestNewBallSpawn();
                }
            }
        }

        public void IncreaseProgress(float amount) {
            _progress += amount;
            if (_progress >= 1) {
                _level.FinishLevel();
            }
        }

        public override void OnLevelFinish(Level level) {
            _uiManager.LoadGameSuccess();
        }
    }
}

