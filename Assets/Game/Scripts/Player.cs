﻿using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour {
    public float speed = 0.05f;
    public float jumpPush = 1f;
    public float extraGravity = 20f;
    public GameObject model;
    public GameObject cameraTarget;

    private float targetYRotation;
    private bool onGround;
    private Rigidbody rb;

    private Animator anim;

    // Called once when scene is loaded
    private void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        loadMe(SaveGameData.current);
    }

    private void Awake() {
        SaveGameData.onSave += saveMe;
        SaveGameData.onLoad += loadMe;
        CinemachineVirtualCamera cvc = FindObjectOfType<CinemachineVirtualCamera>();
        if (cvc != null) {
            cvc.Follow = transform;
            cvc.LookAt = cameraTarget.transform;
        }
    }

    // Update is called once per frame
    private void Update() {
        setAnimatorParameters();
    }

    private void OnDestroy() {
        SaveGameData.onLoad -= loadMe;
        SaveGameData.onSave -= saveMe;
    }

    private void saveMe(SaveGameData savegame) {
        savegame.playerPosition = transform.position;
        savegame.currentScene = gameObject.scene.name;
    }

    private void loadMe(SaveGameData savegame) {
        if (savegame.currentScene == gameObject.scene.name) {
            transform.position = savegame.playerPosition;
        }
    }

    // Helper methods for render update
    private void setAnimatorParameters() {
        anim.SetFloat("forward", Mathf.Abs(Input.GetAxis("Horizontal")));
        anim.SetBool("grounded", onGround);
    }

    // Update of physics in constant time delta
    private void FixedUpdate() {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float playerJump = Input.GetAxis("Jump");

        MovePlayerAndSetAnimator(horizontalMovement);
        RotatePlayerToMovementDirection(horizontalMovement);
        JumpPlayer(playerJump);

        CalculatePlayerOnGround();
    }

    // Helper methods for physics update
    private void MovePlayerAndSetAnimator(float horizontalMovement) {
        transform.position += speed * transform.forward * horizontalMovement;
    }

    private void RotatePlayerToMovementDirection(float horizontalMovement) {
        if (horizontalMovement > 0f) {
            targetYRotation = 0f;
        }
        else if (horizontalMovement < 0f) {
            targetYRotation = -180;
        }

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, Quaternion.Euler(0f, targetYRotation, 0f), Time.deltaTime * 10f);
    }

    private void JumpPlayer(float playerJump) {
        if (playerJump > 0f) {
            if (onGround) {
                Vector3 jumpPower = new Vector3(rb.velocity.x, jumpPush, rb.velocity.z);
                rb.velocity = jumpPower;
            }
        }

        rb.AddForce(new Vector3(0f, -extraGravity, 0f));
    }

    private void CalculatePlayerOnGround() {
        RaycastHit hitInfo;
        onGround = Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.12f);
    }
}
