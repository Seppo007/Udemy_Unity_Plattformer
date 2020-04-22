using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Image progressBar;
    private Player player;

    // Update is called once per frame
    private void Update() {
        if (!player) {
            player = FindObjectOfType<Player>();
        }
        else {
            progressBar.fillAmount = player.health;
        }
    }

}
