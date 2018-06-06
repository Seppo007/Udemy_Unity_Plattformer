using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour {

    public Animator doorAnimator;



    private void OnTriggerStay(Collider other) {
        if (Input.GetAxisRaw("Fire1") != 0f) {
            OpenDoor();
        }
    }

    private void OpenDoor() {
        doorAnimator.SetBool("isOpen", true);
    }
}
