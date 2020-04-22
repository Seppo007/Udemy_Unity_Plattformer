using UnityEngine;
using System.IO;
using GameDevProfi.Utils;

/// <summary>
/// Datenobjekt zum Speichern und Laden von Speicherdaten
/// </summary>
[System.Serializable]
public class SaveGameData {
    public Vector3 playerPosition = Vector3.zero;
    public float playerHealth = 1f;
    public bool doorIsOpen = false;
    public string lastSaveGameTriggerID = "";
    public string currentScene = "";
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
