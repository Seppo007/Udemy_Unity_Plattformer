using UnityEngine;
using System.IO;
using GameDevProfi.Utils;

[System.Serializable]
public class SaveGameData
{

    public Vector3 playerPosition = Vector3.zero;

    private static readonly char FILE_SEPARATOR = Path.DirectorySeparatorChar;

    private static string GetFilename() {
        return Application.persistentDataPath.Replace('/', FILE_SEPARATOR) + FILE_SEPARATOR + "savegame.xml";
    }

    public void Save() {
        SetPlayerPosition();
        string xml = XML.Save(this);
        File.WriteAllText(GetFilename(), xml);
    }

    private void SetPlayerPosition() {
        Player player = Component.FindObjectOfType<Player>();
        playerPosition = player.transform.position;
    }

}
