using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CrumblingPlatform : MonoBehaviour, IShakeProvider {
    public event Action OnCrumble;
    public event Action OnRestore;

    public event Action OnShakeStart;
    public event Action OnShakeEnd;

    [Header("Platform Settings")]
    [SerializeField] private float _destroyDelay;
    [SerializeField] private float _restoreDelay;
    [SerializeField] private float _canInteractDelay;

    private Collider2D _collider;

    private void Awake() {
        _collider = gameObject.GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out PlayerMovement _)) {
            if (collision.contacts[0].normal.y < -0.5f) {
                OnCrumble?.Invoke();
                OnShakeStart?.Invoke();

                StartCoroutine(Crumble());
            }
        }
    }

    private IEnumerator Crumble() {
        yield return new WaitForSeconds(_destroyDelay);
        OnShakeEnd?.Invoke();

        _collider.enabled = false;

        yield return new WaitForSeconds(_restoreDelay);
        OnRestore?.Invoke();

        yield return new WaitForSeconds(_canInteractDelay);
        
        _collider.enabled = true;
    }
}