using UnityEngine;

public class Water : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out PlayerDeath playerDeath)) {
            if (PlayerElectricity.Instance.IsCharged) {
                playerDeath.KillPlayer();
            }
        }
    }
}
