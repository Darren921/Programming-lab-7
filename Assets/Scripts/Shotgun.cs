using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponBase
{
    [SerializeField] private Projectile myBullet;
    [SerializeField] float spread;
    
    protected override void Attack(float percent)
    {
        print(message: "My weapon attacked " + percent);
        for (int i = 0; i < 5; i++)
        {
            Ray camRay = InputManager.GetCameraRay();
            Vector3 dir = camRay.direction + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
            Projectile rb = Instantiate(myBullet, camRay.origin, transform.rotation);
            rb.Init(percent, dir );
            Destroy(rb.gameObject, 5    );
        }
    }



}
