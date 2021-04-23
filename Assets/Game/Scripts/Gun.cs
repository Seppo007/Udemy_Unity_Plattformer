using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    private Light fireLight;
    private bool shotDone = true;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponentInParent<Animator>();
        fireLight = GetComponentInChildren<Light>();
        fireLight.enabled = false;
    }

    public void shoot()
    {
        if (shotDone)
            StartCoroutine(doShoot());
    }

    private IEnumerator doShoot()
    {
        shotDone = false;
        playerAnimator.SetTrigger("gunShot");
        fireLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        fireLight.enabled = false;
        shotDone = true;
    }

}
