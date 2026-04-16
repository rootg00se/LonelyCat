using UnityEngine;

public class LeverAnimation : MonoBehaviour {
    private const string SET_ACTIVATE = "Activate";
    
    [SerializeField] private Lever _lever;
    private Animator _animator;

    private void Start() {
        _animator = gameObject.GetComponent<Animator>();
        _lever.OnActivate += OnActivatePerformed;
    }

    private void OnActivatePerformed() {
        _animator.SetTrigger(SET_ACTIVATE);
    }
}
