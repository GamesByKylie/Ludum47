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
    protected bool timing = true;

    private void OnValidate()
    {
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        currentWeapon = unarmed;
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        //so they can attack right away if they want to
        timer = currentWeapon.cooldown;
    }

    public virtual void DoDamage(Health h)
    {
        h.TakeDamage(currentWeapon.damage);
    }

    public virtual void PerformAttack()
    {
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= currentWeapon.range)
        {
            target.TakeDamage(currentWeapon.damage);
        }
    }

    public virtual void SwitchWeapon(Weapon w)
    {
        w.gameObject.SetActive(true);

        //Unarmed is on the base game object, so we don't want to disable that
        if (!currentWeapon.Equals(unarmed))
        {
            currentWeapon.gameObject.SetActive(false);
        }
        currentWeapon = w;
    }

    public void PauseTiming()
    {
        timing = false;
    }

    public void StartTiming()
    {
        timing = true;
    }
}
