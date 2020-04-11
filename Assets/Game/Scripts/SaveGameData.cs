using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using GameDevProfi.Utils;

/// <summary>
/// Datenobjekt zum Speichern und Laden von Speicherdaten
/// </summary>
[System.Serializable]
public class SaveGameData {
    public Vector3 playerPosition = Vector3.zero;
    private static string getFilename() {
        return Application.persistentDataPath + Path.DirectorySeparatorChar + "savegame.xml";
    }

    public void save() {
        Debug.Log("Speichere Spielstand " + getFilename());

        Player player = Component.FindObjectOfType<Player>();
        playerPosition = player.transform.position;

        string xml = XML.Save(this);
        File.WriteAllText(getFilename(), xml);
    }

    public static SaveGameData load() {
        Debug.Log("Lade Spielstand " + getFilename());

        SaveGameData save = XML.Load<SaveGameData>(File.ReadAllText(getFilename()));

        Player player = Component.FindObjectOfType<Player>();
        player.transform.position = save.playerPosition;

        return save;
    }

}
