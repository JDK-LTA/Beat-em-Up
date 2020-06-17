using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged2 : EnemyBase2
{
    [SerializeField] GameObject arrow;
    [SerializeField] Transform shootingPos;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject go = Instantiate(arrow, shootingPos.position, shootingPos.rotation);
        go.GetComponent<Arrow>().InitDmg(attackDmg);
    }
}
