using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : Singleton<EnemiesManager>
{
    private List<EnemyBase2> createdEnemies;
    public List<EnemyBase2> CreatedEnemies { get => createdEnemies; set => createdEnemies = value; }

    public List<EnemyBase2> GetEnemiesByDistance(Vector3 from)
    {
        List<EnemyBase2> enemies = createdEnemies;
        enemies.Sort(delegate (EnemyBase2 e1, EnemyBase2 e2) { return Vector3.Distance(from, e1.transform.position).CompareTo(Vector3.Distance(from, e2.transform.position)); });
        return enemies;
    }
    public float GetNOfNonHealers()
    {
        float nh = 0;

        for (int i = 0; i < createdEnemies.Count; i++)
        {
            if (!createdEnemies[i].GetComponent<EnemyHealer2>())
            {
                nh++;
            }
        }

        return nh;
    }
    public float GetNOfHealers()
    {
        float nh = 0;

        for (int i = 0; i < createdEnemies.Count; i++)
        {
            if (createdEnemies[i].GetComponent<EnemyHealer2>())
            {
                nh++;
            }
        }

        return nh;
    }
    public List<EnemyBase2> GetEnemiesByHp()
    {
        List<EnemyBase2> enemies = createdEnemies;

        enemies.Sort(delegate (EnemyBase2 e1, EnemyBase2 e2) { return e1.CurrentHp.CompareTo(e2.CurrentHp); });

        return enemies;
    }
}
