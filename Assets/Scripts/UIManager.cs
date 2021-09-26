
namespace BallGatherer {
    public class UIManager : LevelObject {
        private const string key = nameof(UIManager);
        
        public static UIManager GetForLevel(Level level) {
            level.TryGetLevelObjectFromDictionary(key, out UIManager uiManager);
            return uiManager;
        }
        
        private MainMenuUI _mainMenuUI;
        private LevelUI _levelUI;
        private GameSuccessUI _gameSuccessUI;

        public override void Initialize(Level level) {
            level.AddLevelObjectToDictionary(key, this);
        }

        public override void Prepare(Level level) {
            _mainMenuUI = MainMenuUI.GetForLevel(level);
            _levelUI = LevelUI.GetForLevel(level);
            _gameSuccessUI = GameSuccessUI.GetForLevel(level);
        }

        public void LoadLevelUI() {
            _mainMenuUI.gameObject.SetActive(false);
            _levelUI.gameObject.SetActive(true);
            _gameSuccessUI.gameObject.SetActive(false);
        }
        
        public void LoadGameSuccessUI() {
            _mainMenuUI.gameObject.SetActive(false);
            _levelUI.gameObject.SetActive(false);
            _gameSuccessUI.gameObject.SetActive(true);
        }
        
        public override void OnLevelFinish(Level level) { }
    }
}

