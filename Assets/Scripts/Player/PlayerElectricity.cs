using System;
using UnityEngine;

public class PlayerElectricity : MonoBehaviour {
    public static PlayerElectricity Instance { get; private set; }
    public bool IsCharged { get; set; } = false;

    private ElectricitySource _currentSource = null;
    public event Action<string> OnElectrification;

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
        if (_currentSource is ElectrelizePanel) {
            ExecuteInteraction(_currentSource);
        }
    }

    public void ExecuteInteraction(ElectricitySource source) {
        if (source == null) return;

        bool canCharge = source.GetPanelType() == ElectricitySource.SourceType.Charger && !IsCharged;
        bool canDischarge = source.GetPanelType() == ElectricitySource.SourceType.Discharger && IsCharged;

        if (canCharge || canDischarge) {
            IsCharged = !IsCharged;
            source.Interact();

            OnElectrification?.Invoke(source.AnimationTrigger);            
            PlayerMovement.Instance.StopInteraction();
        }
    }

    public void SetCurrentPanel(ElectricitySource source) => _currentSource = source;
    public void ResetCurrentPanel() => _currentSource = null;
}
