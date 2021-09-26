using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfiguration")]
public class GameConfiguration : ScriptableObject {
    public static GameConfiguration Instance {
        get {
            if (_instance == null) {
                _instance = Resources.Load<GameConfiguration>(nameof(GameConfiguration));
            }
            return _instance;
        }
    }
    
    private static GameConfiguration _instance;

    public GameObject ballRes;
    public int ballCount;
}
