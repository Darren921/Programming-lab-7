using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Player : MonoBehaviour
{
    [SerializeField] private WeaponBase myWeapon;
    private bool weaponShootToggle;
    private void Start()
    {
        InputManager.Init(this);
        InputManager.EnableInGame();
    }

    public void Shoot()
    {
        print("I shot: " + InputManager.GetCameraRay());
        weaponShootToggle = !weaponShootToggle;
        if (weaponShootToggle) myWeapon.startShooting();
        else myWeapon.stopShooting();

    }
    public void reload()
    {
        myWeapon.Reload();
    }
}
