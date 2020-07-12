using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged2 : EnemyBase2
{
    [SerializeField] GameObject arrow;
    [SerializeField] Transform shootingPos;
    [SerializeField] float chanceToSpecialArrow;
    [SerializeField] int specialArrowId;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Attack()
    {
        base.Attack();
        AudioSource.PlayClipAtPoint(SoundManager.Instance.enemyArrow, transform.position);
    }

    private void ShootArrow()
    {
        GameObject go = Instantiate(arrow, shootingPos.position, shootingPos.rotation);

        bool poison = false;
        if (Random.Range(0, 100) < chanceToSpecialArrow)
        {
            poison = true;
        }

        go.GetComponent<Arrow>().InitDmg(attackDmg, poison, goingForPlayer, specialArrowId);
    }
}
