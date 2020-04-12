using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Auslöser für automatischen Speicherpunkt.
/// </summary>
public class SaveGameTrigger : MonoBehaviour {
    public string saveGameTriggerID = "";

    private void OnTriggerEnter(Collider other) {
        SaveGameData saveGame = SaveGameData.current;

        if (saveGame.lastSaveGameTriggerID != saveGameTriggerID) {
            saveGame.lastSaveGameTriggerID = saveGameTriggerID;
            saveGame.save();
        }
    }

    // Zeichne Gizmoz nur im Unity Editor
#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if (UnityEditor.Selection.activeGameObject != gameObject) {
            Gizmos.color = Color.magenta;
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
            Gizmos.matrix = oldMatrix;
        }
    }
#endif
}
