using UnityEngine;

public class Danger : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player) {
            player.looseHealth();
        }
    }
}
