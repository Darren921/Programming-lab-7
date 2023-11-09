using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Player : MonoBehaviour
{
    static int counter;
    [SerializeField] ProjectileWeapon _weapon1;
    [SerializeField] Shotgun _weapon2;
    [SerializeField] public BurstWeapon _weapon3;
    
    public WeaponBase currentWeapon ;
    private bool weaponShootToggle;
    
    private void Start()
    {
        counter = 0;
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
        print(counter);

        if (counter == 0)
        {
            print(counter);
            currentWeapon = _weapon1;
            counter++;
            currentWeapon.Ammo.text = "Ammo: " + currentWeapon.ammoLeft.ToString() + " / " + currentWeapon.maxAmmo.ToString();
            return;
        }
        if (counter == 1)
        {
            print(counter);
            currentWeapon = _weapon2;
            counter++;
            currentWeapon.Ammo.text = "Ammo: " + currentWeapon.ammoLeft.ToString() + " / " + currentWeapon.maxAmmo.ToString();

            return;

        }
        if (counter == 2)
        {
            print(counter);
            currentWeapon = _weapon3;
            counter = 0;
            currentWeapon.Ammo.text = "Ammo: " + currentWeapon.ammoLeft.ToString() + " / " + currentWeapon.maxAmmo.ToString();

            return;

        }
    }

    }

