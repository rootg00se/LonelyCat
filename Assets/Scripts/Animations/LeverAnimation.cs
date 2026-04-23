using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LeverAnimation : MonoBehaviour {
    private const string ON_ACTIVATE = "Activate";
    
    [SerializeField] private Lever _lever;
    private Animator _animator;

    private void Start() {
        _animator = gameObject.GetComponent<Animator>();
        _lever.OnActivate += OnActivate;
    }

    private void OnActivate() {
        _animator.SetTrigger(ON_ACTIVATE);
    }
}
