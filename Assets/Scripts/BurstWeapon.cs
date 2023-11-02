using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstWeapon : WeaponBase
{
    [SerializeField] private Rigidbody myBullet;
    [SerializeField] private float force = 50;
    [SerializeField] float spread;
    
    protected override void Attack(float percent)
    {
        print(message: "My weapon attacked " + percent);
        Ray camRay = InputManager.GetCameraRay();
        for (int i = 0; i < 5; i++)
        {
            Vector3 dir = camRay.direction + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
            Rigidbody rb = Instantiate(myBullet, camRay.origin, transform.rotation);
            rb.AddForce(force * dir, ForceMode.Impulse);
            Destroy(rb.gameObject, 3);
        }
    }



}
