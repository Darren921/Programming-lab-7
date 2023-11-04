using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
   public float Health {  get; set; }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Die();
        }
    }

    public void Die();
}
