using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Weapon primaryAttack;

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
        currentWeapon = primaryAttack;
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
        if (InRange(currentWeapon))
        {
            target.TakeDamage(currentWeapon.damage);
        }
    }

    public virtual void SwitchWeapon(Weapon w)
    {
        w.gameObject.SetActive(true);

        //If the object isn't on this parent, turn it off
        if (!currentWeapon.gameObject.Equals(gameObject))
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

    public bool InRange(Weapon w)
    {
        return target != null && Vector3.Distance(transform.position, target.transform.position) <= w.range;
    }

    public bool InRange(float range)
    {
        return target != null && Vector3.Distance(transform.position, target.transform.position) <= range;
    }
}
