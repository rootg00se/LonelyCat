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
    private bool _lastPhysicalGroundState;

    private bool _isLevitating = false;
    private float _defaultGravityScale;

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
            _rigidBody.linearVelocity = new Vector2(_rigidBody.linearVelocity.x, _jumpForce);
        }
    }

    private void Move() {
        _movement = GameInputManager.Instance.GetMovementVector();
        transform.position += _movement * _speed * Time.deltaTime;
    }

    private void CheckIfOnGround() {
        bool detectedGround = Physics2D.OverlapCircle(_groundPoint.position, _groundPointRadius, _groundLayer);

        if (_isLevitating)  {
            _isGrounded = false;
            _lastPhysicalGroundState = detectedGround;

            return;
        }   

        _isGrounded = detectedGround;
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

    public bool GetIsGrounded() => _isLevitating ? _lastPhysicalGroundState : _isGrounded;
    public bool GetIsLevitating() => _isLevitating;
    public bool CanInteract() => _canInteract;
    public bool GetIsRunning() => _movement.x != 0;
    
    public void StopInteraction() => _canInteract = false;
    public void StartInteraction() => _canInteract = true;
}

