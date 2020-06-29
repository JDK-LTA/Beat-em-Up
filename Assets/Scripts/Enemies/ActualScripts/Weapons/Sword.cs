using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private float dmg;
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    public void InitDmg(float a)
    {
        dmg = a;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.ChangeHp(-dmg);
        }

        Nexus nexus = collision.GetComponent<Nexus>();
        if (nexus)
        {
            nexus.DamageNexus(dmg);
        }
    }
}
