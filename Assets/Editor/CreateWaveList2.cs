using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CreateWaveList2
{
    [MenuItem("Assets/Lists/Wave List 2")]
    public static WaveList2 Create()
    {
        WaveList2 asset = ScriptableObject.CreateInstance<WaveList2>();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/WaveList2.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
