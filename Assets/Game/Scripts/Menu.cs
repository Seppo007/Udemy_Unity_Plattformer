using UnityEngine;

public class Menu : MonoBehaviour {
    private bool keyWasPressed = false;
    public GameObject menuRoot;

    // Start is called before the first frame update
    void Start() {
        menuRoot.SetActive(false);
    }

    public void onButtonNew() {
        SaveGameData.current = new SaveGameData();
        FindObjectOfType<LevelManager>().loadScene("Scene1");
        menuRoot.SetActive(false);
        Time.timeScale = 1f;
    }

    public void onButtonQuit() {
        Application.Quit();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxisRaw("Menu") > 0f) {
            if (!keyWasPressed) {
                menuRoot.SetActive(!menuRoot.activeSelf);
                Time.timeScale = menuRoot.activeSelf ? 0f : 1f;
            }
            keyWasPressed = true;
        }
        else {
            keyWasPressed = false;
        }
    }
}
