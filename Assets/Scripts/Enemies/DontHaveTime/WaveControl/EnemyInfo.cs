using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    public enum EnemyType
    {
        MELEE,
        RANGED,
        TANK,
        HEALER
    }

    [SerializeField] private string id = "";
    [SerializeField] private EnemyType type = EnemyType.MELEE;
    [SerializeField] private float maxHp = 100, startingHp = 100;
    [SerializeField] private float attackDmg = 5, abilityPwr = 5;
    [SerializeField] private float armor = 10, timeStunned = 1;
    [SerializeField] private bool canBeDamagedWhenStart = true;

    [SerializeField] private float rangeToChase = 5;
    [SerializeField] private float speed = 5;

    [SerializeField] private float timeBetweenAttacks = 2;
    [SerializeField] private float timeBetweenAbilities = 6;

    public string Id { get => id; set => id = value; }
    public EnemyType Type { get => type; set => type = value; }

    public float MaxHp { get => maxHp; set => maxHp = value; }
    public float StartingHp { get => startingHp; set => startingHp = value; }

    public float AttackDmg { get => attackDmg; set => attackDmg = value; }
    public float AbilityPwr { get => abilityPwr; set => abilityPwr = value; }

    public float Armor { get => armor; set => armor = value; }
    public float TimeStunned { get => timeStunned; set => timeStunned = value; }

    public bool CanBeDamagedWhenStart { get => canBeDamagedWhenStart; set => canBeDamagedWhenStart = value; }

    public float RangeToChase { get => rangeToChase; set => rangeToChase = value; }

    public float TimeBetweenAttacks { get => timeBetweenAttacks; set => timeBetweenAttacks = value; }
    public float TimeBetweenAbilities { get => timeBetweenAbilities; set => timeBetweenAbilities = value; }
    public float Speed { get => speed; set => speed = value; }
}
