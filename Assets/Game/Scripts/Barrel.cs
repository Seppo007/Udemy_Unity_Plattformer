using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : Saveable {

    protected override void saveMe(SaveGameData savegame) {
        base.saveMe(savegame);
        savegame.barrelPosition = transform.position;
        savegame.barrelRotation = transform.rotation.eulerAngles;
    }

    protected override void loadMe(SaveGameData savegame) {
        base.loadMe(savegame);
        transform.position = savegame.barrelPosition;
        Vector3 rot = savegame.barrelRotation;
        transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
    }
}
