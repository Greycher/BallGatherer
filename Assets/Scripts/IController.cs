using UnityEngine;

namespace BallGatherer {
    public interface IController {
        public void Drag(Vector2 weightedDragDirection);
    }
}

