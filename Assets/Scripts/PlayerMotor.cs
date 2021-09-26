using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallGatherer {
    public class PlayerMotor : CharacterMotor {
        private const string key = nameof(PlayerMotor);

        public static PlayerMotor GetForLevel(Level level) {
            level.TryGetLevelObjectFromDictionary(key, out PlayerMotor playerMotor);
            return playerMotor;
        }

        public override void Initialize(Level level) {
            base.Initialize(level);
            level.AddLevelObjectToDictionary(key, this);
        }
    }
}

