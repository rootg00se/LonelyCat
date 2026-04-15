using UnityEngine;

public class TriggerDeath : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out PlayerLife playerLife)) {
            playerLife.KillPlayer();
        }
    }
}
