using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] protected float speed = 3f;
    protected float dmg = 1f;
    protected bool poisonEffect = false;
    ItemIdInScene itemId;

    [SerializeField] Sprite poisonSprite;

    protected void Start()
    {
        Player player = FindObjectOfType<Player>();
        Vector3 dir = /*GameManager.Instance.players[0]*/player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public void InitDmg(float a, bool poison)
    {
        dmg = a;
        if (poison)
        {
            GetComponent<SpriteRenderer>().sprite = poisonSprite;
            itemId = gameObject.AddComponent<ItemIdInScene>();
            itemId.Id = 4;
            poisonEffect = true;
        }
    }

    float timeToSelfDestroy = 5f;
    float t = 0;

    protected void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);

        t += Time.deltaTime;
        if (t > timeToSelfDestroy)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.ChangeHp(-dmg);

            if (poisonEffect)
            {
                ItemManager.Instance.ApplyItem(player, itemId.Id);
            }
        }

        if (!collision.GetComponent<EnemyBase2>() && !collision.GetComponent<ItemIdInScene>())
        {
            Destroy(gameObject);
        }
    }
}
