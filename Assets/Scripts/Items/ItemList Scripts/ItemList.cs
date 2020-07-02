using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
[CreateAssetMenu(fileName = "New Item List", menuName = "ItemList")]
#endif
public class ItemList : ScriptableObject
{
    [SerializeField]
    public List<ItemID> itemList;
}
