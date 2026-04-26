using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlatformDescend : MonoBehaviour {
    [SerializeField] private LayerMask _platformLayer;

    private GameObject _currentPlatform;
    private float _collisionIgnoreTime = 0.25f;

    private void Start() {
        GameInputManager.Instance.OnPlatformDescend += OnPlatformDescend;
    }

    private void OnPlatformDescend() {
        if (_currentPlatform == null) return;

        StartCoroutine(DisableCollision());
    }

    private IEnumerator DisableCollision() {
        Collider2D platformCollider = _currentPlatform.GetComponent<Collider2D>();
        Collider2D playerCollider = gameObject.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider, true);

        yield return new WaitForSeconds(_collisionIgnoreTime);

        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (((1 << collision.gameObject.layer) & _platformLayer) != 0) {
            _currentPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (((1 << collision.gameObject.layer) & _platformLayer) != 0) {
            _currentPlatform = null;
        }
    }
}
