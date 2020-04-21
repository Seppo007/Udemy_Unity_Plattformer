using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour {
    public Image overlay;

    private IEnumerator performFading(float delay, float targetAlpha, bool revertToSaveGame) {
        if (delay > 0f) yield return new WaitForSeconds(delay);

        overlay.CrossFadeAlpha(targetAlpha, 1f, false);

        yield return new WaitForSeconds(1f);

        if (revertToSaveGame) {
            SaveGameData.current = SaveGameData.load();
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.loadScene(SaveGameData.current.currentScene);
        }
    }

    public void Awake() {
        SceneManager.sceneLoaded += onLevelLoaded;
        overlay.gameObject.SetActive(true);
    }

    public void OnDestroy() {
        SceneManager.sceneLoaded -= onLevelLoaded;
    }

    public void fadeIn(bool revertToSaveGame, float delay = 0f) {
        StartCoroutine(performFading(delay, 0f, revertToSaveGame));
    }

    public void fadeOut(bool revertToSaveGame, float delay = 0f) {
        StartCoroutine(performFading(delay, 1f, revertToSaveGame));
    }

    private void onLevelLoaded(Scene scene, LoadSceneMode mode) {
        fadeIn(false);
    }
}
