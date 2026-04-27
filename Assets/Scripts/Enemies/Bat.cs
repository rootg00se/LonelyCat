using UnityEngine;

public class Bat : MonoBehaviour {
    [SerializeField] private float _patrolRadius;
    [SerializeField] private float _speed;

    private Vector2 _defaultPosition;
    private Transform _playerTransform;

    private void Start() {
        _defaultPosition = transform.position;
        _playerTransform = PlayerMovement.Instance.transform;
    }

    private void FixedUpdate() {
        Patrol();
    }

    private void Patrol() {
        float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);
        Vector2 moveToVector = distanceToPlayer <= _patrolRadius ? _playerTransform.position : _defaultPosition;

        float distanceToTarget = Vector2.Distance(transform.position, moveToVector);


        if (distanceToTarget > 0.05f) {
            transform.position = Vector2.MoveTowards(
                transform.position, 
                moveToVector,
                _speed * Time.deltaTime
            );

            Flip(moveToVector.x - transform.position.x);
        }
    }

    private void Flip(float directionX) {
        if (Mathf.Abs(directionX) < 0.1f) return;

        float scaleX = directionX > 0 ? -1 : 1;
        transform.localScale = new Vector3(scaleX, 1, 1);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _patrolRadius);
    }
}
