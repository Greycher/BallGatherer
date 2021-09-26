
namespace BallGatherer {
    public class LevelUI : LevelObject {
        private const string key = nameof(LevelUI);
        
        public static LevelUI GetForLevel(Level level) {
            level.TryGetLevelObjectFromDictionary(key, out LevelUI levelUI);
            return levelUI;
        }
        
        public override void Initialize(Level level) {
            level.AddLevelObjectToDictionary(key, this);
        }

        public override void Prepare(Level level) { }

        public override void OnLevelFinish(Level level) { }
    }
}

