using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item List", menuName = "ItemList")]
public class ItemList : ScriptableObject
{
    [SerializeField]
    public List<ItemID> itemList;
}
