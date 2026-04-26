using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    public static PlayerMovement Instance { get; private set; }

    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private float _groundPointRadius;

    [Header("Movement Speed")]
    [SerializeField] private float _speed;

    private Vector3 _movement;
    private Rigidbody2D _rigidBody;

    private bool _isFacingRight = false;
    private bool _isGrounded = true;
    private bool _canInteract = true;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start() {
        GameInputManager.Instance.OnJump += OnJump;
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (_canInteract) Move();
    }

    private void Update() {
        Reflect();
        CheckIfOnGround();
    }

    private void OnJump() {
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

    public bool GetIsGrounded() => _isGrounded;
    public bool CanInteract() => _canInteract;
    public bool GetIsRunning() => _movement.x != 0;
    
    public void StopInteraction() => _canInteract = false;
    public void StartInteraction() => _canInteract = true;
}

