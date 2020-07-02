using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager2 : Singleton<WaveManager2>
{
    [SerializeField] int _currentWave = -1;
    [SerializeField] List<WaveInfo> _waves;

    private List<EnemyBase2> _enemiesThisWave;
    private List<Vector3> _positionsToSpawn;
    private int _waveDifficulty;
    [SerializeField] private int _currentDifficulty;
    private int _nOfEnemyDiffThisWave;
    private float _cdPerSpawn;

    public List<WaveInfo> Waves { get => _waves; }
    public int CurrentWave { get => _currentWave; }

    bool isSpawning = true, debugStopSpawn = false, roundsStarted = false, endWaveTimerOn = false;
    float tPerSpawn = 0, tPerEndWave = 0, tPerStartWaves = 0;
    [SerializeField] float endWaveCooldown = 5f, startWavesCooldown = 3f;
    private EnemyBase2 lastEnemySpawned = null;

    [SerializeField] private bool debug = false;

    private void Start()
    {
        _waves = Resources.Load<WaveList2>("WaveList2").waves;
    }
    public void Init()
    {
        roundsStarted = true;
        BeginNextWave();
    }

    private void UpdateWave()
    {
        _enemiesThisWave = _waves[_currentWave].EnemiesThisWave;
        _positionsToSpawn = _waves[_currentWave].PositionsToSpawn;

        _cdPerSpawn = _waves[_currentWave].CdBetweenEnemiesSpawn;
        _nOfEnemyDiffThisWave = _waves[_currentWave].NOfEnemyDiffThisWave;

        _waveDifficulty = _waves[_currentWave].TotalDifficulty;
        _currentDifficulty = _waves[_currentWave].TotalDifficulty;

        EnemiesManager.Instance.MaxDiffChasing = _waves[_currentWave].MaxDiffChasing;
    }
    private void AboutToEndWave()
    {
        isSpawning = false;
    }
    private void EndWave()
    {
        if (_currentWave < _waves.Count - 1)
        {
            endWaveTimerOn = true;
            tPerSpawn = 0;
        }
        else
        {
            GameManager.Instance.EndGame(true);
        }
    }
    public void BeginNextWave()
    {
        endWaveTimerOn = false;
        _currentWave++;

        GameManager.Instance.UpdateWaveText();
        UpdateWave();
        isSpawning = true;
    }

    private void Update()
    {
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                debugStopSpawn = !debugStopSpawn;
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                Init();
            }
        }

        if (roundsStarted)
        {
            if (isSpawning)
            {
                if (!debugStopSpawn)
                {
                    if (_currentDifficulty > 0)
                    {
                        tPerSpawn += Time.deltaTime;
                        if (tPerSpawn >= _cdPerSpawn)
                        {
                            tPerSpawn = 0;
                            SpawnEnemy(_enemiesThisWave);
                        }
                    }
                }
                if (_nOfEnemyDiffThisWave == 0)
                {
                    AboutToEndWave();
                }
            }
            else
            {
                if (EnemiesManager.Instance.CreatedEnemies.Count == 0)
                {
                    EndWave();
                }
            }
        }
        else
        {
            if (!debug)
            {
                tPerStartWaves += Time.deltaTime;
                GameManager.Instance.WaveText.text = "Time to start first wave: " + (int)(1 + startWavesCooldown - tPerStartWaves);

                if (tPerStartWaves >= startWavesCooldown)
                {
                    Init();
                }
            }
        }

        if (endWaveTimerOn)
        {
            tPerEndWave += Time.deltaTime;
            GameManager.Instance.WaveText.text = "Time to start next wave: " + (int)(1 + endWaveCooldown - tPerEndWave);

            if (tPerEndWave >= endWaveCooldown)
            {
                tPerEndWave = 0;
                BeginNextWave();
            }

        }
    }

    private void SpawnEnemy(List<EnemyBase2> enemyList)
    {
        List<EnemyBase2> possibleEnemies = new List<EnemyBase2>();

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].Difficulty <= _currentDifficulty)
            {
                possibleEnemies.Add(enemyList[i]);
            }
        }

        if (possibleEnemies.Count > 0)
        {
            int ran = Random.Range(0, possibleEnemies.Count);

            GameObject go = Instantiate(possibleEnemies[ran].gameObject, GetSpawnPosition(), Quaternion.identity);
            EnemiesManager.Instance.CreatedEnemies.Add(go.GetComponent<EnemyBase2>());

            lastEnemySpawned = possibleEnemies[ran];
            _currentDifficulty -= possibleEnemies[ran].Difficulty;
            _nOfEnemyDiffThisWave -= possibleEnemies[ran].Difficulty;
        }
    }
    private Vector3 GetSpawnPosition()
    {
        int ran = Random.Range(0, _positionsToSpawn.Count);

        return _positionsToSpawn[ran];
    }
    public void AddDifficulty(int add)
    {
        _currentDifficulty += add;
    }

}
