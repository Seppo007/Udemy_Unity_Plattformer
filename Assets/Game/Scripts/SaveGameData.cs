using System.Collections.Generic;
using UnityEngine;
using System.IO;
using GameDevProfi.Utils;

/// <summary>
/// Datenobjekt zum Speichern und Laden von Speicherdaten
/// </summary>
[System.Serializable]
public class SaveGameData {
    // Player Data
    public Vector3 playerPosition = Vector3.zero;
    public float playerHealth = 1f;
    public HashSet<string> disabledHealthOrbs = new HashSet<string>();

    // Metadata
    public string lastSaveGameTriggerID = "";
    public string currentScene = "";

    // Entity Data
    public bool doorIsOpen = false;
    public Vector3 barrelPosition = new Vector3(0f, 3.6f, 12.9f);
    public Vector3 barrelRotation = new Vector3(0f, 90f, 0f);


    public static SaveGameData current = new SaveGameData();

    public delegate void SaveHandler(SaveGameData savegame);
    public static event SaveHandler onSave;
    public static event SaveHandler onLoad;

    public SaveGameData() {
        currentScene = "Scene1";
    }

    private static string getFilename() {
        return Application.persistentDataPath + Path.DirectorySeparatorChar + "savegame.xml";
    }

    public void save() {
        Debug.Log("Speichere Spielstand " + getFilename());

        if (onSave != null) onSave(this);

        string xml = XML.Save(this);
        File.WriteAllText(getFilename(), xml);
    }

    public static SaveGameData load() {
        if (!File.Exists(getFilename())) return new SaveGameData();
        Debug.Log("Lade Spielstand " + getFilename());

        SaveGameData save = XML.Load<SaveGameData>(File.ReadAllText(getFilename()));

        if (onLoad != null) onLoad(save);

        return save;
    }

}
