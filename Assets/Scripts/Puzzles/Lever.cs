using System;
using UnityEngine;

public class Lever : MonoBehaviour {
    public event Action OnActivate;
    
    [SerializeField] private Obstacle _obstacle;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out PlayerMovement _)) {
            _obstacle.Hide();
            OnActivate?.Invoke();
        }
    }
}
