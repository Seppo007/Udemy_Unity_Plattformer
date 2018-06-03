using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0.05f;

    // Update is called once per frame
    private void Update() {
        transform.position += speed * transform.forward * Input.GetAxis("Horizontal");
    }
}
