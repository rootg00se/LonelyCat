using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CrumblingPlatformAnimations : MonoBehaviour {
    private const string ON_BREAK = "Break";
    private const string ON_RESTORE = "Restore";

    [SerializeField] private CrumblingPlatform _crumblingPlatform;

    private Animator _animator;

    private void Start() {
        _animator = gameObject.GetComponent<Animator>();

        _crumblingPlatform.OnCrumble += OnCrumble;
        _crumblingPlatform.OnRestore += OnRestore;
    }

    private void OnCrumble() => _animator.SetTrigger(ON_BREAK);
    private void OnRestore() => _animator.SetTrigger(ON_RESTORE);
}
