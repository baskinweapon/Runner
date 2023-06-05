using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputSystem : IInputSystem {
    private InputActions _playerInput;

    public Action OnJump { get; set; }

    public InputSystem() {
        
        _playerInput = new InputActions();
        _playerInput.Enable();
        
        _playerInput.Player.Jump.performed += Jump;
    }

    #region CallbackFunctions

    private void Jump(InputAction.CallbackContext ctx) {
        if (IsClickOnUI()) return;
        OnJump?.Invoke();
    }
    
    #endregion

    private bool IsClickOnUI() {
        var pointerEventData = new PointerEventData(EventSystem.current) {
            position = _playerInput.UI.Point.ReadValue<Vector2>()
        };
             
        var raycastResultsList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);
             
        return raycastResultsList.Any(result => result.gameObject is GameObject);
    }
    
    public Vector2 GetMoveVector() {
        return _playerInput.Player.Move.ReadValue<Vector2>();;
    }

    public Vector2 GetMousePosition() {
        return _playerInput.UI.Point.ReadValue<Vector2>();
    }
    
    public InputSystem GetInputSystem() {
        return this;
    }

    public void OnDestroy() {
        _playerInput.Player.Jump.performed -= Jump;
    }
}
