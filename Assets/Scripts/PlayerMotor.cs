using BallGatherer;
using UnityEngine;

public class PlayerMotor : CharacterMotor
{
    public override void Initialize(Level level) { }

    public override void Prepare(Level level) {
        _border = RectangleBorder.GetForLevel(level);
    }
}
