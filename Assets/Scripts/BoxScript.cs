using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BoxScript : MonoBehaviour, IDamagable
{
    [SerializeField] private Player _player;
    [field: SerializeField] public float Health { get; set; }
    [SerializeField] private float speed;
     [SerializeField]private WeaponBase weapon ;
     public bool AmmoboxDestroyed;
     

   
    private void Awake()
    {
    }
    private void Update()
    {

    }
  
    public void Die()
    {
        print(weapon.GetAmmo());
        if (Health < 0)
        {
            AmmoboxDestroyed = true;
            print("active");
        }
   
        if (Health < 0 &&  AmmoboxDestroyed == true && weapon.GetAmmo() ==true)
        {
            _player.currentWeapon.maxAmmo = _player.currentWeapon.AmmoRefillMax;
            _player.currentWeapon.ammoLeft = _player.currentWeapon.magSize;
            _player.currentWeapon.Ammo.text = "Ammo: " + _player.currentWeapon.ammoLeft.ToString() + " / " + _player.currentWeapon.maxAmmo.ToString();
                    Destroy(gameObject);

                }

            }
        }



    
