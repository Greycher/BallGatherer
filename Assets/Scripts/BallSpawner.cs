using System.Collections.Generic;
using UnityEngine;

namespace BallGatherer {
    public class BallSpawner {
        private Stack<GameObject> _ballStack = new Stack<GameObject>();
        private Level _level;

        public void AssignContext(Level level) {
            _level = level;
        }

        public GameObject SpawnNewBallAtPosition(Vector3 position) {
            var newBall = GetNewBall();
            newBall.transform.position = position;
            newBall.SetActive(true);
            return newBall;
        }

        public void AddBallToStack(GameObject ball) {
            ball.SetActive(false);
            _ballStack.Push(ball);
        }

        private GameObject GetNewBall() {
            GameObject ball;
            if (_ballStack.Count > 0) {
                ball = _ballStack.Pop();
            }
            else {
                var gameConfiguration = GameConfiguration.Instance;
                ball = UnityEngine.Object.Instantiate(gameConfiguration.ballRes, _level.Parent);
            }
            return ball;
        }
    }
}

