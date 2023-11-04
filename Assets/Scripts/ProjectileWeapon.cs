using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    [SerializeField] private Projectile myBullet;
    [SerializeField] private Projectile myBullet2;
    protected override void Attack(float percent)
    {
        print(message: "My weapon attacked " + percent);
        Ray camRay = InputManager.GetCameraRay();
        Projectile rb = Instantiate(percent > 0.5f?myBullet2:myBullet, camRay.origin, transform.rotation);
        rb.Init(percent,camRay.direction);
        Destroy(rb.gameObject, 5);
    }

    
    
}
