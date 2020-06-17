using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    [HideInInspector] public bool canAttack = true;
    protected float cdToAttack;
    protected float damage;

    public virtual void Init(float dmg, float cd)
    {
        cdToAttack = cd;
        damage = dmg;
    }
    protected virtual void Attack()
    {

    }

    float t = 0;
    private void Update()
    {
        if (canAttack)
        {
            t += Time.deltaTime;
            if (t >= cdToAttack)
            {
                t = 0;
                Attack();
            }
        }
    }

}
