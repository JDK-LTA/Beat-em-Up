using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CreateItemList
{
    [MenuItem("Assets/Lists/Item List")]
    public static ItemList Create()
    {
        ItemList asset = ScriptableObject.CreateInstance<ItemList>();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/ItemList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
