using UnityEngine;

public class Danger : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        Player player = collision.gameObject.GetComponent<Player>();
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.magnitude > 0.65f) {
            if (player) {
                player.looseHealth();
            }
        }
        else if (Time.realtimeSinceStartup > 2f) {
            this.enabled = false;
        }
    }
}
