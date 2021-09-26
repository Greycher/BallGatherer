
using UnityEngine.UI;

namespace BallGatherer {
    public class MainMenuUI : LevelObject {
        private const string key = nameof(MainMenuUI);
        
        public static MainMenuUI GetForLevel(Level level) {
            level.TryGetLevelObjectFromDictionary(key, out MainMenuUI mainMenuUI);
            return mainMenuUI;
        }

        public Button playBtn;
        private UIManager _uiManager;

        public override void Initialize(Level level) {
            level.AddLevelObjectToDictionary(key, this);
            playBtn.onClick.AddListener(OnPlayButtonClicked);
        }

        public override void Prepare(Level level) {
            _uiManager = UIManager.GetForLevel(level);
        }

        private void OnPlayButtonClicked() {
            _uiManager.LoadLevelUI();
        }

        public override void OnLevelFinish(Level level) { }
    }
}

