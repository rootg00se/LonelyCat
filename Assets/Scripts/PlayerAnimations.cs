using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    private const string IS_RUNNING = "IsRunning";
    private const string ON_JUMPED = "Jumped";
    private const string IS_GROUNDED = "IsGrounded";
    private const string ON_ELECTRIFY = "Electrify";

    private Animator _animator;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
    } 

    private void Start() {
        GameInputManager.Instance.OnJump += OnJumpPerformed;
        PlayerElectricity.Instance.OnElectrification += OnElectrificationPerformed;
    }

    private void Update() {
        _animator.SetBool(IS_GROUNDED, PlayerMovement.Instance.GetIsGrounded());
        _animator.SetBool(IS_RUNNING, PlayerMovement.Instance.GetIsRunning());
    }

    private void OnJumpPerformed() {
        _animator.SetTrigger(ON_JUMPED);
    }

    private void OnElectrificationPerformed() {
        _animator.SetTrigger(ON_ELECTRIFY);
    }
}
