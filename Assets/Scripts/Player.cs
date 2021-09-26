
using UnityEngine;

namespace BallGatherer {
    public class Player : LevelObject {
        private const string key = nameof(Player) + nameof(Transform);

        public static Player GetForLevel(Level level) {
            level.TryGetLevelObjectFromDictionary(key, out Player player);
            return player;
        }
        
        public override void Initialize(Level level) {
            level.AddLevelObjectToDictionary(key, this);
        }

        public override void Prepare(Level level) { }
    }
}

