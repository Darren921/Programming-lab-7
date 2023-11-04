using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamagable
{
    [field: SerializeField] public float Health { get ; set; }

    private bool isDead;

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
        }
        GetComponent<Animator>().enabled = false;
       
    }

    
}
 