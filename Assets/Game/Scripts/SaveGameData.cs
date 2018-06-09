using GameDevProfi.Utils;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveGameData {

    public Vector3 playerPosition = Vector3.zero;
    public bool doorIsOpen = false;

    public delegate void SaveHandler(SaveGameData savegame);
    public static event SaveHandler OnSave;
    public static event SaveHandler OnLoad;

    private static readonly char FILE_SEPARATOR = Path.DirectorySeparatorChar;

    public void Save() {
        Player player = getPlayer();
        playerPosition = player.transform.position;
        playerPosition.y = 0f;

        if (OnSave != null) {
            OnSave(this);
        }

        string xml = XML.Save(this);
        File.WriteAllText(GetFilename(), xml);
    }

    public static SaveGameData Load() {
        SaveGameData save = null;
        if (File.Exists(GetFilename())) {
            save = XML.Load<SaveGameData>(File.ReadAllText(GetFilename()));
            Player player = getPlayer();
            player.transform.position = save.playerPosition;
            if (OnLoad != null) {
                OnLoad(save);
            }
        }
        return save;
    }

    private static Player getPlayer() {
        return Component.FindObjectOfType<Player>();
    }

    private static string GetFilename() {
        return Application.persistentDataPath.Replace('/', FILE_SEPARATOR) + FILE_SEPARATOR + "savegame.xml";
    }
}
