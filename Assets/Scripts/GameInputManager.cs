using System;
using UnityEngine;

public class GameInputManager : MonoBehaviour {
    public static GameInputManager Instance { get; private set; }

    public event Action OnJump;
    public event Action OnInteract;

    private GameInput _gameInput;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There are more than 2 GameInputManager");
        }

        Instance = this;

        _gameInput = new GameInput();
        _gameInput.Player.Enable();

        _gameInput.Player.Jump.performed += JumpPerformed;
        _gameInput.Player.Interact.performed += InteractPerformed;
    }

    private void JumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnJump?.Invoke();
    }

    private void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteract?.Invoke();
    }

    public Vector2 GetMovementVector() {
        float horizontalInput = _gameInput.Player.Movement.ReadValue<float>();
        Vector2 movement = new(horizontalInput, 0);

        return movement;
    }
}
