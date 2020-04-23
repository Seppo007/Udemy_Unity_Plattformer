using UnityEngine;
using Cinemachine;

public class Player : Saveable {
    public float speed = 0.05f;
    public float jumpPush = 1f;
    public float extraGravity = 20f;
    public float health = 1f;
    public GameObject model;
    public GameObject cameraTarget;

    private float targetYRotation;
    private bool onGround;
    private Rigidbody rb;

    private Animator anim;

    private RaycastHit hitInfo;

    // Called once when scene is loaded
    protected override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        setRagdollMode(false);
    }

    protected override void Awake() {
        base.Awake();
        CinemachineVirtualCamera cvc = FindObjectOfType<CinemachineVirtualCamera>();
        if (cvc != null) {
            cvc.Follow = transform;
            cvc.LookAt = cameraTarget.transform;
        }
    }

    // Update is called once per frame
    private void Update() {
        if (Time.timeScale != 0f) {
            setAnimatorParameters();
            if (transform.position.y < -2.5f) {
                killPlayer();
            }
        }
    }

    private void killPlayer() {
        enabled = false;
        setRagdollMode(true);
        CinemachineVirtualCamera cvc = FindObjectOfType<CinemachineVirtualCamera>();
        if (cvc != null) {
            cvc.Follow = null;
            cvc.LookAt = null;
        }
        ScreenFader screenFader = FindObjectOfType<ScreenFader>();
        screenFader.fadeOut(true, 1.5f);
    }

    protected override void saveMe(SaveGameData savegame) {
        base.saveMe(savegame);
        savegame.playerPosition = transform.position;
        savegame.playerHealth = health;
        savegame.currentScene = gameObject.scene.name;
    }

    protected override void loadMe(SaveGameData savegame) {
        base.loadMe(savegame);
        if (savegame.currentScene == gameObject.scene.name) {
            transform.position = savegame.playerPosition;
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
            health = Mathf.Clamp01(savegame.playerHealth);
        }
    }

    // Helper methods for render update
    private void setAnimatorParameters() {
        anim.SetFloat("forward", Mathf.Abs(Input.GetAxis("Horizontal")));
        anim.SetBool("grounded", onGround);
    }

    // Update of physics in constant time delta
    private void FixedUpdate() {
        if (Time.timeScale != 0f) {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float playerJump = Input.GetAxis("Jump");

            MovePlayer(horizontalMovement);
            RotatePlayerToMovementDirection(horizontalMovement);
            JumpPlayer(playerJump);

            CalculatePlayerOnGround();
        }
    }

    // Helper methods for physics update
    private void MovePlayer(float horizontalMovement) {
        transform.position += speed * transform.forward * horizontalMovement;
        if (onGround && Vector3.Angle(Vector3.up, hitInfo.normal) > 10f) {
            rb.AddForce(hitInfo.normal);
        }
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
        onGround = Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.25f);
    }

    private void setRagdollMode(bool playerDead) {
        // GetComponensInChildren also affects component on root
        foreach (Collider collider in GetComponentsInChildren<Collider>()) {
            collider.enabled = playerDead;
        }

        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>()) {
            rigidbody.isKinematic = !playerDead;
        }

        // set the right state for Collider and Rigidbody after all were activated / deactivated before
        GetComponent<Rigidbody>().isKinematic = playerDead;
        GetComponent<Collider>().enabled = !playerDead;
        GetComponentInChildren<Animator>().enabled = !playerDead;
    }

    public void looseHealth() {
        health -= 0.5f;
        if (health <= 0) killPlayer();
    }
}
