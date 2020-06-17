using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee2 : EnemyBase2
{
    private GameObject sword;

    protected override void Awake()
    {
        base.Awake();
        sword = GetComponentInChildren<Sword>(true).gameObject;
        sword.GetComponent<Sword>().InitDmg(attackDmg);
    }

    protected override void Attack()
    {
        base.Attack();

        sword.SetActive(true);
    }
}
