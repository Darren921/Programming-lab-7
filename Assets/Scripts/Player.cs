using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Player : MonoBehaviour
{
    [SerializeField] ProjectileWeapon _weapon1;
    [SerializeField] Shotgun _weapon2;
    [SerializeField] public BurstWeapon _weapon3;
    public WeaponBase currentWeapon ;
    private bool weaponShootToggle;
    
    private void Start()
    {

        currentWeapon = _weapon3;
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
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentWeapon.Ammo.text = "Ammo: " + currentWeapon.ammoLeft.ToString() + " / " + currentWeapon.maxAmmo.ToString();
            currentWeapon = _weapon2;
            currentWeapon.Ammo.text = "Ammo: " + currentWeapon.ammoLeft.ToString() + " / " + currentWeapon.maxAmmo.ToString();
        }
           

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentWeapon.Ammo.text = "Ammo: " + currentWeapon.ammoLeft.ToString() + " / " + currentWeapon.maxAmmo.ToString();
            currentWeapon = _weapon1;
            currentWeapon.Ammo.text = "Ammo: " + currentWeapon.ammoLeft.ToString() + " / " + currentWeapon.maxAmmo.ToString();

        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentWeapon.Ammo.text = "Ammo: " + currentWeapon.ammoLeft.ToString() + " / " + currentWeapon.maxAmmo.ToString();
            currentWeapon = _weapon3;
            currentWeapon.Ammo.text = "Ammo: " + currentWeapon.ammoLeft.ToString() + " / " + currentWeapon.maxAmmo.ToString();

        }


    }
    }

