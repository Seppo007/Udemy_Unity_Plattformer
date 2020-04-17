using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private void Awake() {
        SaveGameData.current = SaveGameData.load();
    }

    private void Start() {
        loadScene(SaveGameData.current.currentScene);
    }

    public void loadScene(string name) {
        if (name == "")
            return;

        for (int i = SceneManager.sceneCount - 1; i > 0; i--) {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name);
        }

        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        Debug.Log("Loading scene " + name);
    }
}
