using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(ShootSet))]
public class EnemyRanged : EnemyBase
{
    [HideInInspector] public ShootSet shootSet;
    protected override void Start()
    {
        base.Start();
        shootSet = gameObject.AddComponent<ShootSet>();
        //shootSet = GetComponent<ShootSet>();
    }

    public override void Init(int id)
    {
        base.Init(id);
        shootSet.Init(_attackDmg, _timeBetweenAttacks);
    }
}
