using System;
using UnityEngine;

public class GameInputManager : MonoBehaviour {
    public static GameInputManager Instance { get; private set; }

    public event Action OnJump;
    public event Action OnInteract;
    public event Action OnPlatformDescend;

    private GameInput _gameInput;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _gameInput = new GameInput();
        _gameInput.Player.Enable();

        _gameInput.Player.Jump.performed += JumpPerformed;
        _gameInput.Player.Interact.performed += InteractPerformed;
        _gameInput.Player.PlatformDescent.performed += PlatformDescent;
    }

    private void JumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        if (PlayerMovement.Instance != null && PlayerMovement.Instance.CanInteract()) {
            OnJump?.Invoke();
        }
    }

    private void PlatformDescent(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        if (PlayerMovement.Instance != null && PlayerMovement.Instance.CanInteract()) {
            OnPlatformDescend?.Invoke();
        }
    }

    private void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        if (PlayerMovement.Instance != null && PlayerMovement.Instance.CanInteract()) {
            OnInteract?.Invoke();
        }
    }

    public Vector2 GetMovementVector() {
        if (_gameInput == null) return Vector2.zero;

        float horizontalInput = _gameInput.Player.Movement.ReadValue<float>();
        Vector2 movement = new(horizontalInput, 0);

        return movement;
    }

    private void OnDestroy() {
        if (_gameInput != null) {
            _gameInput.Player.Jump.performed -= JumpPerformed;
            _gameInput.Player.Interact.performed -= InteractPerformed;

            _gameInput.Player.Disable();
            _gameInput.Dispose();
        }
    }
}
