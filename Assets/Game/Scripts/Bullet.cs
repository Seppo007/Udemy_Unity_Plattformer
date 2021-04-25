using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.forward * (transform.rotation.y < 0 ? 5f : -5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        BulletCatcher bulletCatcher = collision.gameObject.GetComponent<BulletCatcher>();
        Destroy(gameObject);
        if (bulletCatcher)
            bulletCatcher.onHitByBullet();
    }
}
