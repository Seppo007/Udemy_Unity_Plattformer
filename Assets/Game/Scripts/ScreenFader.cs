using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour {
    public Image overlay;

    private IEnumerator performFading(float targetAlpha, bool revertToSaveGame) {
        overlay.CrossFadeAlpha(targetAlpha, 1f, false);

        yield return new WaitForSeconds(1f);

        if (revertToSaveGame) {
            SaveGameData.current = SaveGameData.load();
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.loadScene(SaveGameData.current.currentScene);
        }
    }

    public void Awake() {
        SceneManager.sceneLoaded += OnLevelWasLoaded;
        overlay.gameObject.SetActive(true);
    }

    public void OnDestroy() {
        SceneManager.sceneLoaded -= OnLevelWasLoaded;
    }

    public void fadeIn(bool revertToSaveGame) {
        StartCoroutine(performFading(0f, revertToSaveGame));
    }

    public void fadeOut(bool revertToSaveGame) {
        StartCoroutine(performFading(1f, revertToSaveGame));
    }

    private void OnLevelWasLoaded(Scene scene, LoadSceneMode mode) {
        fadeIn(false);
    }
}
