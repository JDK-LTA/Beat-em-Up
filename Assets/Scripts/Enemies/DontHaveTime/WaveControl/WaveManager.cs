using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: CLASE CON ID Y NUMERO DE ENEMIGOS POR OLEADA DE ESE TIPO ESPECIFICO Y TIEMPO DE APARICION
// LISTA DE ENEMIGOS ÚNICOS

    // ID TERNA: NUMERO ENEMIGOS, ID ENEMIGOS, TIEMPO APARICIÓN
    // ID POSICION WAVE: LISTA DE POSICIONES (ID+VECTOR3)
    // ID APARICIONES: LISTA DE ID TERNA, LISTA DE POSICIONES WAVE Y UN MODO DE COMPORTAMIENTO

    // GESTIONADOR DE ENTRE OLEADAS

//TODO: REINTEGRAR EL SISTEMA CON EL WAVEMANAGER (QUIZÁS CREAR ENEMIESMANAGER!!)
public class WaveManager : Singleton<WaveManager>
{
    [HideInInspector] public List<EnemyInfo> enemiesThisWave;
    [HideInInspector] public List<Waypoint> pointsToSpawn;

    WaveList waveList;
    EnemyList enemyList;

    [SerializeField] private GameObject enemyContainer;

    private List<GameObject> enemiesInScene = new List<GameObject>();
    private int waveIndex = 0;

    public delegate void EnemiesEvents(int id);
    public static event EnemiesEvents InitializeEnemies;

    void Start()
    {
        waveList = Resources.Load<WaveList>("WaveList");
        enemyList = Resources.Load<EnemyList>("EnemyList");

        UpdateEnemiesThisWave();
        InitWave();
    }

    void Update()
    {
        
    }

    private void InitWave()
    {
        for (int i = 0; i < enemiesThisWave.Count; i++)
        {
            if(enemiesInScene.Count<=i)
            {
                GameObject enemy = Instantiate(new GameObject("Enemy"), enemyContainer.transform);
                AddComponentsAndInit(i, enemy);

                enemiesInScene.Add(enemy);
            }
            else
            {
                Component[] comps = enemiesInScene[i].GetComponents<Component>();
                for (int j = 0; j < comps.Length; j++)
                {
                    if (comps[j] != transform)
                    {
                        Destroy(comps[j]);
                    }
                }

                AddComponentsAndInit(i, enemiesInScene[i]);
            }
        }
    }

    //public List<EnemyBase> GetEnemiesByDistance(Vector3 from)
    //{
    //    List<EnemyBase> enemies = enemiesInScene;

    //    enemies.Sort(delegate (EnemyBase e1, EnemyBase e2) { return Vector3.Distance(from, e1.transform.position).CompareTo(Vector3.Distance(from, e2.transform.position)); });

    //    return enemies;
    //}

    private void AddComponentsAndInit(int i, GameObject enemy)
    {
        switch (enemiesThisWave[i].Type)
        {
            case EnemyInfo.EnemyType.MELEE:
                enemy.AddComponent<EnemyMelee>();           
                break;
            case EnemyInfo.EnemyType.RANGED:
                enemy.AddComponent<EnemyRanged>();
                break;
            case EnemyInfo.EnemyType.TANK:
                enemy.AddComponent<EnemyTank>();
                break;
            case EnemyInfo.EnemyType.HEALER:
                enemy.AddComponent<EnemyHealer>();
                break;
            default:
                enemy.AddComponent<EnemyMelee>();
                break;
        }
        enemy.GetComponent<EnemyBase>().Init(i);
    }

    private void UpdateEnemiesThisWave()
    {
        for (int i = 0; i < enemiesThisWave.Count; i++)
        {
            enemiesThisWave.Remove(enemiesThisWave[i]);
        }

        List<EnemyCreation> enCrList = waveList.wave[waveIndex].enemyCreations;

        for (int i = 0; i < enCrList.Count; i++)
        {
            for (int j = 0; j < enCrList[i].enemyWaveTriadIds.Count; j++)
            {
                CompareTriadsId(enCrList, i, j);
            }
        }
    }

    private void CompareTriadsId(List<EnemyCreation> enCrList, int i, int j)
    {
        for (int k = 0; k < waveList.enemyWave_Triads.Count; k++)
        {
            if (enCrList[i].enemyWaveTriadIds[j] == waveList.enemyWave_Triads[k].triadId)
            {
                for (int l = 0; l < waveList.enemyWave_Triads[k].numberOfEnemies; l++)
                {
                    CompareEnemyIdAndAddToList(k);
                }
            }
        }
    }

    private void CompareEnemyIdAndAddToList(int k)
    {
        for (int m = 0; m < enemyList.enemies.Count; m++)
        {
            if (waveList.enemyWave_Triads[k].enemyId == enemyList.enemies[m].Id)
            {
                enemiesThisWave.Add(enemyList.enemies[m]);
            }
        }
    }
}
