using System;
using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour {
    public event Action OnShoot;

    [SerializeField] private float _reloadTime;
    [SerializeField] private GameObject _spearPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _detectionRange;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private Vector2 _shootDirection = Vector2.left;
    [SerializeField] private float _observeAngle;

    private bool _isCharged = true;
    private Transform _playerTransform;

    private void Start() {
        _playerTransform = PlayerMovement.Instance.transform;
    }

    private void Update() {
        if (_playerTransform == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);

        if (distanceToPlayer <= _detectionRange && CanSeePlayer()) {
            Shoot();
        } 
    }

    private bool CanSeePlayer() {
        Vector2 directionToPlayer = (_playerTransform.position - transform.position).normalized;
        float angle = Vector2.Angle(_shootDirection, directionToPlayer);

        if (angle < _observeAngle) {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position, 
                directionToPlayer, 
                _detectionRange, 
                _obstacleMask | _playerMask
            );

            if (hit.collider != null) {
                return hit.collider.TryGetComponent(out PlayerMovement _);
            }
        }

        return false;
    }

    private void Shoot() {
        if (_isCharged) {
            GameObject spearTransform = Instantiate(_spearPrefab, _spawnPoint.position, _spawnPoint.rotation);

            if (spearTransform.TryGetComponent(out Spear spear)) {
                spear.Launch(_shootDirection.x);
            }

            _isCharged = false;
            OnShoot?.Invoke();

            StartCoroutine(ReloadTurret());
        }
    }

    private IEnumerator ReloadTurret() {
        yield return new WaitForSeconds(_reloadTime);

        _isCharged = true;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, _detectionRange);
        Gizmos.DrawRay(transform.position, _shootDirection * _detectionRange);
    }
}
