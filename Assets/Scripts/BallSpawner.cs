using System.Collections.Generic;
using UnityEngine;

namespace BallGatherer {
    public class BallSpawner {
        private Stack<GameObject> _ballStack = new Stack<GameObject>();

        public GameObject SpawnNewBallAtPosition(Transform parent, Vector3 position) {
            var newBall = GetNewBall();
            var tr = newBall.transform;
            tr.position = position;
            tr.SetParent(parent);
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
                ball = UnityEngine.Object.Instantiate(gameConfiguration.ballRes);
            }
            return ball;
        }
    }
}

