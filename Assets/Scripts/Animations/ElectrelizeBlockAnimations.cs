using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ElectrelizeBlockAnimations : MonoBehaviour {
   private const string ON_CHARGE = "Charge";

    [SerializeField] private ElectricBlock _electrelizeBlock;

    private Animator _animator;

    private void Start() {
        _animator = gameObject.GetComponent<Animator>();

        _electrelizeBlock.OnCharge += OnCharge;
    }

    private void OnCharge() => _animator.SetTrigger(ON_CHARGE);
}
