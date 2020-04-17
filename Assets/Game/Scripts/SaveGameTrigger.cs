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

    private void OnDrawGizmos() {
        Utils.drawBoxCollider(this, Color.magenta);
    }
}
