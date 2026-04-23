using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TurretAnimations : MonoBehaviour {
    private const string ON_SHOOT = "Shoot";
    private Animator _animator;

    [SerializeField] private Turret _turret;

    private void Start() {
        _animator = gameObject.GetComponent<Animator>();
        _turret.OnShoot += OnShoot;
    }

    private void OnShoot() {
        _animator.SetTrigger(ON_SHOOT);
    }
}
