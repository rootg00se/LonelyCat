using System;
using UnityEngine;

public class PlayerElectricity : MonoBehaviour {
    public static PlayerElectricity Instance { get; private set; }

    private bool _canInteract = false;
    public event Action OnElectrification;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Instance of PlayerElectricity already exists");
        }

        Instance = this;
    }

    private void Start() {
        GameInputManager.Instance.OnInteract += OnInteractPerformed;
    }

    private void OnInteractPerformed() {
        if (_canInteract) {
            OnElectrification?.Invoke();
        }
    }

    public void SetCanInteract(bool canInteract) {
        _canInteract = canInteract;
    }
}
