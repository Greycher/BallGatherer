using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfiguration")]
public class GameConfiguration : ScriptableObject {
    public static GameConfiguration GetGameConfiguration() {
        return Resources.Load<GameConfiguration>(nameof(GameConfiguration));
    }
    
    [TagSelectorAttribute] public string playerTag;
}
