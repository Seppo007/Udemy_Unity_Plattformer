using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour {

    public Animator doorAnimator;
    public MeshRenderer meshRenderer;

    private void OnTriggerStay(Collider other) {
        if (Input.GetAxisRaw("Fire1") != 0f && !doorAnimator.GetBool("isOpen")) {
            OpenDoor();
        }
    }

    private void OpenDoor() {
        doorAnimator.SetBool("isOpen", true);
        Material[] mats = meshRenderer.materials;
        Material lightBulbOff = mats[2];
        mats[2] = mats[1];
        mats[1] = lightBulbOff;
        meshRenderer.materials = mats;
    }
}
