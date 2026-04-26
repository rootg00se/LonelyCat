using UnityEngine;

public abstract class ElectricitySource : MonoBehaviour {
    public enum SourceType { Charger, Discharger };
    public abstract string  AnimationTrigger { get; }

    [SerializeField] protected SourceType _panelType;

    public abstract void Interact();
    public SourceType GetPanelType() => _panelType;
}
