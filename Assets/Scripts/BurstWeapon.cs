using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstWeapon : WeaponBase
{
    [SerializeField] private Projectile myBullet;
    [SerializeField] float spread;

    protected override void Attack(float percent)
    {

        StartCoroutine(DelayShot(percent));  
        
    }

    IEnumerator DelayShot(float percent)
    {
        print(message: "My weapon attacked " + percent);
        for (int i = 0; i < 3; i++)
        {
            Ray camRay = InputManager.GetCameraRay();
            Projectile rb = Instantiate(myBullet, camRay.origin, transform.rotation);
            yield return new WaitForSeconds(0.2f);
            rb.Init(percent, camRay.direction);
            Destroy(rb.gameObject, 5);
        }
    }

}
