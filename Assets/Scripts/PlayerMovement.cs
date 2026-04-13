using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private float _groundPointRadius;

    private Rigidbody2D _rigidBody;
    private bool _isFacingRight = false;
    private bool _isGrounded = true;
    private Vector3 _movement;

    void Awake() {
        GameInputManager.Instance.OnJump += OnJumpPerformed;
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update() {
        Move();
        Reflect();
        CheckIfOnGround();
    }

    private void OnJumpPerformed() {
        if (_isGrounded) {
            _rigidBody.linearVelocity = new Vector2(_rigidBody.linearVelocity.x, _jumpForce);
        }
    }

    private void Move() {
        _movement = GameInputManager.Instance.GetMovementVector();
        transform.position += _movement * _speed * Time.deltaTime;
    }

    private void CheckIfOnGround() {
        _isGrounded = Physics2D.OverlapCircle(_groundPoint.position, _groundPointRadius, _groundLayer);
    }

    private void Reflect() {
        if ((_movement.x > 0 && !_isFacingRight) || (_movement.x < 0 && _isFacingRight)) {
            transform.localScale *= new Vector2(-1, 1);
            _isFacingRight = !_isFacingRight;
        }
    }
}
