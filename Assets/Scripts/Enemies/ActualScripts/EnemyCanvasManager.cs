using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCanvasManager : MonoBehaviour
{
    EnemyBase2 enemy;
    [SerializeField] Image hpBar;

    private void Start()
    {
        enemy = transform.parent.GetComponent<EnemyBase2>();
    }
    private void Update()
    {
        hpBar.fillAmount = enemy.CurrentHp / enemy.MaxHp;
    }
}
