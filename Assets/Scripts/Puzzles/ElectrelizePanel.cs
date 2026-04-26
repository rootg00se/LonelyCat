using UnityEngine;

public class ElectrelizePanel : ElectricitySource {
    public override string AnimationTrigger => "Shock";
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

    public override void Interact() {
        _isUsed = true;
    }
}
 