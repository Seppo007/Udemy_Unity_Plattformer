using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0.05f;
    public float jumpPush = 1f;
    public float extraGravity = 20f;
    public GameObject model;

    private float targetYRotation;
    private bool onGround;
    private Rigidbody rb;

    // Called once when scene is loaded
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update() {

    }

    // Update of physics in constant time delta
    private void FixedUpdate() {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float playerJump = Input.GetAxis("Jump");

        MovePlayerHorizontal(horizontalMovement);
        RotatePlayerToMovementDirection(horizontalMovement);
        Jump(playerJump);
    }

    // Helper methods
    private void MovePlayerHorizontal(float horizontalMovement) {
        transform.position += speed * transform.forward * horizontalMovement;
    }

    private void RotatePlayerToMovementDirection(float horizontalMovement) {
        if (horizontalMovement > 0f) {
            targetYRotation = 0f;
        } else if (horizontalMovement < 0f) {
            targetYRotation = -180;
        }

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, Quaternion.Euler(0f, targetYRotation, 0f), Time.deltaTime * 10f);
    }

    private void Jump(float playerJump) {
        if (playerJump > 0f) {
            onGround = calculatePlayerOnGround();
            if (onGround) {
                Vector3 jumpPower = new Vector3(rb.velocity.x, jumpPush, rb.velocity.z);
                rb.velocity = jumpPower;
            }
        }

        rb.AddForce(new Vector3(0f, -extraGravity, 0f));
    }

    private bool calculatePlayerOnGround() {
        RaycastHit hitInfo;
        return Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.12f);
    }
}
