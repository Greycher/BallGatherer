using System.Collections.Generic;
using UnityEngine;

namespace BallGatherer {
    public class Level : MonoBehaviour {
        public Transform Parent => transform;
        
        private Dictionary<string, LevelObject> _levelObjectDictionary = new Dictionary<string, LevelObject>();
        
        private void Awake() {
            InitializeLevelObjects();
        }

        private void InitializeLevelObjects() {
            var parent = Parent;
            var levelObjects = parent.GetComponentsInChildren<LevelObject>();
            for (int i = 0; i < levelObjects.Length; i++) {
                levelObjects[i].Initialize(this);
            }
            for (int i = 0; i < levelObjects.Length; i++) {
                levelObjects[i].Prepare(this);
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
    }
}

