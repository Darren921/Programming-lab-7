using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    private WeaponBase weapon;
    [SerializeField] private float damage;
    [SerializeField] private float shootForce;
    [SerializeField] private Rigidbody rb;
    private float trueDamage;
    public bool hittarget;
    private EnemyController enemyController;
    static bool IsBleedingTypeA;
    public enum EWoundType
    {
        Standard,
        Graze,
        Fatal,
    }

    private static EWoundType type;
    private void Awake()
    {
        enemyController = GameObject.Find("Swat").GetComponent<EnemyController>();
        type = EWoundType.Standard;
        weapon = GetComponent<WeaponBase>();
    }

    public void Init(float chargePercent, Vector3 fireDirection)
    {
        rb.AddForce(shootForce * chargePercent * fireDirection, ForceMode.Impulse);
        trueDamage = chargePercent * damage;
    }
    private void OnCollisionEnter(Collision other)
    {
        var isGraze = Random.Range(0, 10);
        var isFatal = Random.Range(0, 20);
         print(other.transform.name + ", " + other.transform.root.name);
        if (isGraze <= 4)
        {
            type = EWoundType.Graze;

        }
        if (isFatal <= 10)
        {
            type = EWoundType.Fatal;

        }
        if (isGraze >= 5 && isFatal >= 11)
        {
            type = EWoundType.Standard;
        }

        if (other.transform.root.TryGetComponent(out IDamagable hitTarget))
        {
            switch (other.transform.tag)
            {
                case "Head":
                    print(type);
                    if (type == EWoundType.Standard)
                    {
                        trueDamage *= 2;
                    }
                    if (type == EWoundType.Graze)
                    {
                        trueDamage *= 1f;
                    }
                    if (type == EWoundType.Fatal)
                    {
                        trueDamage = (enemyController.Health * 1) + 1;
                    }

                    break;
                case "Limb":
                    print(type);
                    if (type == EWoundType.Standard)
                    {
                        trueDamage *= 0.60f;
                    }
                    if (type == EWoundType.Graze)
                    {
                        trueDamage *= 0.4f;
                    }
                    if (type == EWoundType.Fatal)
                    {
                        trueDamage = (enemyController.Health * 1) + 1;

                    }
                    break;
                case "Body":
                    print(type);
                    if (type == EWoundType.Standard)
                    {
                        trueDamage *= 0.40f;
                    }
                    if (type == EWoundType.Graze)
                    {
                        trueDamage *= 0.3f;
                    }
                    if (type == EWoundType.Fatal)
                    {
                        trueDamage = (enemyController.Health * 1) + 1;

                    }
                    break;
            }
            print(trueDamage);
            hitTarget.TakeDamage(trueDamage);
            Destroy(gameObject);


        }
    }
}
  
  

