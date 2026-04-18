using System.Collections;
using UnityEngine;

public class PointMoving : MonoBehaviour {
    [SerializeField] private Transform[] _movingPoints;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _movingSpeed;

    private Transform _targetPoint;
    private int _currentIndex;
    private bool _isDirectionForward = true;
    private bool _isWaiting = false;
    private float _gizomsRadius = 0.5f;

    private void Awake() {
        if (_movingPoints.Length > 0) {
            transform.position = _movingPoints[0].position;
            SetNextTarget(); 
        }
    }

    private void FixedUpdate() {
        if (_movingPoints.Length < 2 || _isWaiting) return;
        MoveToPoints();
    }

    private void MoveToPoints() {
        if (Vector2.Distance(transform.position, _targetPoint.position) > 0.01f) {
            transform.position = Vector2.MoveTowards(
                transform.position, 
                _targetPoint.transform.position,  
                _movingSpeed * Time.deltaTime
            );
        } else {
            transform.position = _targetPoint.position;
            StartCoroutine(ChangeTarget());
        }
    }

    private IEnumerator ChangeTarget() {
        _isWaiting = true;

        yield return new WaitForSeconds(_cooldown);

        SetNextTarget();
        _isWaiting = false;
    }

    private void SetNextTarget() {
        if (_isDirectionForward) {
            _currentIndex++;
            if (_currentIndex >= _movingPoints.Length - 1) {
                _currentIndex = _movingPoints.Length - 1;
                _isDirectionForward = false;
            }
        } else {
            _currentIndex--;
            if (_currentIndex <= 0) {
                _currentIndex = 0;
                _isDirectionForward = true;
            }
        }

        _targetPoint = _movingPoints[_currentIndex];
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;

        for (int i = 0; i < _movingPoints.Length; i++) {
            int nextIndex = i + 1;

            Gizmos.DrawWireSphere(_movingPoints[i].position, _gizomsRadius);

            if (nextIndex < _movingPoints.Length) {
                Gizmos.DrawLine(_movingPoints[i].position, _movingPoints[nextIndex].position);
            }
        }
    }
}
