using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : Singleton<EnemiesManager>
{
    [SerializeField] private List<EnemyBase2> createdEnemies;
    [SerializeField] private int currDiffChasing = 0;
    [SerializeField] private int maxDiffChasing = 6;
    public List<EnemyBase2> CreatedEnemies { get => createdEnemies; set => createdEnemies = value; }
    public int CurrDiffChasing { get => currDiffChasing; set => currDiffChasing = value; }
    public int MaxDiffChasing { get => maxDiffChasing; set => maxDiffChasing = value; }

    public List<EnemyBase2> GetEnemiesByDistance(Vector3 from)
    {
        List<EnemyBase2> enemies = new List<EnemyBase2>(createdEnemies);

        enemies.Sort(delegate (EnemyBase2 e1, EnemyBase2 e2) { return Vector3.Distance(from, e1.transform.position).CompareTo(Vector3.Distance(from, e2.transform.position)); });

        return enemies;
    }
    public List<EnemyBase2> GetEnemiesByDistance(Vector3 from, EnemyBase2 excluding)
    {
        List<EnemyBase2> enemies = new List<EnemyBase2>(createdEnemies);
        enemies.Remove(excluding);

        enemies.Sort(delegate (EnemyBase2 e1, EnemyBase2 e2) { return Vector3.Distance(from, e1.transform.position).CompareTo(Vector3.Distance(from, e2.transform.position)); });

        return enemies;
    }
    public float GetNOfNonHealers()
    {
        float nh = 0;

        for (int i = 0; i < createdEnemies.Count; i++)
        {
            EnemyHealer2 eh = createdEnemies[i].GetComponent<EnemyHealer2>();
            if (!eh)
            {
                nh++;
            }
            else if (!eh.Healing)
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
            EnemyHealer2 eh = createdEnemies[i].GetComponent<EnemyHealer2>();
            if (eh)
            {
                if (eh.Healing)
                {
                    nh++;
                }
            }
        }

        return nh;
    }
    public List<EnemyBase2> GetEnemiesByHp()
    {
        List<EnemyBase2> enemies = new List<EnemyBase2>(createdEnemies);

        enemies.Sort(delegate (EnemyBase2 e1, EnemyBase2 e2) { return e1.CurrentHp.CompareTo(e2.CurrentHp); });

        return enemies;
    }
    public List<EnemyBase2> GetEnemiesByHp(EnemyBase2 excluding)
    {
        List<EnemyBase2> enemies = new List<EnemyBase2>(createdEnemies);
        enemies.Remove(excluding);

        enemies.Sort(delegate (EnemyBase2 e1, EnemyBase2 e2) { return e1.CurrentHp.CompareTo(e2.CurrentHp); });

        return enemies;
    }
}
