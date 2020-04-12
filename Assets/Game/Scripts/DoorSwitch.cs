using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour {
    public Animator doorAnimator;
    public MeshRenderer lightBulbMesh;

    private void Awake() {
        SaveGameData.onSave += saveMe;
        SaveGameData.onLoad += loadMe;
    }

    private void OnDestroy() {
        SaveGameData.onLoad -= loadMe;
        SaveGameData.onSave -= saveMe;
    }

    private void saveMe(SaveGameData savegame) {
        savegame.doorIsOpen = doorAnimator.GetBool("isOpen");
    }

    private void loadMe(SaveGameData savegame) {
        if (savegame.doorIsOpen) openTheDoor();
    }

    private void OnTriggerStay(Collider other) {
        if (Input.GetAxisRaw("Fire1") != 0f && !doorAnimator.GetBool("isOpen")) {
            openTheDoor();
        }
    }

    private void openTheDoor() {
        doorAnimator.SetBool("isOpen", true);
        lightBulbMesh.materials = switchLights(lightBulbMesh.materials);
    }

    private Material[] switchLights(Material[] oldMaterials) {
        Material[] switchedMaterials = lightBulbMesh.materials;
        Material lightOn = switchedMaterials[1];
        Material lightOff = switchedMaterials[2];

        switchedMaterials[1] = lightOff;
        switchedMaterials[2] = lightOn;

        return switchedMaterials;
    }
}
