using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
//[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    private int _id = -1;

    protected float _maxHp, _currentHp;
    protected float _attackDmg, _abilityPwr;
    protected float _armor, _timeStunned;
    protected bool _canBeDamaged, _isStunned = false;

    protected float _rangeToChase;

    protected float _timeBetweenAttacks, _timeBetweenAbilities;

    public float GetHp { get => _currentHp; }

    [HideInInspector] public BoxCollider2D bcComp;
    [HideInInspector] public Rigidbody2D rbComp;

    private Transform target;
    private float _speed = 5f;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        //target = GameManager.Instance.players[0].transform;
        //bcComp = gameObject.AddComponent<BoxCollider2D>();
        rbComp = gameObject.AddComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
    }
    protected virtual void LateUpdate()
    {
        if(Vector3.Distance(transform.position, target.position) < _rangeToChase)
        {
            Chase();
        }
        else
        {
            GoForNexus();
        }
    }
    //NO PUEDO PASAR ID MANDANDO UN MENSAJE. TENGO QUE ENCONTRAR LA FORMA DE QUE CADA UNO RECIBA EL MENSAJE Y HAGA LO QUE DEBE
    public virtual void Init(int id)
    {
        target = GameManager.Instance.players[0].transform;
        bcComp = gameObject.AddComponent<BoxCollider2D>();
        //rbComp = gameObject.AddComponent<Rigidbody2D>();

        _id = id;
        _maxHp = ((WaveManager)WaveManager.Instance).enemiesThisWave[id].MaxHp;
        _currentHp = ((WaveManager)WaveManager.Instance).enemiesThisWave[id].StartingHp;

        _attackDmg = ((WaveManager)WaveManager.Instance).enemiesThisWave[id].AttackDmg;
        _abilityPwr = ((WaveManager)WaveManager.Instance).enemiesThisWave[id].AbilityPwr;

        _armor = ((WaveManager)WaveManager.Instance).enemiesThisWave[id].Armor;
        _timeStunned = ((WaveManager)WaveManager.Instance).enemiesThisWave[id].TimeStunned;

        _canBeDamaged = ((WaveManager)WaveManager.Instance).enemiesThisWave[id].CanBeDamagedWhenStart;

        _rangeToChase = ((WaveManager)WaveManager.Instance).enemiesThisWave[id].RangeToChase;
        _speed = WaveManager.Instance.enemiesThisWave[id].Speed;

        _timeBetweenAttacks = ((WaveManager)WaveManager.Instance).enemiesThisWave[id].TimeBetweenAttacks;
        _timeBetweenAbilities = ((WaveManager)WaveManager.Instance).enemiesThisWave[id].TimeBetweenAbilities;
    }
    public virtual void Init(EnemyInfo enemy)
    {
        //_id = enemy.Id;
        _maxHp = enemy.MaxHp;
        _currentHp = enemy.StartingHp;

        _attackDmg = enemy.AttackDmg;
        _abilityPwr = enemy.AbilityPwr;

        _armor = enemy.Armor;
        _timeStunned = enemy.TimeStunned;

        _canBeDamaged = enemy.CanBeDamagedWhenStart;

        _rangeToChase = enemy.RangeToChase;

        _timeBetweenAttacks = enemy.TimeBetweenAttacks;
        _timeBetweenAbilities = enemy.TimeBetweenAbilities;
    }

    protected virtual void Chase()
    {
        GoTowardsGoal(target.position);
    }

    protected virtual void GoForNexus()
    {
        GoTowardsGoal(GameManager.Instance.Nexus.transform.position);
    }

    protected virtual void GoTowardsGoal(Vector3 goal)
    {
        Vector3 dir = goal - transform.position;
        dir.Normalize();

        float x = Mathf.Abs(transform.localScale.x);
        if (dir.x < 0)
        {
            x = -x;
        }
        transform.localScale = new Vector2(x, transform.localScale.y);

        rbComp.MovePosition(transform.position + (dir * _speed * Time.deltaTime));
    }

    public void ChangeHp(float num)
    {
        _currentHp += num;

        if (_currentHp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }
}
