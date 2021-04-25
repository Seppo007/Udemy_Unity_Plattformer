using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCatcher : MonoBehaviour
{
    public virtual void onHitByBullet()
    {
        Debug.Log(gameObject.name + " wurde von einer Kugel getroffen");
    }
}
