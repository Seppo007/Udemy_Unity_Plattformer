using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    private bool keyWasPressed = false;

    // Start is called before the first frame update
    void Start() {
        GetComponent<Canvas>().enabled = false;
    }

    public void onButtonNew() {
        SaveGameData.current = new SaveGameData();
        FindObjectOfType<LevelManager>().loadScene("Scene1");
        GetComponent<Canvas>().enabled = false;
    }

    public void onButtonQuit() {
        Application.Quit();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxisRaw("Menu") > 0f) {
            if (!keyWasPressed) {
                Canvas canvas = GetComponent<Canvas>();
                canvas.enabled = !canvas.enabled;
            }
            keyWasPressed = true;
        }
        else {
            keyWasPressed = false;
        }
    }
}
