using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour, IDamagable
{
    [field: SerializeField] public float Health { get; set; }
    [SerializeField] private float speed;
    private Rigidbody rb;
    [SerializeField] private  WeaponBase weapon;


    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Bullet") && weapon.GetComponent<WeaponBase>().GetAmmo() == true)
        {
            Destroy(gameObject);
        }
    }
    public void Die()
    {
        if (Health < 0 && weapon.GetComponent<WeaponBase>().GetAmmo() == true)
       Destroy(gameObject);
    }

  
    
}
