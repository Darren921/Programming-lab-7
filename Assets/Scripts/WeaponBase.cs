using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public abstract class WeaponBase : MonoBehaviour
{
  
        
    [Header("Other Classes")]
    private static Camera _camera;
    [SerializeField] private  GameObject _Weapon;
    [SerializeField] private Player _player; 
   [SerializeField] private  BoxScript _box ;
    private  Projectile projectile;
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
    [SerializeField] public int magSize;
    [SerializeField] public int maxAmmo;
    [SerializeField] public int AmmoRefillMax;
    private Boolean HasAmmo;
    public int ammoLeft;
    [Header("Player UI")]
    [SerializeField] public TextMeshProUGUI Ammo;
    [SerializeField] public TextMeshProUGUI ReloadAlert;
    public AudioSource Source;
    public AudioClip clip;
    private void Start ()
    {
        _camera = Camera.main;
        ammoLeft = magSize;
        _player.currentWeapon.Ammo.text = "Ammo: " + _player.currentWeapon.ammoLeft.ToString() + " / " + _player.currentWeapon.maxAmmo.ToString();
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

    public bool GetAmmo()
    {
        if (_player.currentWeapon.maxAmmo < _player.currentWeapon.AmmoRefillMax)
        {
            print("acitve");
            return true;
        }
        else
        {
            print("acitvef");
            return false;
        }
    }
    private void TryAttack(float percent)
    {
        _currentChargeTime = 0;
        if(!CanAttack(percent)) return;
        if (ammoLeft > 0)
        {
            if (_player.currentWeapon != _player._weapon3) {
                Attack(percent);
                ParticleSystem smoke = _camera.GetComponent<ParticleSystem>();
                var shape = smoke.shape;
                smoke.Play();
                ammoLeft--;
                Source.PlayOneShot(clip);
                Ammo.text = "Ammo: " + ammoLeft.ToString() + " / " + maxAmmo.ToString();
            }
            else
            {
                ParticleSystem smoke = _camera.GetComponent<ParticleSystem>();
                for (int i = 0; i <= 3; i++) 
                {
                    var shape = smoke.shape;
                    shape.rotation = InputManager.GetCameraRay().direction  ;
                    smoke.Play();
                    Source.PlayOneShot(clip);
                    Source.PlayOneShot(clip);
                    Source.PlayOneShot(clip);

                }
                Attack(percent);
                ammoLeft--;
                ammoLeft--;
                ammoLeft--;
                Ammo.text = "Ammo: " + ammoLeft.ToString() + " / " + maxAmmo.ToString();
            }
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
  
        StartCoroutine(CooldownTimer());   
        if (isFullAuto && percent >= 1) _currentFireTimer = StartCoroutine(ReFireTimer());

        }

    


    protected virtual bool CanAttack(float percent)
    {
        return !_isOnCooldown && percent >= minChargePercent;
    }
    protected abstract void Attack(float percent);

}
