using UnityEngine;

public class SaveGameTrigger : MonoBehaviour {

    public string ID = "";

    private void OnTriggerEnter(Collider other) {
        SaveGameData savegame = SaveGameData.current;
        if (savegame.lastTriggerID != ID) {
            savegame.lastTriggerID = ID;
            savegame.Save();
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if (UnityEditor.Selection.activeGameObject != this.gameObject) {
            Gizmos.color = Color.magenta;
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
            Gizmos.matrix = oldMatrix;
        }
    }
#endif
}
