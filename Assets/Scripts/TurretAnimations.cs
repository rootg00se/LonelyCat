using UnityEngine;

public class TurretAnimations : MonoBehaviour {
    private const string ON_SHOOT = "Shoot";
    private Animator _animator;

    [SerializeField] private Turret _turret;

    private void Start() {
        _animator = GetComponent<Animator>();
        _turret.OnShoot += OnShoot;
    }

    private void OnShoot() {
        _animator.SetTrigger(ON_SHOOT);
    }
}
