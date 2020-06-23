using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase2 : MonoBehaviour
{
    protected BoxCollider2D bcComp;
    protected Rigidbody2D rbComp;
    protected Animator animComp;
    protected Transform player;

    [SerializeField] GameObject itemPrefab;

    protected bool readyToAttack = false;
    protected bool countAsChasing = true;
    protected bool dead = false;
    private float t = 0;

    protected bool goingForPlayer = false;
    [SerializeField]
    protected float maxHp = 100;
    [SerializeField]
    protected float currentHp = 100;
    [SerializeField]
    protected float attackDmg = 5;
    [SerializeField]
    protected float rangeToChase = 20;
    [SerializeField]
    protected float cdAttack = 2;
    [SerializeField]
    protected float speed = 5;
    [SerializeField]
    protected float stoppingDistanceX = 2f;
    [SerializeField]
    protected float stoppingDistanceY = 0.5f;
    [SerializeField]
    protected float chanceForItem = 5;
    [SerializeField]
    protected List<int> itemIdsToSpawn;
    [SerializeField]
    protected int difficulty;

    public bool GoingForPlayer { get => goingForPlayer; set => goingForPlayer = value; }
    public float MaxHp { get => maxHp; set => maxHp = value; }
    public float CurrentHp { get => currentHp; set => currentHp = value; }
    public float AttackDmg { get => attackDmg; set => attackDmg = value; }
    public float RangeToChase { get => rangeToChase; set => rangeToChase = value; }
    public float CdAttack { get => cdAttack; set => cdAttack = value; }
    public float Speed { get => speed; set => speed = value; }
    public float StoppingDistanceX { get => stoppingDistanceX; set => stoppingDistanceX = value; }
    public float StoppingDistanceY { get => stoppingDistanceY; set => stoppingDistanceY = value; }
    public int Difficulty { get => difficulty; set => difficulty = value; }

    protected virtual void Awake()
    {
        bcComp = GetComponent<BoxCollider2D>();
        rbComp = GetComponent<Rigidbody2D>();
        animComp = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //player = GameManager.Instance.players[0].transform;
        player = FindObjectOfType<Player>().transform;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (goingForPlayer)
        {
            if (Vector3.Distance(transform.position, player.position) > rangeToChase)
            {
                goingForPlayer = false;

                if (countAsChasing)
                {
                    EnemiesManager.Instance.CurrDiffChasing -= difficulty;
                }
            }
        }
        else
        {
            if (EnemiesManager.Instance.CurrDiffChasing < EnemiesManager.Instance.MaxDiffChasing
                && Vector3.Distance(transform.position, player.position) < rangeToChase)
            {
                goingForPlayer = true;

                if (countAsChasing)
                {
                    EnemiesManager.Instance.CurrDiffChasing += difficulty;
                }
            }
        }

        if (readyToAttack)
        {
            t += Time.deltaTime;
            if (t >= cdAttack)
            {
                t = 0;
                Attack();
            }
        }
    }

    protected virtual void LateUpdate()
    {
        if (goingForPlayer)
        {
            Chase();
        }
        else
        {
            GoForNexus();
        }
    }
    protected virtual void Chase()
    {
        GoTowardsGoal(player.position);
    }

    protected virtual void GoForNexus()
    {
        GoTowardsGoal(GameManager.Instance.Nexus.transform.position);
    }

    protected virtual void GoTowardsGoal(Vector3 goal)
    {
        Vector3 dir = goal - transform.position;

        if (Mathf.Abs(goal.x - transform.position.x) > stoppingDistanceX || Mathf.Abs(goal.y - transform.position.y) > stoppingDistanceY)
        {
            readyToAttack = false;

            dir.Normalize();

            animComp.SetBool("Walking", true);

            rbComp.MovePosition(transform.position + (dir * speed * Time.deltaTime));
        }
        else
        {
            animComp.SetBool("Walking", false);
            readyToAttack = true;
        }

        float x = Mathf.Abs(transform.localScale.x);
        if (dir.x < 0)
        {
            x = -x;
        }
        transform.localScale = new Vector2(x, transform.localScale.y);
    }

    protected virtual void Attack()
    {
        animComp.SetTrigger("Attack");
    }

    public void ChangeHp(float num)
    {
        currentHp += num;

        if (currentHp <= 0)
        {
            Die();
        }
        else if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    protected void Die()
    {
        if (!dead)
        {
            dead = true;
            animComp.SetTrigger("Die");
        }
    }
    protected virtual void DeathBehaviour()
    {
        if (Random.Range(0, 100) < chanceForItem)
        {
            SpawnItem();
        }

        Destroy(gameObject);
    }
    protected void SpawnItem()
    {
        GameObject go = Instantiate(itemPrefab, transform.position, transform.rotation);
        go.GetComponent<ItemIdInScene>().Id = itemIdsToSpawn[Random.Range(0, itemIdsToSpawn.Count)];
    }
}
