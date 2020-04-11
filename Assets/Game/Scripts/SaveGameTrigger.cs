using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Auslöser für automatischen Speicherpunkt.
/// </summary>
public class SaveGameTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        SaveGameData saveGame = new SaveGameData();
        saveGame.save();
    }

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

}
