using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Stats",menuName = "MyScriptableObjects/WeaponStats", order = 1)]
public class WeaponSO : ScriptableObject
{
    [field: Header("Weapon Base Stats ")]
    
    [SerializeField] private float TimeBetweenAttacks ; 
    [field: SerializeField] public float ChargeUpTime { get; private set; }
    [field: SerializeField, Range(0, 1)] public float MinChargePercent { get; private set; }
    [field: SerializeField] public bool IsFullAuto { get; private set; }
    public WaitForSeconds _coolDownWait {  get; private set; }

    private void OnEnable()
    {
        _coolDownWait = new WaitForSeconds(TimeBetweenAttacks);
    }
}
