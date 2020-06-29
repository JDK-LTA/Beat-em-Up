using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasManager : MonoBehaviour
{
    Player player;
    [SerializeField] Image hpBar, fireBar;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        hpBar.fillAmount = player.HpCurrent / player.HpMax;
        fireBar.fillAmount = 1 - player.TFireCd;
    }
}
