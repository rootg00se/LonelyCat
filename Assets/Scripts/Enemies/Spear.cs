using UnityEngine;

public class Spear : MonoBehaviour {
    [Header("Spear settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;

    private float _direction;
    private bool _isInitialized = false;

    public void Launch(float direction) {
        _direction = direction;
        _isInitialized = true;

        if (direction > 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Destroy(gameObject, _lifeTime);
    }

    private void Update() {
        if (_isInitialized) {
            transform.position += new Vector3(_direction * _speed * Time.deltaTime, 0, 0);
        }
    }
}

