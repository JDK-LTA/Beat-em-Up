using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AttackSet))]
public class EnemyMelee : EnemyBase
{
    [HideInInspector] public AttackSet attackSet;
    protected override void Start()
    {
        base.Start();
        //attackSet = GetComponent<AttackSet>();
    }

    public override void Init(int id)
    {
        attackSet = gameObject.AddComponent<AttackSet>();

        base.Init(id);
        attackSet.Init(_attackDmg, _timeBetweenAttacks);
    }
}
