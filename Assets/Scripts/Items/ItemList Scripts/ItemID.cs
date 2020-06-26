using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemID
{
    public enum ItemType
    {
        I_HEALTH_UP,
        I_SPEED_UP,
        O_INVINCIBILITY,
        O_W_PEM,
        O_W_POISON,
        O_SPEED_CHANGE
    }
    [SerializeField] private string name = "";
    [SerializeField] private int id = -1;
    [SerializeField] private ItemType type = ItemType.I_HEALTH_UP;

    [SerializeField] private float immediateModifier = 0;

    [SerializeField] private float timeToExpire = 0;
    [SerializeField] private float poisonDamage = 0;
    [SerializeField] private float timeBetweenWaves = 0;
    [SerializeField] private GameObject pemGO = null;

    public ItemID()
    {
        if (type == ItemType.I_HEALTH_UP || type == ItemType.I_SPEED_UP)
        {
            timeToExpire = 0;
            poisonDamage = 0;
            timeBetweenWaves = 0;
        }
        else
        {
            Debug.Log("yeah babe");
            if (timeToExpire <= 0)
            {
                throw new System.Exception("Time to expire is not properly set");
            }
        }
    }

    public ItemType Type { get => type; }

    public float Modifier { get => immediateModifier; }

    public float TimeToExpire { get => timeToExpire; }
    public float PoisonDamage { get => poisonDamage; }
    public float TimeBetweenWaves { get => timeBetweenWaves; }
    public GameObject PemGO { get => pemGO; }
    public int Id { get => id; }
}
