using UnityEngine;

public class VisualShake : MonoBehaviour {
    [SerializeField] private float _intensity = 0.05f;
    [SerializeField] private float _speed = 50f;

    private Vector3 _originalLocalPos;
    private bool _isShaking = false;
    private IShakeProvider _provider;

    private void Awake() {
        _originalLocalPos = transform.localPosition;
        _provider = GetComponentInParent<IShakeProvider>();
    }

    private void OnEnable() {
        if (_provider != null) {
            _provider.OnShakeStart += StartShake;
            _provider.OnShakeEnd += StopShake;
        }
    }

    private void OnDisable() {
        if (_provider != null) {
            _provider.OnShakeStart -= StartShake;
            _provider.OnShakeEnd -= StopShake;
        }
    }

    private void Update() {
        if (_isShaking) {
            float offsetX = Mathf.Sin(Time.time * _speed) * _intensity;
            float offsetY = Mathf.Sin(Time.time * _speed * 1.2f) * _intensity;

            transform.localPosition = _originalLocalPos + new Vector3(offsetX, offsetY, 0);
        }
    }

    private void StartShake() {
        _isShaking = true;
    }

    private void StopShake() {
        _isShaking = false;
        transform.localPosition = _originalLocalPos;
    }
}