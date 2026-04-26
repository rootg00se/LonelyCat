using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour {
    private const string IS_RUNNING = "IsRunning";
    private const string ON_JUMPED = "Jumped";
    private const string IS_GROUNDED = "IsGrounded";

    private Animator _animator;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Start() {
        GameInputManager.Instance.OnJump += OnJump;
        PlayerElectricity.Instance.OnElectrification += OnElectrification;
    }

    private void Update() {
        _animator.SetBool(IS_GROUNDED, PlayerMovement.Instance.GetIsGrounded());
        _animator.SetBool(IS_RUNNING, PlayerMovement.Instance.GetIsRunning());
    }

    private void OnJump() => _animator.SetTrigger(ON_JUMPED);
    private void OnElectrification(string animationTrigger) =>_animator.SetTrigger(animationTrigger);
}
