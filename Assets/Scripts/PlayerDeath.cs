using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour {
    public static PlayerDeath Instance { get; private set; }
    public event Action OnDeath;

    [SerializeField] private GameObject _playerVisual;
    [SerializeField] private GameObject _explosionPrefab;

    private Rigidbody2D _rigidBody;
    private Collider2D _collider;
    private float _reloadSceneTime = 1f;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Instance of PlayerDeath already exists");
        }

        Instance = this;

        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _collider = gameObject.GetComponent<Collider2D>();
    }

    public void KillPlayer() {
        OnDeath?.Invoke();
        Death();
    }

    private void Death() {
        PlayerMovement.Instance.StopInteraction();

        _rigidBody.linearVelocity = Vector2.zero;
        _rigidBody.simulated = false;

        _collider.enabled = false;

        _playerVisual.SetActive(false);

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel() {
        yield return new WaitForSeconds(_reloadSceneTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
