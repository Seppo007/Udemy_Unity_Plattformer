using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainer : BulletCatcher
{
    public override void onHitByBullet()
    {
        base.onHitByBullet();
        Debug.Log("Trainer zerstört");
        Destroy(gameObject);
    }
}
