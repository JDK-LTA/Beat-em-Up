using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealer2 : EnemyRanged2
{
    bool healing = true;
    [SerializeField] float amountToHeal;
    [SerializeField] GameObject healingParticles;

    public bool Healing { get => healing; set => healing = value; }

    protected override void Start()
    {
        base.Start();
        countAsChasing = false;
        //WHEN ATTACKING IT COUNTS AS CHASING
    }
    protected override void Update()
    {
        if (healing && EnemiesManager.Instance.GetNOfHealers() >= EnemiesManager.Instance.GetNOfNonHealers())
        {
            healing = false;
            countAsChasing = true;
        }

        base.Update();
    }
    protected override void Attack()
    {
        if (healing)
        {
            animComp.SetTrigger("Heal");
            Heal(EnemiesManager.Instance.GetEnemiesByHp(this)[0]);
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
            GoTowardsGoal(EnemiesManager.Instance.GetEnemiesByHp(this)[0].transform.position);
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
        Instantiate(healingParticles, enemy.transform.position, enemy.transform.rotation);
        enemy.ChangeHp(amountToHeal);
    }
}
