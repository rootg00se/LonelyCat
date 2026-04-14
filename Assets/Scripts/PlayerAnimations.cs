using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    private const string IS_RUNNING = "IsRunning";
    private const string JUMPED = "Jumped";
    private const string IS_GROUNDED = "IsGrounded";

    private Animator _animator;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
    } 

    private void Start() {
        GameInputManager.Instance.OnJump += OnJumpPerformed;
    }

    private void Update() {
        _animator.SetBool(IS_GROUNDED, PlayerMovement.Instance.GetIsGrounded());
        _animator.SetBool(IS_RUNNING, PlayerMovement.Instance.GetIsRunning());
    }

    private void OnJumpPerformed() {
        _animator.SetTrigger(JUMPED);
    }
}
