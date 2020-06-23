using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    float dmg = 0;

    public void InitDmg(float d)
    {
        dmg = d;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyBase2 enemy = collision.GetComponent<EnemyBase2>();

        if (enemy)
        {
            enemy.ChangeHp(-dmg * Time.deltaTime);
        }
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
