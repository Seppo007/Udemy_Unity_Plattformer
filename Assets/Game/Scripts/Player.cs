using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0.05f;
    public GameObject model;

    private float targetYRotation;

    // Update is called once per frame
    private void Update() {
        float horizontalAxis = Input.GetAxis("Horizontal");
        MovePlayerHorizontal(horizontalAxis);
        RotatePlayerToMovementDirection(horizontalAxis);
    }

    private void MovePlayerHorizontal(float horizontalAxis) {
        transform.position += speed * transform.forward * horizontalAxis;
    }

    private void RotatePlayerToMovementDirection(float horizontalAxis) {
        if (horizontalAxis > 0f) {
            targetYRotation = 0f;
        } else if (horizontalAxis < 0f) {
            targetYRotation = -180f;
        }

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, Quaternion.Euler(0f, targetYRotation, 0f), Time.deltaTime * 10f);
    }
}
