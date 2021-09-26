using UnityEngine;

namespace BallGatherer {
    public abstract class LevelObject : MonoBehaviour {
        public abstract void Initialize(Level level);
        public abstract void Prepare(Level level);
    }
}

