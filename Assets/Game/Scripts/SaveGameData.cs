using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using GameDevProfi.Utils;

/// <summary>
/// Datenobjekt zum Halten von Speicherdaten
/// </summary>
[System.Serializable]
public class SaveGameData {
    public Vector3 playerPosition = Vector3.zero;
    private static string getFilename() {
        return Application.persistentDataPath + Path.DirectorySeparatorChar + "savegame.xml";
    }

    public void save() {
        Player player = Component.FindObjectOfType<Player>();
        playerPosition = player.transform.position;

        string xml = XML.Save(this);
        File.WriteAllText(getFilename(), xml);
    }

}
