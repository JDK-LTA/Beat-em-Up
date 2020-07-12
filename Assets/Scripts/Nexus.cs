using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nexus : MonoBehaviour
{
    private float nexusHp = 200f;
    private float maxNexusHp = 200f;
    [SerializeField] Image nexusHpBar;

    public void DamageNexus(float dmg)
    {
        nexusHp -= dmg;
        nexusHpBar.fillAmount = nexusHp / maxNexusHp;
        AudioSource.PlayClipAtPoint(SoundManager.Instance.nexusHit, transform.position);

        if (nexusHp <= 0)
        {
            GameManager.Instance.EndGame(false);
        }
    }
}
