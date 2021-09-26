using BallGatherer;

public class InputManager : LevelObject {
    protected IController _controller;
    
    public override void Initialize(Level level) { }

    public override void Prepare(Level level) { }

    public void AssignController(IController controller) {
        _controller = controller;
    }
    
    public override void OnLevelFinish(Level level) { }
}
