using System;
using UnityEngine;

public interface IInputSystem {
    public Action OnJump { get; set; }
    Vector2 GetMoveVector();
    Vector2 GetMousePosition();
}

