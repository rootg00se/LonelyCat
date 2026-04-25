using System;
using UnityEngine;

public class PlayerElectricity : MonoBehaviour {
    public static PlayerElectricity Instance { get; private set; }
    public bool IsCharged { get; set; } = false;

    private ElectrelizePanel _currentPanel = null;
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
        if (_currentPanel == null) return;

        bool canCharge = _currentPanel.GetPanelType() == ElectrelizePanel.PanelType.Charger && !IsCharged;
        bool canDischarge = _currentPanel.GetPanelType() == ElectrelizePanel.PanelType.Discharger && IsCharged;

        if (canCharge || canDischarge) {
            ExecuteInteraction();
        }
    }

    public void ExecuteInteraction() {
        IsCharged = !IsCharged;
        _currentPanel.Use();

        OnElectrification?.Invoke();
        PlayerMovement.Instance.StopInteraction();
    }

    public void SetCurrentPanel(ElectrelizePanel panel) => _currentPanel = panel;
    public void ResetCurrentPanel() => _currentPanel = null;
}
