using System.Collections.Generic;
using UnityEngine;

namespace BallGatherer {
    public class Level : MonoBehaviour {
        public Transform Parent => transform;
        public bool IsPlaying => isPlaying;
        
        private Dictionary<string, LevelObject> _levelObjectDictionary = new Dictionary<string, LevelObject>();
        private List<LevelObject> _levelObjects;
        private bool isPlaying;

        private void Awake() {
            FindLevelObjects();
            InitializeLevelObjects();
            StartLevel();
        }
        
        private void FindLevelObjects() {
            var parent = Parent;
            _levelObjects = new List<LevelObject>(parent.GetComponentsInChildren<LevelObject>(true));
        }

        private void InitializeLevelObjects() {
            for (int i = 0; i < _levelObjects.Count; i++) {
                _levelObjects[i].Initialize(this);
            }
        }

        public void StartLevel() {
            isPlaying = true;
            for (int i = 0; i < _levelObjects.Count; i++) {
                _levelObjects[i].Prepare(this);
            }
        }

        public void AddLevelObjectToDictionary(string key, LevelObject levelObject) {
            _levelObjectDictionary.Add(key, levelObject);
        }

        public bool TryGetLevelObjectFromDictionary<T>(string key, out T t) where T : LevelObject {
            if (_levelObjectDictionary.TryGetValue(key, out LevelObject levelObject)) {
                if (levelObject is T value) {
                    t = value;
                    return true;
                }
            }

            t = null;
            return false;
        }

        public void OnNewLevelObjectSpawn(LevelObject levelObject) {
            levelObject.Initialize(this);
            levelObject.Prepare(this);
            _levelObjects.Add(levelObject);
        }

        public void FinishLevel() {
            isPlaying = false;
            for (int i = 0; i < _levelObjects.Count; i++) {
                _levelObjects[i].OnLevelFinish(this);
            }
        }
    }
}

