using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    private static Projectile projectile = new();
    [Header("Weapon Base Stats ")]
    [SerializeField] protected float timeBetweenAttacks;
    [SerializeField] protected float chargeUpTime;
    [SerializeField, Range(0,1)] protected float minChargePercent;
    [SerializeField] private bool isFullAuto;
    private Coroutine _currentFireTimer;
    private bool _isOnCooldown;
    private WaitForSeconds _coolDownWait;
    private WaitUntil _coolDownEnforce;
    private float _currentChargeTime;
    [Header("Ammo Stats ")]
    [SerializeField] public int maxAmmo;
    [SerializeField] public int magSize;
    [SerializeField] public int AmmoRefillMax;
    private Boolean HasAmmo;
    public int ammoLeft;
    [Header("Player UI")]
    [SerializeField] public TextMeshProUGUI Ammo;
    [SerializeField] public TextMeshProUGUI ReloadAlert;


    private void Start ()
    {
        ammoLeft = magSize;
        Ammo.text = "Ammo: " + ammoLeft.ToString() + " / " + maxAmmo.ToString();
        HasAmmo = true;
        _coolDownWait = new WaitForSeconds(timeBetweenAttacks);
        _coolDownEnforce = new WaitUntil(() => !_isOnCooldown);
    }

    public  void Reload()
    {
        if (HasAmmo == true && maxAmmo >= 0)
        {
            if (ammoLeft == 0)
            {
                maxAmmo -= magSize + ammoLeft;
                ammoLeft = magSize;
                HasAmmo = true;
                Ammo.text = "Ammo: " + ammoLeft.ToString() + " / " + maxAmmo.ToString();
                ReloadAlert.text = "";
            }
            if (maxAmmo >= (magSize - ammoLeft))
            {
                maxAmmo = (maxAmmo - magSize) + ammoLeft;
                ammoLeft = magSize;
                HasAmmo = true;
                Ammo.text = "Ammo: " + ammoLeft.ToString() + " / " + maxAmmo.ToString();
                ReloadAlert.text = "";
            }
            if (maxAmmo <= (magSize - ammoLeft))
            {
                HasAmmo = false;
                ammoLeft += maxAmmo;
                maxAmmo = 0;
                Ammo.text = "Ammo: " + ammoLeft.ToString() + " / " + maxAmmo.ToString();
                ReloadAlert.text = "";
            }
        }
    }
   


    // Start is called before the first frame update
    public void startShooting()
    {
        _currentFireTimer = StartCoroutine(ReFireTimer());
    }
    public void stopShooting() 
    { 
        StopCoroutine(_currentFireTimer);
        float percent = _currentChargeTime / chargeUpTime;
        if (percent != 0) TryAttack(percent);
    }


    private IEnumerator CooldownTimer()
    {
        _isOnCooldown = true;
        yield return _coolDownWait;
        _isOnCooldown = false;
    }
    private IEnumerator ReFireTimer()
    {
        print("Waiting for cooldown");
        yield return _coolDownEnforce;
        print("Post coolDown");

        while (_currentChargeTime < chargeUpTime)
        {
            _currentChargeTime += Time.deltaTime;
            yield return null;
        }
        TryAttack(1);
        yield return null;
    }

    public Boolean GetAmmo()
    {
        if (maxAmmo < AmmoRefillMax)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void TryAttack(float percent)
    {
        _currentChargeTime = 0;
        if(!CanAttack(percent)) return;
        if (ammoLeft > 0)
        {
            Attack(percent);
            ammoLeft--;
            Ammo.text = "Ammo: " + ammoLeft.ToString() + " / " + maxAmmo.ToString();

        }
        if (maxAmmo == 0 && ammoLeft != 0)
        {
            HasAmmo = false;
        }
        if (ammoLeft == 0)
        {
            ReloadAlert.text = "OUT OF AMMMO, PRESS R TO RELOAD";

        }
        else
        {
            ReloadAlert.text = "";
        }

        if (maxAmmo == 0 && ammoLeft == 0)
        {
            ReloadAlert.text = "OUT OF AMMMO ";
        }

        if (GetAmmo() == true)
        {
            maxAmmo = AmmoRefillMax;
            Ammo.text = "Ammo: " + ammoLeft.ToString() + " / " + maxAmmo.ToString();
            Destroy(GameObject.FindWithTag("AmmoBox"));
        }

        StartCoroutine(CooldownTimer());   
        if (isFullAuto && percent >= 1) _currentFireTimer = StartCoroutine(ReFireTimer());

       

    }




    protected virtual bool CanAttack(float percent)
    {
        return !_isOnCooldown && percent >= minChargePercent;
    }
    protected abstract void Attack(float percent);

}
