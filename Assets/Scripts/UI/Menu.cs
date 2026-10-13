using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    [SerializeField] private int levelIndex;

    public void Play() {
        SceneManager.LoadScene(levelIndex);
    }

    public void Quit() {
        Application.Quit();
    }
}
