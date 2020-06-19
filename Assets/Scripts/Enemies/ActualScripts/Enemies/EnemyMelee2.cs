using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee2 : EnemyBase2
{
    private Sword sword;

    protected override void Awake()
    {
        base.Awake();
        sword = GetComponentInChildren<Sword>(true);
        sword.InitDmg(attackDmg);
    }

    protected override void Attack()
    {
        base.Attack();

        sword.gameObject.SetActive(true);
    }
}
