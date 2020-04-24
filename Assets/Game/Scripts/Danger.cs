using UnityEngine;

public class Danger : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        if (this.enabled) {
            Player player = collision.gameObject.GetComponent<Player>();
            Rigidbody rb = GetComponent<Rigidbody>();
            Debug.Log("current magnitude: " + rb.velocity.magnitude);
            if (rb.velocity.magnitude > 0.66f) {
                if (player) {
                    player.looseHealth();
                }
            }
            else if (Time.realtimeSinceStartup > 2f) {
                this.enabled = false;
            }
        }
    }
}
