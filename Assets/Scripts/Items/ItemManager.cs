using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] bool debug = false;

    List<ItemParent> items = new List<ItemParent>();
    ItemList itemList;

    private void Start()
    {
        itemList = Resources.Load<ItemList>("ItemList");

        if (debug)
        {
            for (int i = 0; i < itemList.itemList.Count; i++)
            {
                Debug.Log(itemList.itemList[i].Type);
            }
        }
    }

    public void ApplyItem(Player player, int id)
    {
        ItemID aux = null;

        for (int i = 0; i < itemList.itemList.Count; i++)
        {
            if (id == itemList.itemList[id].Id)
            {
                aux = itemList.itemList[id];
                break;
            }
        }

        //PUEDO INTENTAR HACER DOUBLE DISPATCH AQUÍ
        switch (aux.Type)
        {
            case ItemID.ItemType.I_HEALTH_UP:
                II_HealthUp healthUpItem = new II_HealthUp(player, aux.Modifier);
                items.Add(healthUpItem);
                break;
            case ItemID.ItemType.I_SPEED_UP:
                II_SpeedUp speedUpItem = new II_SpeedUp(player, aux.Modifier);
                items.Add(speedUpItem);
                break;
            case ItemID.ItemType.O_INVINCIBILITY:
                OI_Invincibility invincibleItem = new OI_Invincibility(player, aux.TimeToExpire);
                items.Add(invincibleItem);
                break;
            case ItemID.ItemType.O_W_PEM:
                OI_W_PEM pemItem = new OI_W_PEM(player, aux.TimeToExpire, aux.TimeBetweenWaves, aux.PemGO);
                items.Add(pemItem);
                break;
            case ItemID.ItemType.O_W_POISON:
                OI_W_Poison poisonItem = new OI_W_Poison(player, aux.TimeToExpire, aux.TimeBetweenWaves, aux.PoisonDamage);
                items.Add(poisonItem);
                break;
        }
    }

    public void removeItemFromList(ItemParent item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            
        }
    }
}
