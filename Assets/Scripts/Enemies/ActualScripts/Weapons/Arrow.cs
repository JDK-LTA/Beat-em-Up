using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] protected float speed = 3f;
    protected float dmg = 1f;
    protected bool specialEffect = false;
    ItemIdInScene itemId;

    [SerializeField] Sprite specialSprite;

    protected void Start()
    {
    }
    public void InitDmg(float a, bool poison, bool chasingPlayer, int specialArrowId)
    {
        dmg = a;
        if (poison)
        {
            GetComponent<SpriteRenderer>().sprite = specialSprite;
            itemId = gameObject.AddComponent<ItemIdInScene>();
            itemId.Id = specialArrowId;
            specialEffect = true;
        }

        Vector3 dir;
        if (chasingPlayer)
        {
            dir = FindObjectOfType<Player>().transform.position - transform.position;
        }
        else
        {
            dir = GameManager.Instance.Nexus.transform.position - transform.position;

        }

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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

            if (specialEffect)
            {
                ItemManager.Instance.ApplyItem(player, itemId.Id);
            }
        }

        if (!collision.GetComponent<EnemyBase2>() && !collision.GetComponent<ItemIdInScene>() && collision.tag != "Background")
        {
            Destroy(gameObject);
        }
    }
}
