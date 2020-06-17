using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaveList : ScriptableObject
{
    public List<EnemyWave_Triad> enemyWave_Triads;
    public List<Waypoint> waypoints;
    public List<EnemyCreationList> wave;
}

[System.Serializable]
public class EnemyCreationList
{
    public List<EnemyCreation> enemyCreations;
}

//public class CreateWaveList
//{
//    [MenuItem("Assets/Lists/Wave List")]
//    public static WaveList Create()
//    {
//        WaveList asset = ScriptableObject.CreateInstance<WaveList>();
//        AssetDatabase.CreateAsset(asset, "Assets/Resources/WaveList.asset");
//        AssetDatabase.SaveAssets();
//        return asset;
//    }
//}
