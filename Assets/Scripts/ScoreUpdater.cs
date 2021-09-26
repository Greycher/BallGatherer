using System;
using BallGatherer;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : LevelObject {
    public Text Text;
    public string format = "Score: {0}";
    
    private LevelManager _levelManager;

    public override void Initialize(Level level) { }

    public override void Prepare(Level level) {
        _levelManager = LevelManager.GetForLevel(level);
    }

    private void Update() {
        if (_levelManager != null) {
            Text.text = String.Format(format, Mathf.RoundToInt(_levelManager.Progress * 100));
        }
    }

    public override void OnLevelFinish(Level level) { }
}
