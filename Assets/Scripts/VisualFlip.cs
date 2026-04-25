using UnityEngine;

public class VisualFlip : MonoBehaviour {
    [SerializeField] private PointMoving _pointMoving;

    private void Start() {
        _pointMoving.OnDirectionChanged += OnDirectionChanged;
    }

    private void OnDirectionChanged(float directionX) {
        if (directionX > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (directionX < 0) transform.localScale = new Vector3(-1, 1, 1);
    }
}
