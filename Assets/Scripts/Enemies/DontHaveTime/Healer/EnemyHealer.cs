using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealer : EnemyBase
{
    [HideInInspector] public HealAreaEveryXSec heal;
    [HideInInspector] public AttackSet attackSet;

    private bool healing = true;

    protected override void Start()
    {
        base.Start();
        heal = gameObject.AddComponent<HealAreaEveryXSec>();
        attackSet = gameObject.AddComponent<AttackSet>();
    }
    public override void Init(int id)
    {
        base.Init(id);
        heal.Init(_abilityPwr, _timeBetweenAbilities);
        attackSet.Init(_attackDmg, _timeBetweenAttacks);
        attackSet.enabled = false;
    }
    protected override void Chase()
    {
        if (healing)
        {
            GoTowardsGoal(EnemiesManager.Instance.GetEnemiesByHp()[0].transform.position);
        }
        else
        {
            base.Chase();
        }
    }
    protected override void Update()
    {
        if (EnemiesManager.Instance.GetNOfNonHealers() < EnemiesManager.Instance.GetNOfHealers())
        {
            healing = false;
        }
    }
}
