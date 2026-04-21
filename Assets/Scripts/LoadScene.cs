using UnityEngine;

public class LoadScene : MonoBehaviour {
    private const string ON_DEATH = "Death";
    private Animator _animator;

    private void Start() {
        PlayerDeath.Instance.OnDeath += OnDeathPerformed;
        _animator = GetComponent<Animator>();
    }

    private void OnDeathPerformed() {
        _animator.SetTrigger(ON_DEATH);
    }
}
