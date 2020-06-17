using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAreaEveryXSec : MonoBehaviour
{
    private float secsToHeal, aux;

    private float amountToHeal;
    public void Init(float toHeal, float cd)
    {
        amountToHeal = toHeal;
        secsToHeal = cd;

        aux = secsToHeal;
    }
    private void Update()
    {
        aux -= Time.deltaTime;
        if (aux < 0)
        {
            //FIND OTHER ENEMIES TO HEAL THE LOWER HEALTH % ONE

            //Heal(EnemiesManager.Instance.GetEnemiesByDistance(transform.position)[0], amountToHeal);

            aux = secsToHeal;
        }
    }

    private void Heal(EnemyBase enemy, float amount)
    {
        enemy.ChangeHp(amount);
    }
}
