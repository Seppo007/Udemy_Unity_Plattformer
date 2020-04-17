using UnityEngine;

public class DoorSwitch : Saveable {
    public Animator doorAnimator;
    public MeshRenderer lightBulbMesh;

    protected override void saveMe(SaveGameData savegame) {
        base.saveMe(savegame);
        savegame.doorIsOpen = doorAnimator.GetBool("isOpen");
    }

    protected override void loadMe(SaveGameData savegame) {
        base.loadMe(savegame);
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
