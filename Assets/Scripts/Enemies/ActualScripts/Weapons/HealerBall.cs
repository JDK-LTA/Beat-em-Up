using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerBall : Arrow
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.ChangeHp(-dmg);
        }

        if (!collision.GetComponent<EnemyBase2>() && !collision.GetComponent<ItemIdInScene>())
        {
            speed = 0;
            GetComponent<Animator>().SetTrigger("Hit");
        }
    }
    private void DestroyItself()
    {
        Destroy(gameObject);
    }
}
