using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyList : ScriptableObject
{
    public List<EnemyInfo> enemies;
}

#if UNITY_EDITOR
public class CreateEnemyList
{
    [MenuItem("Assets/Lists/Enemy List")]
    public static EnemyList Create()
    {
        EnemyList asset = ScriptableObject.CreateInstance<EnemyList>();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/EnemyList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
#endif