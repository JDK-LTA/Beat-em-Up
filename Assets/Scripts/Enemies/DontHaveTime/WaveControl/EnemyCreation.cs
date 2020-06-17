using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyCreation
{
    public List<string> enemyWaveTriadIds;
    public List<string> waypointIds;
    public WayToSpawn wayToSpawn;
}
public enum WayToSpawn
{
    RANDOM,
    IN_ORDER,
    AT_ONCE
}