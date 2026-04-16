using UnityEngine;

public class Obstacle : MonoBehaviour {
    [SerializeField] private GameObject _obstacleObject;

    public void Show() {
        _obstacleObject.SetActive(true);
    }

    public void Hide() {
        _obstacleObject.SetActive(false);
    }
}
