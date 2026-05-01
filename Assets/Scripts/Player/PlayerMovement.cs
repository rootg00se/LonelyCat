using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    public static PlayerMovement Instance { get; private set; }

    [Header("Jump Settings")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private float _groundPointRadius;

    [Header("Default Movement")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;

    [Header("Water Movement")]
    [SerializeField] private float _swimSpeed;
    [SerializeField] private float _swimJumpForce;

    [SerializeField] private GameObject _splashPrefab;

    private Vector3 _movement;
    private Rigidbody2D _rigidBody;

    private bool _isFacingRight = false;
    private bool _isGrounded = true;
    private bool _canInteract = true;

    private bool _isLevitating = false;
    private float _defaultGravityScale;

    private float _currentSpeed;
    private float _currentJumpForce;

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
        _defaultGravityScale = _rigidBody.gravityScale;

        _currentJumpForce = _jumpForce;
        _currentSpeed = _speed;
    }

    private void FixedUpdate() {
        if (_canInteract) Move();
    }

    private void Update() {
        Reflect();
        CheckIfOnGround();
    }

    private void OnJump() {
        if (_isLevitating) return;

        if (_isGrounded) {
            _rigidBody.linearVelocity = new Vector2(_rigidBody.linearVelocity.x, _currentJumpForce);
        }
    }

    private void Move() {
        _movement = GameInputManager.Instance.GetMovementVector();
        transform.position += _movement * _currentSpeed * Time.deltaTime;
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

    public void SetLevitation(bool active) {
        _isLevitating = active;
        _rigidBody.gravityScale = active ? 0 : _defaultGravityScale;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out Water _)) {
            Splash();
            SetSwimParams();
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out Water _)) {
            SetDefaultParams();
        }
    }

    private void Splash() {
        Instantiate(_splashPrefab, transform.position, Quaternion.identity);
    }

    private void SetSwimParams() {
        _currentJumpForce = _swimJumpForce;
        _currentSpeed = _swimSpeed;
    }

    private void SetDefaultParams() {
        _currentJumpForce = _jumpForce;
        _currentSpeed = _speed;
    }

    public bool GetIsGrounded() => _isGrounded;
    public bool GetIsLevitating() => _isLevitating;
    public bool CanInteract() => _canInteract;
    public bool GetIsRunning() => _movement.x != 0;
    
    public void StopInteraction() => _canInteract = false;
    public void StartInteraction() => _canInteract = true;
}

