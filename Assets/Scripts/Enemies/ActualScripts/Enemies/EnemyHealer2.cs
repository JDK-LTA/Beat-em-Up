using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealer2 : EnemyRanged2
{
    bool healing = true;
    [SerializeField] float amountToHeal;

    protected override void Start()
    {
        base.Start();
        countAsChasing = false;
        //WHEN ATTACKING IT COUNTS AS CHASING
    }

    protected override void Attack()
    {
        if (healing)
        {
            Heal(EnemiesManager.Instance.GetEnemiesByDistance(transform.position)[0]);
        }
        else
        {
            base.Attack();
        }
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
    protected override void GoForNexus()
    {
        if (healing)
        {
            Chase();
        }
        else
        {
            base.GoForNexus();
        }
    }

    private void Heal(EnemyBase2 enemy)
    {
        enemy.ChangeHp(amountToHeal);
    }
}
