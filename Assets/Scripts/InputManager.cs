using System.Collections;
using System.Collections.Generic;
using BallGatherer;
using UnityEngine;

public class InputManager : MonoBehaviour {
    protected IController _controller;

    public void AssignController(IController controller) {
        _controller = controller;
    }
}
