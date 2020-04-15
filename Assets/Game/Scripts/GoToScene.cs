using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToScene : MonoBehaviour {
    public string scene = "";

    private void OnTriggerEnter(Collider other) {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.loadScene(scene);
    }
}
