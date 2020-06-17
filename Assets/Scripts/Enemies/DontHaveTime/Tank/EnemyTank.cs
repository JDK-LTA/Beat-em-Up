using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(GainStatSet))]
//[RequireComponent(typeof(AttackSet))]
public class EnemyTank : EnemyBase
{
    [HideInInspector] public GainStatSet gainStatSet;
    [HideInInspector] public AttackSet attackSet;
    protected override void Start()
    {
        base.Start();
        gainStatSet = gameObject.AddComponent<GainStatSet>();
        attackSet = gameObject.AddComponent<AttackSet>();
    }

    public override void Init(int id)
    {
        base.Init(id);
        gainStatSet.Init();
        attackSet.Init(_attackDmg, _timeBetweenAttacks);
    }
}
