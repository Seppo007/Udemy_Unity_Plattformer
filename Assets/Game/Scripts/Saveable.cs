using UnityEngine;

public class Saveable : MonoBehaviour {
    protected virtual void Awake() {
        SaveGameData.onSave += saveMe;
        SaveGameData.onLoad += loadMe;
    }

    protected virtual void Start() {
        loadMe(SaveGameData.current);
    }

    protected virtual void OnDestroy() {
        SaveGameData.onLoad -= loadMe;
        SaveGameData.onSave -= saveMe;
    }

    protected virtual void saveMe(SaveGameData savegame) {
    }

    protected virtual void loadMe(SaveGameData savegame) {
    }
}
