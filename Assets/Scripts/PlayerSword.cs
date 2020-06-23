using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    private float dmg;

    public void InitDmg(float a)
    {
        dmg = a;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase2 enemy = collision.GetComponent<EnemyBase2>();

        if (enemy != null)
        {
            enemy.ChangeHp(-dmg);
        }
    }
}
