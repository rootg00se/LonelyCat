using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RatAnimations : MonoBehaviour {
    private const string IS_PATROLLING = "IsPatrolling";

    [SerializeField] private PointMoving _pointMoving;
    private Animator _animator;

    private void Start() {
        _animator = gameObject.GetComponent<Animator>();

        _pointMoving.OnPatrolling += OnPatrolling;
        _pointMoving.OnWaiting += OnWaiting;

        _animator.SetBool(IS_PATROLLING, true);
    }

    private void OnPatrolling() => _animator.SetBool(IS_PATROLLING, true);
    private void OnWaiting() => _animator.SetBool(IS_PATROLLING, false);
}
