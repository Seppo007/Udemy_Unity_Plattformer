using UnityEngine;

public class GoToScene : MonoBehaviour {
    public string scene = "";

    private void OnTriggerEnter(Collider other) {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.loadScene(scene);
    }

    private void OnDrawGizmos() {
        Utils.drawBoxCollider(this, Color.red);
    }
}
