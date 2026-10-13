using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerLevelEnd : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collision) {
        SceneManager.LoadScene(0);
    }
}
