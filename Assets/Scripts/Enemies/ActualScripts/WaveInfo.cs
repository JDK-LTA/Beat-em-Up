using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveInfo
{
    [SerializeField] private int totalDifficulty;
    [SerializeField] private List<EnemyBase2> enemiesThisWave;
    [SerializeField] private float cdBetweenEnemiesSpawn = 0.75f;
    [SerializeField] private int nOfEnemyDiffThisWave;
    [SerializeField] private List<Vector3> positionsToSpawn;

    public int TotalDifficulty { get => totalDifficulty; set => totalDifficulty = value; }
    public List<EnemyBase2> EnemiesThisWave { get => enemiesThisWave; set => enemiesThisWave = value; }
    public List<Vector3> PositionsToSpawn { get => positionsToSpawn; set => positionsToSpawn = value; }
    public int NOfEnemyDiffThisWave { get => nOfEnemyDiffThisWave; set => nOfEnemyDiffThisWave = value; }
    public float CdBetweenEnemiesSpawn { get => cdBetweenEnemiesSpawn; set => cdBetweenEnemiesSpawn = value; }
}
