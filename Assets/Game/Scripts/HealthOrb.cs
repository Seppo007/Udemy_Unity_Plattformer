using UnityEngine;

public class HealthOrb : Saveable {
    public string ID = "";

    private void OnTriggerEnter(Collider other) {
        Player player = other.gameObject.GetComponent<Player>();
        if (player) {
            player.health += 0.25f;
            gameObject.SetActive(false);
        }
    }

    protected override void Start() {
        base.Start();
        if (ID == "") {
            Debug.LogError("Entity " + gameObject.name + " has no ID!");
        }
    }

    protected override void saveMe(SaveGameData savegame) {
        base.saveMe(savegame);
        if (!gameObject.activeSelf) {
            savegame.disabledHealthOrbs.Add(ID);
        }
    }

    protected override void loadMe(SaveGameData savegame) {
        base.loadMe(savegame);
        if (savegame.disabledHealthOrbs.Contains(ID)) gameObject.SetActive(false);
    }
}
