using UnityEngine;

public class Barrel : Saveable {
    public string ID;

    protected override void Start() {
        base.Start();
        if (ID == "") {
            Debug.LogError("Entity " + gameObject.name + " has no ID!");
        }
    }

    protected override void saveMe(SaveGameData savegame) {
        base.saveMe(savegame);
        SaveGameData.BarrelData barrelData = savegame.findBarrelDataByID(ID);
        if (barrelData == null) {
            barrelData = new SaveGameData.BarrelData();
            savegame.barrelData.Add(barrelData);
        }
        barrelData.ID = ID;
        barrelData.position = transform.position;
        barrelData.rotation = transform.rotation.eulerAngles;
    }

    protected override void loadMe(SaveGameData savegame) {
        base.loadMe(savegame);
        SaveGameData.BarrelData data = savegame.findBarrelDataByID(ID);
        if (data != null) {
            Debug.Log("Data is " + data.position);
            transform.position = data.position;
            Vector3 rot = data.rotation;
            transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
        }
    }
}
