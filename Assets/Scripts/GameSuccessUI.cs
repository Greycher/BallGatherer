using UnityEngine.UI;

namespace BallGatherer {
    public class GameSuccessUI : LevelObject {
        private const string key = nameof(GameSuccessUI);
        
        public static GameSuccessUI GetForLevel(Level level) {
            level.TryGetLevelObjectFromDictionary(key, out GameSuccessUI gameSuccessUI);
            return gameSuccessUI;
        }
        
        public Button playAgainBtn;
        
        private Level _level;
        private UIManager _uiManager;

        public override void Initialize(Level level) {
            _level = level;
            level.AddLevelObjectToDictionary(key, this);
            playAgainBtn.onClick.AddListener(OnPlayAgainButtonClicked);
        }

        public override void Prepare(Level level) {
            _uiManager = UIManager.GetForLevel(level);
        }

        private void OnPlayAgainButtonClicked() {
            _uiManager.LoadLevel();
            _level.StartLevel();
        }

        public override void OnLevelFinish(Level level) { }
    }
}

