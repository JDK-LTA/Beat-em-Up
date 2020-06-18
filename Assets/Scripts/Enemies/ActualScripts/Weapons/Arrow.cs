using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    private float dmg = 1f;
    private bool poisonEffect = false;
    ItemIdInScene itemId;
    private void Start()
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
            itemId = gameObject.AddComponent<ItemIdInScene>();
            itemId.Id = 4;
            poisonEffect = true;
        }
    }

    private void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
