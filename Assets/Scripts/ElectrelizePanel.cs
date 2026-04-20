using UnityEngine;

public class ElectrelizePanel : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out PlayerElectricity playerElectricity)) {
            playerElectricity.SetCanInteractWithPanel(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out PlayerElectricity playerElectricity)) {
            playerElectricity.SetCanInteractWithPanel(false);
        }
    }
}
