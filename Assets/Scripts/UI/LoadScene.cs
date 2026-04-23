using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LoadScene : MonoBehaviour {
    private const string ON_DEATH = "Death";
    private Animator _animator;

    private void Start() {
        PlayerDeath.Instance.OnDeath += OnDeath;
        _animator = gameObject.GetComponent<Animator>();
    }

    private void OnDeath() {
        _animator.SetTrigger(ON_DEATH);
    }
}
