using UnityEngine;

public class ElectrelizePanel : MonoBehaviour {
    public enum PanelType { Charger, Discharger };

    [SerializeField] private PanelType _panelType;

    private bool _isUsed = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (_isUsed) return;

        if (collision.gameObject.TryGetComponent(out PlayerElectricity playerElectricity)) {
            playerElectricity.SetCurrentPanel(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out PlayerElectricity playerElectricity)) {
            playerElectricity.ResetCurrentPanel();
        }
    }

    public PanelType GetPanelType() => _panelType;
    public void Use() => _isUsed = true;
}
 