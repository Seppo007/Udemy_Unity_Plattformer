using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    // called once beforce the scene is initialized
    private void Awake() {
        SaveGameData.Load();
    }
}
