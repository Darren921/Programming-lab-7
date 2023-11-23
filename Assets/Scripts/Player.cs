using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Projectile;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Player : MonoBehaviour
{
    static int counterW;
    [SerializeField] ProjectileWeapon _weapon1;
    [SerializeField] Shotgun _weapon2;
    [SerializeField] public BurstWeapon _weapon3;
    private Projectile Projectile = new();
    public WeaponBase currentWeapon ;
    private bool weaponShootToggle;
    private void Start()
    {
        counterW = 0;
        currentWeapon = _weapon1;
        InputManager.Init(this);
        InputManager.EnableInGame();
    }

    public void Shoot()
    {
        print("I shot: " + InputManager.GetCameraRay());
        weaponShootToggle = !weaponShootToggle;
        if (weaponShootToggle) currentWeapon.startShooting();
        else currentWeapon.stopShooting();

    }
    public void reload()
    {
        currentWeapon.Reload();
    }

    public void weaponSwap()
    {
        counterW++;
        print(counterW);

        if (counterW == 0)
        {
            print(counterW);
            currentWeapon = _weapon1;
           
            currentWeapon.Ammo.text = "Ammo: " + currentWeapon.ammoLeft.ToString() + " / " + currentWeapon.maxAmmo.ToString();
            return;
        }
        if (counterW == 1)
        {
            print(counterW);
            currentWeapon = _weapon2;
           
            currentWeapon.Ammo.text = "Ammo: " + currentWeapon.ammoLeft.ToString() + " / " + currentWeapon.maxAmmo.ToString();

            return;

        }
        if (counterW == 2)
        {
            print(counterW);
            currentWeapon = _weapon3;
            counterW -=  3;
            currentWeapon.Ammo.text = "Ammo: " + currentWeapon.ammoLeft.ToString() + " / " + currentWeapon.maxAmmo.ToString();

            return;

        }
    }
   

    }

