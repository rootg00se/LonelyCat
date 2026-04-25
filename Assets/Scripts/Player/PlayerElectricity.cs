using System;
using UnityEngine;

public class PlayerElectricity : MonoBehaviour {
    public static PlayerElectricity Instance { get; private set; }
    public bool IsCharged { get; set; } = false;

    private bool _canInteractWithPanel = false;
    public event Action OnElectrification;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start() {
        GameInputManager.Instance.OnInteract += OnInteract;
    }

    private void OnInteract() {
        if (_canInteractWithPanel) {
            OnElectrification?.Invoke();
            PlayerMovement.Instance.StopInteraction();
        }
    }

    public void SetCanInteractWithPanel(bool canInteract) {
        _canInteractWithPanel = canInteract;
    }
}
