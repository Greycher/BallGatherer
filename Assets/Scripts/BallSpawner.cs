using System.Collections.Generic;
using UnityEngine;

namespace BallGatherer {
    public class BallSpawner : LevelObject {
        private const string key = nameof(BallSpawner);
        
        public static BallSpawner GetForLevel(Level level) {
            level.TryGetLevelObjectFromDictionary(key, out BallSpawner ballSpawner);
            return ballSpawner;
        }
        
        private Stack<Ball> _ballStack = new Stack<Ball>();
        private Level _level;

        public override void Initialize(Level level) {
            _level = level;
            level.AddLevelObjectToDictionary(key, this);
        }

        public override void Prepare(Level level) { }

        public Ball SpawnNewBallAtPosition(Vector3 position) {
            var newBall = GetNewBall();
            var tr = newBall.transform;
            tr.position = position;
            tr.SetParent(_level.Parent);
            newBall.gameObject.SetActive(true);
            return newBall;
        }
        
        private Ball GetNewBall() {
            Ball ball;
            if (_ballStack.Count > 0) {
                ball = _ballStack.Pop();
            }
            else {
                var gameConfiguration = GameConfiguration.Instance;
                ball = Instantiate(gameConfiguration.ballRes);
                _level.OnNewLevelObjectSpawn(ball);
            }
            return ball;
        }

        public void AddBallToStack(Ball ball) {
            ball.gameObject.SetActive(false);
            _ballStack.Push(ball);
        }

        public override void OnLevelFinish(Level level) { }
    }
}

