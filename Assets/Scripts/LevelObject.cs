using UnityEngine;

namespace BallGatherer {
    public abstract class LevelObject : MonoBehaviour {
        
        /// <summary>
        /// Only called at the start of the object's lifetime.
        /// </summary>
        /// <param name="level"></param>
        public abstract void Initialize(Level level);
        
        /// <summary>
        /// Called every time level starts.
        /// </summary>
        /// <param name="level"></param>
        public abstract void Prepare(Level level);
        
        /// <summary>
        /// Called every time level ends.
        /// </summary>
        /// <param name="level"></param>
        public abstract void OnLevelFinish(Level level);
    }
}

