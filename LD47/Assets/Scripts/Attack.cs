using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Weapon unarmed;
    public Weapon weapon;

    public Health target;

    protected Weapon currentWeapon;
    protected Animator anim;
    protected float timer;

    private void OnValidate()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        currentWeapon = unarmed;
    }

    public virtual void DoDamage(Health h)
    {
        h.TakeDamage(currentWeapon.damage);
    }

    public virtual void PerformAttack()
    {
        Debug.Log("Performing Attack");
        if (Vector3.Distance(transform.position, target.transform.position) <= currentWeapon.range)
        {
            target.TakeDamage(currentWeapon.damage);
        }
    }

    public virtual void SwitchWeapon(Weapon w)
    {
        currentWeapon = w;
    }
}
