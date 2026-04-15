using System;
using UnityEngine;

public class PlayerLife : MonoBehaviour {
    public static PlayerLife Instance { get; private set; }
    public event Action OnDeath;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Instance of PlayerLife already exists");
        }

        Instance = this;
    }

    public void KillPlayer() {
        OnDeath?.Invoke();
        Debug.Log("Player is dead");
    }
}
