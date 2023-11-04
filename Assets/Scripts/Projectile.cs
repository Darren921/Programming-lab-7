using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
     private static WeaponBase Weapon;
    [SerializeField] private float damage;
    [SerializeField] private float shootForce;
    [SerializeField] private Rigidbody rb;
    private float trueDamage;
    public bool hittarget;
    public void Init(float chargePercent, Vector3 fireDirection)
    {
        rb.AddForce(shootForce * chargePercent * fireDirection, ForceMode.Impulse);
        trueDamage = chargePercent * damage;
    }


    private void OnCollisionEnter(Collision other)
    {
        print(other.transform.name + ", " + other.transform.root.name); 
        if(other.transform.root.TryGetComponent(out IDamagable hitTarget))
        {
            switch (other.transform.tag)
            {
                case "Head":
                    trueDamage *=2 ;
                    break;
                case "Limb":
                    trueDamage *= 0.65f;
                    break;
                case "Body":
                    trueDamage *= 0.85f;
                    break;
            }
            print(trueDamage);
            hitTarget.TakeDamage(trueDamage);

        }
        Destroy(gameObject);




    }
}
