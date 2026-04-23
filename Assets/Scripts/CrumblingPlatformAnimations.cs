using UnityEngine;

public class CrumblingPlatformAnimations : MonoBehaviour {
    private const string ON_BREAK = "Break";
    private const string ON_RESTORE = "Restore";

    [SerializeField] private CrumblingPlatform _crumblingPlatform;

    private Animator _animator;

    private void Start() {
        _animator = GetComponent<Animator>();

        _crumblingPlatform.OnCrumble += OnCrumblePerformed;
        _crumblingPlatform.OnRestore += OnRestorePerformed;
    }

    private void OnCrumblePerformed() => _animator.SetTrigger(ON_BREAK);
    private void OnRestorePerformed() => _animator.SetTrigger(ON_RESTORE);
}
