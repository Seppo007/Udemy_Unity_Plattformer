using UnityEngine;

public class DoorSwitch : MonoBehaviour {

    public Animator doorAnimator;
    public MeshRenderer meshRenderer;

    // called once beforce the scene is initialized
    private void Awake() {
        SaveGameData.OnSave += SaveMe;
        SaveGameData.OnLoad += LoadMe;
    }

    // called when scene gets destroyed or object get removed programmatically
    private void OnDestroy() {
        SaveGameData.OnLoad += LoadMe;
        SaveGameData.OnSave -= SaveMe;
    }

    private void OnTriggerStay(Collider other) {
        if (Input.GetAxisRaw("Fire1") != 0f && !doorAnimator.GetBool("isOpen")) {
            OpenDoor();
        }
    }

    private void OpenDoor() {
        doorAnimator.SetBool("isOpen", true);
        switchLights();
    }

    private void SaveMe(SaveGameData savegame) {
        savegame.doorIsOpen = doorAnimator.GetBool("isOpen");
    }

    private void LoadMe(SaveGameData savegame) {
        doorAnimator.SetBool("isOpen", savegame.doorIsOpen);
        if (savegame.doorIsOpen) {
            switchLights();
        }
    }

    private void switchLights() {
        Material[] mats = meshRenderer.materials;
        Material lightBulbOff = mats[2];
        mats[2] = mats[1];
        mats[1] = lightBulbOff;
        meshRenderer.materials = mats;
    }

}
