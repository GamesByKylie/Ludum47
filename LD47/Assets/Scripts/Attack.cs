using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float unarmedDamage;
    public float unarmedDistance;
    //weapon goes here

    public Health target;

    protected bool usingWeapon = false;
    protected Animator anim;

    private void OnValidate()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public virtual void DoDamage(Health h)
    {
        h.TakeDamage(unarmedDamage);
    }

    public virtual void PerformAttack()
    {
        Debug.Log($"{gameObject.name} is attacking something!");
        //anim.SetTrigger("Attack");
        if (Vector3.Distance(transform.position, target.transform.position) <= unarmedDistance)
        {
            target.TakeDamage(unarmedDamage);
        }
    }

    public virtual void SwitchToWeapon()
    {
        usingWeapon = true;
    }
}
